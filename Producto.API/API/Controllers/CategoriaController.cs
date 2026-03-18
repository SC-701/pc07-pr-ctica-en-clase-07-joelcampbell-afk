using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaFlujo _categoriaFlujo;

        public CategoriaController(ICategoriaFlujo categoriaFlujo)
        {
            _categoriaFlujo = categoriaFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _categoriaFlujo.Obtener();
            return Ok(resultado);
        }
    }
}