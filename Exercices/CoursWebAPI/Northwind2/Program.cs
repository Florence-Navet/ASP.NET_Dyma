
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Northwind2.Data;
using Northwind2.Services;
using System;
using System.Text.Json.Serialization;

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
            // pour désactiver le suivi des modifications
            // on utilise UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            builder.Services.AddDbContext<ContexteNorthwind>(opt => opt.UseSqlServer(connect).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            //enregistre les services métier
            // il aura la duree de vie d'une requête HTTP
            builder.Services.AddScoped<IServiceEmployes, ServiceEmployes>();

            //evite une boucle infinie de références circulaires
            builder.Services.AddControllers().AddJsonOptions(opt =>
			opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Si l'application est en mode développement, active Swagger
            if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}