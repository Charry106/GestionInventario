using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IProductoServicio
    {
        Task CrearProductoAsync(Producto producto);
        Task<Producto?> ObtenerProductoAsync(int id);
        Task<List<Producto>> ObtenerTodosAsync();
        Task ActualizarProductoAsync(Producto producto);
        Task EliminarProductoAsync(int id);
    }
}
