using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Northwind2.Data;

namespace Northwind2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Récupère la chaîne de connexion à la base dans les paramètres
            string? connect = builder.Configuration.GetConnectionString("Northwind2Connect");

            // Add services to the container.
            // Enregistre la classe de contexte de données comme service
            // en lui indiquant la connexion à utiliser
            builder.Services.AddDbContext<ContexteNorthwind>(opt => opt.UseSqlServer(connect));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind2 API v1");
                    c.RoutePrefix = ""; // ?? Swagger à la racine
                });

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();


        }
    }
}
