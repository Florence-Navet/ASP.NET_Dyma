using JobOverview.Entities;
using JobOverview_v55.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobOverview_v55.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogicielsController : ControllerBase
    {
        private readonly IServiceLogiciels _serviceLog;

        public LogicielsController(IServiceLogiciels service)
        {
            _serviceLog = service;
        }

        // GET: api/Logiciels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logiciel>>> GetLogiciels()
        {
            return await _serviceLog.ObtenirLogiciels();
        }

        // GET: api/Logiciels/ABC
        [HttpGet("{code}")]
        public async Task<ActionResult<Logiciel>> GetLogiciel(string code)
        {
            var logiciel = await _serviceLog.ObtenirLogiciel(code);

            if (logiciel == null)
            {
                return NotFound();
            }

            return logiciel;
        }
    }
}