using Microsoft.EntityFrameworkCore;
using Northwind2.Entities;

namespace Northwind2.Data
{
	public class ContexteNorthwind : DbContext
	{
		public ContexteNorthwind(DbContextOptions<ContexteNorthwind> options)
			 : base(options)
		{
		}

		public virtual DbSet<Adresse> Adresses { get; set; }
		public virtual DbSet<Employe> Employés { get; set; }
		public virtual DbSet<Region> Régions { get; set; }
		public virtual DbSet<Territoire> Territoires { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Adresse>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).ValueGeneratedNever();
				entity.Property(e => e.Ville).HasMaxLength(40);
				entity.Property(e => e.Pays).HasMaxLength(40);
				entity.Property(e => e.Tel).HasMaxLength(20).IsUnicode(false);
				entity.Property(e => e.CodePostal).HasMaxLength(20).IsUnicode(false);
				entity.Property(e => e.Region).HasMaxLength(40);
				entity.Property(e => e.Rue).HasMaxLength(100);
			});

			modelBuilder.Entity<Employe>(entity =>
			{
				entity.ToTable("Employes");
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Prenom).HasMaxLength(40);
				entity.Property(e => e.Nom).HasMaxLength(40);
				entity.Property(e => e.Notes).HasMaxLength(1000);
				entity.Property(e => e.Photo).HasColumnType("image");
				entity.Property(e => e.Fonction).HasMaxLength(40);
				entity.Property(e => e.Civilite).HasMaxLength(40);

				// Relation de la table Employe sur elle-même 
				entity.HasOne<Employe>().WithMany().HasForeignKey(d => d.IdManager);

				// Relation Employe - Adresse de cardinalités 0,1 - 1,1
				entity.HasOne<Adresse>(e => e.Adresse).WithOne().HasForeignKey<Employe>(d => d.IdAdresse)
					.OnDelete(DeleteBehavior.NoAction);
			});

			modelBuilder.Entity<Affectation>(entity =>
			{
				entity.ToTable("Affectations");
				entity.HasKey(e => new { e.IdEmploye, e.IdTerritoire });

				//entity.HasOne<Employe>().WithMany().HasForeignKey(a => a.IdEmploye);
				//entity.HasOne<Territoire>().WithMany().HasForeignKey(a => a.IdTerritoire);
			});

			modelBuilder.Entity<Region>(entity =>
			{
				entity.ToTable("Regions");
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Id).ValueGeneratedNever();
				entity.Property(e => e.Nom).HasMaxLength(40);

				//entity.HasMany<Territoire>().WithOne().HasForeignKey(d => d.IdRegion)
				//	.OnDelete(DeleteBehavior.NoAction);
			});

			modelBuilder.Entity<Territoire>(entity =>
			{
				entity.ToTable("Territoires");
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Id).HasMaxLength(20).IsUnicode(false);
				entity.Property(e => e.Nom).HasMaxLength(40);

				entity.HasOne<Region>(t => t.Région).WithMany(r => r.Territoires).HasForeignKey(d => d.IdRegion)
								.OnDelete(DeleteBehavior.NoAction);

				// Crée la relation N-N avec Employe en utilisant l'entité Affectation comme entité d'association
				entity.HasMany<Employe>().WithMany(e => e.Territoires).UsingEntity<Affectation>(
					l => l.HasOne<Employe>().WithMany().HasForeignKey(a => a.IdEmploye),
					r => r.HasOne<Territoire>().WithMany().HasForeignKey(a => a.IdTerritoire));
			});

			// Exemple de syntaxe pour créer un jeu de données par le code

			if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
			{
				//modelBuilder.Entity<Adresse>().HasData(
				//	new Adresse
				//	{
				//		Id = new Guid("01fcbc07-b6ba-4f3a-ac69-891e5a41b14e"),
				//		Rue = "1600 route des milles",
				//		Ville = "Aix en provence",
				//		CodePostal = "13070",
				//		Pays = "France",
				//		Region = "PACA",
				//		Tel = "07 33 33 33 33"
				//	},
				//	new Adresse
				//	{
				//		Id = new Guid("02fcbc07-b6ba-4f3a-ac69-891e5a41b14e"),
				//		Rue = "Cheongdam Fashion Street",
				//		Ville = "Séoul",
				//		CodePostal = "150000",
				//		Pays = "Corée du Sud",
				//		Region = "Gangnam",
				//		Tel = "01 98 76 54 32"
				//	});
			

                /*modelBuilder.Entity<Employe>().HasData(
				new Employe
				{
					Id = 11,
					Nom = "Mousquet",
					Prenom = "Kévin",
					IdManager = 2,
					Fonction = "Architect Engeneer",
					Civilite = "Mr.",
					DateNaissance = new DateTime(1994, 11, 01),
					DateEmbauche = new DateTime(2020, 10, 01),
					IdAdresse = new Guid("01fcbc07-b6ba-4f3a-ac69-891e5a41b14e")
				},
				new Employe
				{
					Id = 12,
					Nom = "Park",
					Prenom = "Shin Hee",
					IdManager = 2,
					Fonction = "Sales Representative",
					Civilite = "Mrs.",
					DateNaissance = new DateTime(2000, 5, 20),
					DateEmbauche = new DateTime(2023, 10, 11),
					IdAdresse = new Guid("01fcbc07-b6ba-4f3a-ac69-891e5a41b14e")
				});*/
			}

        }
    }
}
