using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind2.Entities
{
	public class Employe
	{
		public int Id { get; set; }
		public Guid IdAdresse { get; set; }
		public int? IdManager { get; set; }
		public string Nom { get; set; } = string.Empty;
		public string Prenom { get; set; } = string.Empty;
		public string? Fonction { get; set; }
		public string? Civilite { get; set; }
		public DateTime? DateNaissance { get; set; }
		public DateTime? DateEmbauche { get; set; }
		public byte[]? Photo { get; set; }
		public string? Notes { get; set; }

		//propriété de navigation
		public virtual Adresse Adresse { get; set; } = null!;
		public virtual List<Territoire> Territoires { get; set; } = new();
    }

	public class Adresse
	{
		public Guid Id { get; set; }
		public string Rue { get; set; } = string.Empty;
		public string Ville { get; set; } = string.Empty;
		public string CodePostal { get; set; } = string.Empty;
		public string Pays { get; set; } = string.Empty;
		public string? Region { get; set; }
		public string? Tel { get; set; }
	}


	public class Affectation
	{
		public int IdEmploye { get; set; }
		public string IdTerritoire { get; set; } = string.Empty;
	}

	public class Territoire
	{
		//[Key, MaxLength(20), Unicode(false)]
        public string Id { get; set; } = string.Empty;

		//[ForeignKey("Région")]
		public int IdRegion { get; set; }


		//[MaxLength(40)]
        public string Nom { get; set; } = string.Empty;

		//propriété de navigation
		public virtual Region Région { get; set; } = null!;

        //propriété de navigation - elle doit être virtuelle pour EF Core - équivalent de on delete no action
        //[DeleteBehavior(DeleteBehavior.NoAction)]

        //public virtual Region Région { get; set; } = null!; // ! pour indiquer que cette propriété ne sera jamais nulle
    }

	public class Region
	{
		public int Id { get; set; }
		public string Nom { get; set; } = string.Empty;

        //propriété de navigation - elle doit être virtuelle pour EF Core
        // elle est initialisée pour éviter les problèmes de nullité
        public virtual List<Territoire> Territoires { get; set; } = new();
    }
}
