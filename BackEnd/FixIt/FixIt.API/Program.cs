using DotNetEnv;
using FixIt.API.SignalR;
using FixIt.Core;
using FixIt.Core.MiddleWare;
using FixIt.Core.Settings;
using FixIt.Domain.Entities;
using FixIt.Infrastructure;
using FixIt.Infrastructure.Context;
using FixIt.Service;
using FixIt.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


namespace FixIt.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            Env.Load();
            builder.Configuration.AddEnvironmentVariables();



            #region Service Container

            // Add services to the container.

            builder.Services.AddControllers();
            // ضيف ده في Program.cs لو مش موجود
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .SetIsOriginAllowed(origin => true) // للتست بس
                           .AllowCredentials(); // مهمة جداً للـ SignalR
                });
            });
            // وتحت استخدمها: app.UseCors("AllowAll");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]));
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = secretKey,
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:IssuerIP"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:AudienceIP"]
                    };

                    // دمج SignalR مع التأكد من حظر المستخدم
                    options.Events = new JwtBearerEvents
                    {
                        // 1. قراءة التوكن للـ SignalR
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        },

                        // 2. 👈 الجزء الجديد: التأكد إن اليوزر مش معموله حظر مع كل Request
                        OnTokenValidated = async context =>
                        {
                            // جلب الـ ID بتاع اليوزر من التوكن
                            var userId = context.Principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                            if (!string.IsNullOrEmpty(userId))
                            {
                                // جلب الـ DbContext بتاعك
                                var dbContext = context.HttpContext.RequestServices.GetRequiredService<FIXITDbContext>();

                                // البحث عن اليوزر في الداتابيز
                                // ملاحظة: لو الـ ID عندك في الداتابيز int مش string، هتحتاج تعمل int.Parse(userId)
                                var user = await dbContext.Users.FindAsync(userId);

                                // لو اليوزر مش موجود أو الإدمن عمله حظر (IsBlock = true)
                                // ملحوظة: تأكد إن اسم الخاصية IsBlock مكتوب زي ما هو في الـ Entity بتاعك
                                if (user == null || user.isBlocked)
                                {
                                    context.Fail("This account has been blocked by the administrator.");
                                }
                            }
                        }
                    };
                });
            // L
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // 1. تعريف نوع الحماية (Bearer Token)
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your valid token in the text input below.\r\n\r\nExample: \"eyJhbGci...\""
                });

                // 2. تطبيق الحماية على كل الـ Endpoints (عشان يظهر القفل ويتقفل)
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddDbContext<FIXITDbContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDbContext<FIXITDbContext>();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            #endregion



            //DI
            builder.Services.AddInfrastructureDependencies();
            builder.Services.AddCoreDependances();
            builder.Services.AddServiceDependencies();
            //Img
            builder.Services.AddHttpContextAccessor();

            // Register repositories and business services
            builder.Services.AddScoped<JWTService>();

            builder.Services.AddSignalR();
            var app = builder.Build();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI();


            #region Request Piplines

            if (app.Environment.IsDevelopment())
            {
            }

            app.UseRouting(); // 1. Routing الأول
            app.UseCors("AllowAll"); // 2. الـ CORS بالاسم اللي انت معرفه فوق


            app.UseAuthentication(); // 3. 👈 ضفنا دي (التأكد من التوكن)
            app.UseAuthorization();  // 4. (التأكد من الصلاحيات)


            app.MapControllers();
            app.MapHub<ChatHub>("/chat"); // 5. 👈 المسار هنا اسمه /chat

            app.Run();
            #endregion
        }
    }
}
