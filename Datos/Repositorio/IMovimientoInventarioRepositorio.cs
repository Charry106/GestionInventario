namespace GestionInventario.Datos.Repositorio
{
    public interface IMovimientoInventarioRepositorio
    {
        Task RegistrarMovimientoAsync(MovimientoInventario movimiento);
        Task<List<MovimientoInventario>> ObtenerMovimientosPorProductoAsync(int productoId);
    }
}