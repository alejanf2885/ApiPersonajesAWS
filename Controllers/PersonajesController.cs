using ApiPersonajesAWS.Models;
using ApiPersonajesAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly RepositorieTelevision repositorie;

        public PersonajesController(RepositorieTelevision repositorie)
        {
            this.repositorie = repositorie;
        }

        [HttpGet]
        public async Task<ActionResult<List<Personaje>>> Get()
        {
            return await this.repositorie.GetAllPersonajesAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(string nombre, string imagen)
        {
            await this.repositorie.InsertPersonajeAsync(nombre, imagen);
            return Ok();
        }
    }
}
