using GestionInventario.Datos.Repositorio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductoServicio(IProductoRepositorio productoRepositorio)
        {
            this._productoRepositorio = productoRepositorio;
        }

        public async Task CrearProductoAsync(Producto producto) 
        {
            // Verificar si el proveedor existe de manera asíncrona
            if (!await _productoRepositorio.ProveedorExisteAsync(producto.ProveedorId))
            {
                throw new ArgumentException("El proveedor con el ID especificado no existe");
            }
            await _productoRepositorio.CrearProductoAsync(producto); 
        }

        public async Task<Producto?> ObtenerProductoAsync(int id) 
        {
            return await _productoRepositorio.ObtenerProductoPorIdAsync(id); 
        }

        public async Task<List<Producto>> ObtenerTodosAsync() 
        {
            return await _productoRepositorio.ObtenerTodosAsync(); 
        }

        public async Task ActualizarProductoAsync(Producto producto) 
        {
            await _productoRepositorio.ActualizarProductoAsync(producto); 
        }

        public async Task EliminarProductoAsync(int id) 
        {
            await _productoRepositorio.EliminarProductoAsync(id);
        }
    }
}
