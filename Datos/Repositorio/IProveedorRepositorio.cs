namespace GestionInventario.Datos.Repositorio
{
    public interface IProveedorRepositorio
    {
        Task<bool> ProveedorExisteAsync(int proveedorId);
        void CrearProveedor(Proveedor proveedor);
        Proveedor? ObtenerProveedor(int id);
        List<Proveedor> ObtenerTodos();
        void ActualizarProveedor(Proveedor proveedor);
        void EliminarProveedor(int id);

    }
}
