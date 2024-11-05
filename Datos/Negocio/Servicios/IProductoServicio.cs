namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IProductoServicio
    {
        void CrearProducto(Producto producto);
        Producto ObtenerProducto(int id);
        List<Producto> ObtenerTodos();
        void ModificarProducto(Producto producto);
        void EliminarProducto(int id);
        void ActualizarInventario(int productoId, int cantidad, string tipoMovimiento, string motivo);
    }
}
