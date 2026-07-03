using Dsw2026Ej15.Api.Configurations;
using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAplicationPersistence(builder.Configuration);
            //termindado el ejercicio 16

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();
            builder.Services.AddScoped<IPersistence, PersistenceEf>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            { 
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();

            app.MapControllers();
            app.MapHealthChecks("/health-check");

            app.LoadSpecialityData();

            app.Run();
        }
    }
}
