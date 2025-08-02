using Microsoft.AspNetCore.Mvc;
using Northwind2.Entities;
using Northwind2.Services;

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
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmployés(
            [FromQuery] string? rechercheNom, 
            [FromQuery] DateTime? dateEmbaucheMax) // renvoie un resultat de requete
        {
            var employés = await _serviceEmp.ObtenirEmployes(rechercheNom, dateEmbaucheMax);
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

        [HttpGet("/api/Regions/{id}")]
        public async Task<ActionResult<Region>> GetRégion(int id)
        {
            Region? region = await _serviceEmp.ObtenirRégion(id);

            if (region == null) return NotFound();

            return Ok(region);
        }



    }
}
