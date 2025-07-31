using JobOverview.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Version = JobOverview.Entities.Version;

namespace JobOverview.Data
{
    public class ContexteJobOverview : DbContext
    {
        
        public ContexteJobOverview(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Filiere> Filieres { get; set; }
        public virtual DbSet<Logiciel> Logiciels { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Version> Versions { get; set; }

        public virtual DbSet<Release> Releases { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filiere>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Nom).HasMaxLength(60).IsUnicode(false);

            });

            modelBuilder.Entity<Logiciel>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.codeFiliere).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Nom).HasMaxLength(60).IsUnicode(false);

                entity.HasOne<Filiere>().WithMany()
                     .HasForeignKey(d => d.codeFiliere)
                     .OnDelete(DeleteBehavior.NoAction);


            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => new{ e.Code, e.CodeLogiciel});

                entity.Property(e => e.Code).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.CodeLogiciel).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.CodeModuleParent).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.CodeLogicielParent).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Nom).HasMaxLength(20).IsUnicode(false);


                entity.HasOne<Logiciel>().WithMany()
                    .HasForeignKey(d => d.CodeLogiciel)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<Module>().WithMany()
                    .HasForeignKey(d => new {d.CodeModuleParent, d.CodeLogicielParent })
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FKModulesCodeModParentCodeLogParent"); // pour changer le nom de la contrainte

            });

            modelBuilder.Entity<Version>(entity =>
            { 
                entity.HasKey(e => new { e.Numero, e.CodeLogiciel });

                entity.Property(e => e.Numero);
                entity.Property(e => e.CodeLogiciel).HasMaxLength(20).IsUnicode(false);

                entity.Property(e => e.DateOuverture);
                entity.Property(e => e.DateSortiePrevue);
                entity.Property(e => e.DateSortieReelle);


                entity.HasOne<Logiciel>().WithMany()
                        .HasForeignKey(d => d.CodeLogiciel)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FKversionLogCodeLog");

            });

            modelBuilder.Entity<Release>(entity =>
            {
                entity.HasKey(e => new { e.Numero, e.NumeroVersion, e.CodeLogiciel });

                entity.Property(e => e.NumeroVersion);
                entity.Property(e => e.CodeLogiciel).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.DatePubli);

                entity.HasOne<Version>().WithMany()
                        .HasForeignKey(d => new { d.Numero, d.CodeLogiciel }); // on ne met rien pour suppression en cascade

            });
        }
    }
}



