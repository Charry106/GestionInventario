using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/inventario")]
    public class ConsultaInventarioController : ControllerBase
    {
        private readonly ConsultaInventarioServicio _consultaInventarioServicio;
        public ConsultaInventarioController(ConsultaInventarioServicio consultaInventarioServicio)
        {
            _consultaInventarioServicio = consultaInventarioServicio;
        }

        // Endpoint para obtener el inventario completo
        [HttpGet("Todos")]
        public async Task<ActionResult<List<ProductoInventarioDto>>> ObtenerInventarioCompleto()
        {
            var inventario = await _consultaInventarioServicio.ObtenerInventarioCompletoAsync();
            return Ok(inventario);
        }

        // Endpoint para consultar el inventario por Nombre y Categoria
        [HttpGet("Buscar")]
        public async Task<ActionResult<List<ProductoInventarioDto>>> ConsultarPorCriterios(string nombre, string categoria)
        {
            var inventario = await _consultaInventarioServicio.ConsultarPorCriterioAsync(nombre, categoria);
            return Ok(inventario);
        }
    }
}