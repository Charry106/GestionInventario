using System.Threading.Tasks;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IMovimientoInventarioServicio
    {
        Task AdicionarExistenciaAsync(int productoId, int cantidad, decimal precioUnitario, string motivo); 
        Task DisminuirExistenciaAsync(int productoId, int cantidad, string motivo); 
    }
}
