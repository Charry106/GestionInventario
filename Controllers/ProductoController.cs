using GestionInventario.Datos;
using GestionInventario.Datos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.DTOS;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IProveedorRepositorio _proveedorRepositorio;
        private readonly IMovimientoInventarioRepositorio _movimientoInventarioRepositorio;
        private readonly IMovimientoInventarioServicio _movimientoServicio;

        public ProductoController(
            IProductoRepositorio productoRepositorio,
            IProveedorRepositorio proveedorRepositorio,
            IMovimientoInventarioRepositorio movimientoInventarioRepositorio, 
            IMovimientoInventarioServicio movimientoServicio)
        {
            _productoRepositorio = productoRepositorio;
            _proveedorRepositorio = proveedorRepositorio;
            _movimientoInventarioRepositorio = movimientoInventarioRepositorio;
            _movimientoServicio = movimientoServicio;
            
        }

        // Endpoint para crear un producto sin movimientos
        [HttpPost("CrearProducto")]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        var producto = new Producto
            {
                CodigoProducto = productoDto.CodigoProducto,
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Categoria = productoDto.Categoria,
                Marca = productoDto.Marca,
                FechaExpiracion = productoDto.FechaExpiracion,
                ProveedorId = productoDto.ProveedorId
            };

            await _productoRepositorio.CrearProductoAsync(producto);
            return Ok("Producto creado exitosamente.");
        }

        // Endpoint para agregar un movimiento de inventario a un producto existente
        [HttpPost("{productoId}/AgregarMovimiento")]
        public async Task<IActionResult> AgregarMovimiento(int productoId, [FromBody] MovimientoInventarioDto movimientoDto)
        {
            var producto = await _productoRepositorio.ObtenerProductoPorIdAsync(productoId);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            var movimiento = new MovimientoInventario
            {
                ProductoId = productoId,
                Fecha = DateTime.Now,
                TipoMovimiento = movimientoDto.TipoMovimiento,
                Cantidad = movimientoDto.Cantidad,
                PrecioUnitario = movimientoDto.PrecioUnitario,
                Motivo = movimientoDto.Motivo
            };

            await _movimientoInventarioRepositorio.RegistrarMovimientoAsync(movimiento);    
            return Ok("Movimiento de inventario registrado exitosamente.");
        }

        // Endpoint para obtener todos los productos
        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<Producto>>> ObtenerTodos()
        {
            var productos = await _productoRepositorio.ObtenerTodosAsync();
            return Ok(productos);
        }

        // Endpoint para obtener un producto por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            var producto = await _productoRepositorio.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }
            return Ok(producto);
        }

        // Endpoint para actualizar un producto
        [HttpPut("ActualizarProducto/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto productoActualizado)
        {
            var productoExistente = await _productoRepositorio.ObtenerProductoPorIdAsync(id);
            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Actualizar los campos del producto
            productoExistente.Nombre = productoActualizado.Nombre;
            productoExistente.Descripcion = productoActualizado.Descripcion;
            productoExistente.Categoria = productoActualizado.Categoria;
            productoExistente.Marca = productoActualizado.Marca;
            productoExistente.FechaExpiracion = productoActualizado.FechaExpiracion;
            productoExistente.ProveedorId = productoActualizado.ProveedorId;

            await _productoRepositorio.ActualizarProductoAsync(productoExistente);
            return Ok("Producto actualizado exitosamente.");
        }

        // Endpoint para eliminar un producto
        [HttpDelete("EliminarProducto/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _productoRepositorio.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            await _productoRepositorio.EliminarProductoAsync(id);
            return Ok("Producto eliminado exitosamente.");
        }
    
        // Endpoint para adicionar existencias a un producto
        [HttpPost("{productoId}/adicionar-existencias")]
        public async Task<IActionResult> AdicionarExistencia([FromRoute] int productoId, [FromBody] MovimientoInventarioDto movimientoDto)
        {
            // Verificar que el producto exista
            var producto = await _productoRepositorio.ObtenerProductoPorIdAsync(productoId);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Llamar al método AdicionarExistenciaAsync con todos los parámetros requeridos
            await _movimientoServicio.AdicionarExistenciaAsync(
                productoId, 
                movimientoDto.Cantidad, 
                movimientoDto.PrecioUnitario, 
                movimientoDto.Motivo
            );

            return Ok("Existencia adicionada exitosamente.");
        }

        // Endpoint para disminuir existencias en el inventario
        [HttpPost("{productoId}/disminuir-existencias")]
        public async Task<IActionResult> DisminuirExistencia([FromRoute] int productoId, [FromBody] AjusteInventarioRequest request)
        {
            try
            {
                await _movimientoServicio.DisminuirExistenciaAsync(productoId, request.Cantidad, request.Motivo);
                return Ok("Existencia disminuida con éxito.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
    // Clase para manejar el ajuste de inventario
    public class AjusteInventarioRequest
    {
        public int Cantidad { get; set; }
        public string Motivo { get; set; }
    }
}
