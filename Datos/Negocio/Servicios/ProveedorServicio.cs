using GestionInventario.Datos.Repositorio;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ProveedorServicio
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;
        public ProveedorServicio(IProveedorRepositorio proveedorRepositorio)
        {
            _proveedorRepositorio = proveedorRepositorio;
        }
        public void CrearProveedor(Proveedor proveedor)
        {
            _proveedorRepositorio.CrearProveedor(proveedor);
        }
        public Proveedor? ObtenerProveedor(int id)
        {
            return _proveedorRepositorio.ObtenerProveedor(id);
        }
        public List<Proveedor> ObtenerTodos()
        {
            return _proveedorRepositorio.ObtenerTodos();
        }
        public void ActualizarProveedor(Proveedor proveedor)
        {
            _proveedorRepositorio.ActualizarProveedor(proveedor);
        }
        public void EliminarProveedor(int id)
        {
            _proveedorRepositorio.EliminarProveedor(id);
        }
    }
}
