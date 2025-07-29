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

        public virtual DbSet<Adresse> Adresses { get; set; } // virtuel pour les redefinir pour des tests unitaires
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
                entity.ToTable("Employes"); // nom de la table
                entity.HasKey(e => e.Id); // clé primaire

                entity.Property(e => e.Prenom).HasMaxLength(40);
                entity.Property(e => e.Nom).HasMaxLength(40);
                entity.Property(e => e.Notes).HasMaxLength(1000);
                entity.Property(e => e.Photo).HasColumnType("image"); // type image pour SQL Server
                entity.Property(e => e.Fonction).HasMaxLength(40);
                entity.Property(e => e.Civilite).HasMaxLength(40);

        
            });

            modelBuilder.Entity<Affectation>(entity =>
            {
                entity.ToTable("Affectations");
                entity.HasKey(e => new { e.IdEmploye, e.IdTerritoire }); // clé primaire porte sur les deux colonnes

                entity.Property(e => e.IdTerritoire).HasMaxLength(20).IsUnicode(false); // ce n'est pas un champ unicode

            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Regions");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Nom).HasMaxLength(40);
            });

            modelBuilder.Entity<Territoire>(entity =>
            {
                entity.ToTable("Territoires");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Nom).HasMaxLength(40);

     
            });




        }
    }
    
    
}
