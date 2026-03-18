using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoriaController : ControllerBase
    {
        private readonly ISubCategoriaFlujo _subCategoriaFlujo;

        public SubCategoriaController(ISubCategoriaFlujo subCategoriaFlujo)
        {
            _subCategoriaFlujo = subCategoriaFlujo;
        }

        [HttpGet("{idCategoria}")]
        public async Task<IActionResult> Obtener(Guid idCategoria)
        {
            var resultado = await _subCategoriaFlujo.Obtener(idCategoria);
            return Ok(resultado);
        }
    }
}