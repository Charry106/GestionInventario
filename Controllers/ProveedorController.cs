using GestionInventario;
using GestionInventario.Datos;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos.Repositorio;
using GestionInventario.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/Proveedores")]
    public class ProveedoresController : ControllerBase
    {
        private readonly ProveedorServicio _proveedorServicio;
        private readonly IProductoRepositorio _productoRepositorio;

        public ProveedoresController(ProveedorServicio proveedorServicio,  IProductoRepositorio productoRepositorio)

        {
            _proveedorServicio = proveedorServicio;
            _productoRepositorio = productoRepositorio;
        }

        [HttpPost("Crear")]
        public IActionResult CrearProveedor([FromBody] ProveedorDto proveedorDto)
        {
            if (proveedorDto == null)
            {
                return BadRequest("El proveedor no puede ser nulo.");
            }

            // Mapear el ProveedorDto a una entidad Proveedor
            var proveedor = new Proveedor
            {
                Nit = proveedorDto.Nit,
                Nombre = proveedorDto.Nombre,
                Direccion = proveedorDto.Direccion,
                Telefono = proveedorDto.Telefono,
                Email = proveedorDto.Email
            };

            _proveedorServicio.CrearProveedor(proveedor);
            return Ok("Proveedor creado exitosamente");
        }
 
        [HttpGet("Mostrar Todos")]
        public IActionResult ObtenerTodos()
        {
            var proveedores = _proveedorServicio.ObtenerTodos();
            return Ok(proveedores);
        }

        [HttpPut("Actualizar{id}")]
        public IActionResult ActualizarProveedor(int id, [FromBody] Proveedor proveedorActualizado)
        {
            var proveedorExistente = _proveedorServicio.ObtenerProveedor(id);
            if (proveedorExistente == null) return NotFound("Proveedor no encontrado");

            proveedorExistente.Nombre = proveedorActualizado.Nombre;
            proveedorExistente.Direccion = proveedorActualizado.Direccion;
            proveedorExistente.Telefono = proveedorActualizado.Telefono;
            proveedorExistente.Email = proveedorActualizado.Email;
            
            _proveedorServicio.ActualizarProveedor(proveedorExistente);
            return Ok("Proveedor actualizado exitosamente");
        }

        [HttpDelete("Eliminar{id}")]
        public IActionResult EliminarProveedor(int id)
        {
            var proveedor = _proveedorServicio.ObtenerProveedor(id);
            if (proveedor == null) return NotFound("Proveedor no encontrado");
            
            _proveedorServicio.EliminarProveedor(id);
            return Ok("Proveedor eliminado exitosamente");
        }

        [HttpGet("{proveedorId}/productos")]
        public async Task<IActionResult> ObtenerProductosPorProveedor(int proveedorId)
        {
            var productos = await _productoRepositorio.ObtenerProductoPorProveedorIdAsync(proveedorId);
            if (productos == null || !productos.Any())
            {
                return NotFound("No se encontraron productos para este proveedor.");
            }
            return Ok(productos);
        }
    }
}
