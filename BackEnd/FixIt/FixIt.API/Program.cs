using DotNetEnv;
using FixIt.API.SignalR;
using FixIt.Core;
using FixIt.Core.MiddleWare;
using FixIt.Infrastructure;
using FixIt.Infrastructure.Context;
using FixIt.Service;
using FixIt.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

                    // 👇 الكود ده إجباري عشان الـ SignalR يقرأ التوكن من اللينك
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            // لو الريكويست رايح للـ /chat، اقرأ التوكن
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            // L
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FIXITDbContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDbContext<FIXITDbContext>();


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
