using Microsoft.AspNetCore.Mvc;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServicio _productoServicio;

        public ProductoController(IProductoServicio productoServicio)
        {
            _productoServicio = productoServicio;
        }

        [HttpPost("crear")]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            _productoServicio.CrearProducto(producto);
            return Ok("Producto creado exitosamente");
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerProducto(int id)
        {
            var producto = _productoServicio.ObtenerProducto(id);
            return producto == null ? NotFound("Producto no encontrado") : Ok(producto);
        }

        [HttpGet("todos")]
        public IActionResult ObtenerTodos() => Ok(_productoServicio.ObtenerTodos());

        [HttpPut("{id}")]
        public IActionResult ModificarProducto(int id, [FromBody] Producto producto)
        {
            producto.Id = id;
            _productoServicio.ModificarProducto(producto);
            return Ok("Producto modificado exitosamente");
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            _productoServicio.EliminarProducto(id);
            return Ok("Producto eliminado exitosamente");
        }

        [HttpPost("{id}/actualizar-inventario")]
        public IActionResult ActualizarInventario(int id, int cantidad, string tipoMovimiento, string motivo)
        {
            _productoServicio.ActualizarInventario(id, cantidad, tipoMovimiento, motivo);
            return Ok("Inventario actualizado exitosamente");
        }
    }
}

