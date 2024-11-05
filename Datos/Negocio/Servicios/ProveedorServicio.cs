using GestionInventario.Datos.Repositorio;
using GestionInventario.Datos;
using System.Collections.Generic;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ProveedorServicio : IProveedorServicio
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;

        public ProveedorServicio(IProveedorRepositorio proveedorRepositorio)
        {
            _proveedorRepositorio = proveedorRepositorio;
        }

        public void CrearProveedor(Proveedor proveedor) => _proveedorRepositorio.CrearProveedor(proveedor);
        
        public Proveedor ObtenerProveedor(int id) => _proveedorRepositorio.ObtenerProveedor(id);
        
        public List<Proveedor> ObtenerTodos() => _proveedorRepositorio.ObtenerTodos();
        
        public void ModificarProveedor(Proveedor proveedor) => _proveedorRepositorio.ModificarProveedor(proveedor);
        
        public void EliminarProveedor(int id) => _proveedorRepositorio.EliminarProveedor(id);
    }
}
