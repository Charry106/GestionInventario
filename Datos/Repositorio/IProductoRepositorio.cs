using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Datos.Repositorio
{
    public interface IProductoRepositorio
    {
        Task<bool> ProveedorExisteAsync(int proveedorId);
        Task<List<Producto>> ObtenerProductoPorProveedorIdAsync(int proveedorId);
        Task CrearProductoAsync(Producto producto);
        Task ActualizarProductoAsync(Producto producto); 
        Task EliminarProductoAsync(int id);
        Task<Producto> ObtenerProductoPorIdAsync(int id);
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto> ObtenerProductoAsync(int id);
        //Task<List<Producto>> ObtenerProductoAsync();
    }
}

