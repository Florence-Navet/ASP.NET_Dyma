using Microsoft.EntityFrameworkCore;
using Northwind2.Data;
using Northwind2.Entities;

namespace Northwind2.Services
{
    public interface IServiceEmployes
    {
        Task<List<Employe>> ObtenirEmployes(string? rechercheNom, DateTime? dateEmbaucheMax);
        Task<Employe?> ObtenirEmploye(int id);
        Task<Region?> ObtenirRégion(int id);
    }

    public class ServiceEmployes : IServiceEmployes
    {
        private readonly ContexteNorthwind _contexte;

        public ServiceEmployes(ContexteNorthwind contexte)
        {
            _contexte = contexte;
        }
        public async Task<List<Employe>> ObtenirEmployes(string? rechercheNom, DateTime? dateEmbaucheMax)
        {
            //var req = from e in _contexte.Employés select e;

            //selection partielle d'employés
            var req = from e in _contexte.Employés
                      where (rechercheNom == null || e.Nom.Contains(rechercheNom))
                      && (dateEmbaucheMax == null || e.DateEmbauche == null || e.DateEmbauche <= dateEmbaucheMax)
                      select new Employe
                      {
                          Id = e.Id,
                          Civilite = e.Civilite,
                          Nom = e.Nom,
                          Prenom = e.Prenom,
                          Fonction = e.Fonction,
                          DateEmbauche = e.DateEmbauche
                      };

            //tri par date d'embauche décroissante
            if (dateEmbaucheMax != null)
                req = req.OrderByDescending(e => e.DateEmbauche);
            else
                req = req.OrderBy(e => e.Nom).ThenBy(e => e.Prenom);

                return await req.ToListAsync();
            //return await _contexte.Employés.ToListAsync();
        }

        // a partir de 2 niveaux c'est ThenInclude
        public async Task<Employe?> ObtenirEmploye(int id)
        {
            var req = from e in _contexte.Employés
                      .Include(e => e.Adresse) 
                      .Include(e => e.Territoires)
                      .ThenInclude(t => t.Région)
                      where (e.Id == id) select e;
            return await req.FirstOrDefaultAsync();
            //return await _contexte.Employés.FindAsync(id);
        }

        // Récupère une région et ses territoires
        //le include permet de charger les territoires associés à la région
        public async Task<Region?> ObtenirRégion(int id)
        {
            var req = from r in _contexte.Régions.Include(r => r.Territoires)
                      where r.Id == id
                      select r;

            return await req.FirstOrDefaultAsync();
        }


    }
    
}
