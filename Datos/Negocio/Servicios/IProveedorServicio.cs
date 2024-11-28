namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IProveedorServicio
    {
        void CrearProveedor(Proveedor proveedor);
        Proveedor ObtenerProveedor(int id);
        List<Proveedor> ObtenerTodos();
        void ModificarProveedor(Proveedor proveedor);
        void EliminarProveedor(int id);
    }
}

