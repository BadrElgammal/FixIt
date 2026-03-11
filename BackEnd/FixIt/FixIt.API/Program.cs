using FixIt.Core;
using FixIt.Core.MiddleWare;
using FixIt.Infrastructure;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using FixIt.Infrastructure.Repositories;
using FixIt.Service;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nest;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace FixIt.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Service Container

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy => policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme  = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]));
                    //validate function
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = secretKey,
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:IssuerIP"],
                        ValidateAudience = true,
                        ValidAudience= builder.Configuration["Jwt:AudienceIP"]
                    };
                });
            // L
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FIXITDbContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDbContext<FIXITDbContext>();






            #endregion
            // Register repositories and business services
            builder.Services.AddInfrastructureDependencies();
            builder.Services.AddServiceDependencies();
            builder.Services.AddCoreDependances();
            builder.Services.AddScoped<JWTService>();

            var app = builder.Build();

            #region Request Piplines


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }
                app.UseSwagger();
                app.UseSwaggerUI();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors("AllowAngularApp");
            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
