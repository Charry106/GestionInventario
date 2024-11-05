using GestionInventario.Datos;
using GestionInventario.Datos.Repositorio;
using System.Collections.Generic;
using System.Linq;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IMovimientoServicio _movimientoServicio;

        public ProductoServicio(IProductoRepositorio productoRepositorio, IMovimientoServicio movimientoServicio)
        {
            _productoRepositorio = productoRepositorio;
            _movimientoServicio = movimientoServicio;
        }

        public void CrearProducto(Producto producto) => _productoRepositorio.CrearProducto(producto);
        
        public Producto ObtenerProducto(int id) => _productoRepositorio.ObtenerProducto(id);
        
        public List<Producto> ObtenerTodos() => _productoRepositorio.ObtenerTodos();
        
        public void ModificarProducto(Producto producto) => _productoRepositorio.ModificarProducto(producto);
        
        public void EliminarProducto(int id) => _productoRepositorio.EliminarProducto(id);
        
        public void ActualizarInventario(int productoId, int cantidad, string tipoMovimiento, string motivo)
        {
            var producto = ObtenerProducto(productoId);
            if (producto == null) throw new Exception("Producto no encontrado");

            if (tipoMovimiento == "entrada") producto.Cantidad += cantidad;
            else if (tipoMovimiento == "salida") producto.Cantidad -= cantidad;

            _movimientoServicio.RegistrarMovimiento(productoId, tipoMovimiento, cantidad, motivo);
        }
    }
}
