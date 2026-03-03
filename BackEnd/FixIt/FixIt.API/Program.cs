using FixIt.Infrastructure;
using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using FixIt.Infrastructure.Repositories;
using FixIt.Service;
using FixIt.Service.Abstracts;
using FixIt.Service.Services;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;

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
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<FIXITDbContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDbContext<FIXITDbContext>();



            // Register repositories and business services
            builder.Services.AddInfrastructureDependencies();
            builder.Services.AddServiceDependencies();



            #endregion

            var app = builder.Build();

            #region Request Piplines


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
