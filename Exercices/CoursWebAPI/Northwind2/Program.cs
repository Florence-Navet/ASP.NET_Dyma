
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Northwind2_v36.Data;
using Northwind2_v36.Services;
using System;

namespace Northwind2_v36
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// R�cup�re la cha�ne de connexion � la base dans les param�tres
			string? connect = builder.Configuration.GetConnectionString("Northwind2Connect");

			// Add services to the container.
			// Enregistre la classe de contexte de donn�es comme service
			// en lui indiquant la connexion � utiliser
			builder.Services.AddDbContext<ContexteNorthwind>(opt => opt.UseSqlServer(connect));

            //enregistre les services m�tier
            // il aura la duree de vie d'une requ�te HTTP
            builder.Services.AddScoped<IServiceEmployes, ServiceEmployes>();

            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Si l'application est en mode d�veloppement, active Swagger
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