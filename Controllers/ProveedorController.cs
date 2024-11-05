using Microsoft.AspNetCore.Mvc;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/proveedores")]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorServicio _proveedorServicio;

        public ProveedorController(IProveedorServicio proveedorServicio)
        {
            _proveedorServicio = proveedorServicio;
        }

        [HttpPost("crear")]
        public IActionResult CrearProveedor([FromBody] Proveedor proveedor)
        {
            _proveedorServicio.CrearProveedor(proveedor);
            return Ok("Proveedor creado exitosamente");
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerProveedor(int id)
        {
            var proveedor = _proveedorServicio.ObtenerProveedor(id);
            return proveedor == null ? NotFound("Proveedor no encontrado") : Ok(proveedor);
        }

        [HttpGet("todos")]
        public IActionResult ObtenerTodos() => Ok(_proveedorServicio.ObtenerTodos());

        [HttpPut("{id}")]
        public IActionResult ModificarProveedor(int id, [FromBody] Proveedor proveedor)
        {
            proveedor.Id = id;
            _proveedorServicio.ModificarProveedor(proveedor);
            return Ok("Proveedor modificado exitosamente");
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProveedor(int id)
        {
            _proveedorServicio.EliminarProveedor(id);
            return Ok("Proveedor eliminado exitosamente");
        }
    }
}

