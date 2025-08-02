﻿using Microsoft.EntityFrameworkCore;
using Northwind2_v36.Data;
using Northwind2_v36.Entities;

namespace Northwind2_v36.Services
{
    public interface IServiceEmployes
    {
        Task<List<Employe>> ObtenirEmployes();
        Task<Employe?> ObtenirEmploye(int id);
    }

    public class ServiceEmployes : IServiceEmployes
    {
        private readonly ContexteNorthwind _contexte;

        public ServiceEmployes(ContexteNorthwind contexte)
        {
            _contexte = contexte;
        }
        public async Task<List<Employe>> ObtenirEmployes()
        {
            return await _contexte.Employés.ToListAsync();
        }

        public async Task<Employe?> ObtenirEmploye(int id)
        {
            return await _contexte.Employés.FindAsync(id);
        }


    }
    
}
