using Microsoft.AspNetCore.Mvc;
using Northwind2_v36.Entities;
using Northwind2_v36.Services;

namespace Northwind2_v36.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly IServiceEmployes _serviceEmp;

        public EmployesController(IServiceEmployes service)
        {
            _serviceEmp = service ;
        }

        // GET: api/Employes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmployés() // renvoie un resultat de requete
        {
            var employés = await _serviceEmp.ObtenirEmployes();
            return Ok(employés); // methode ok de la classe ControllerBase
        }

        // GET: api/Employes/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Employe>> GetEmploye(int id)
        {
            var employe = await _serviceEmp.ObtenirEmploye(id); // cette methode renvoie nul si pas trouvé

            if (employe == null)
            {
                return NotFound();
            }

            return Ok(employe);
        }

      
    }
}
