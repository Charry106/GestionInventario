using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos.Repositorio
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly MyDbContext _context;
        public ProveedorRepositorio(MyDbContext context)
        {
            _context = context;
        }

        public void CrearProveedor(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            _context.SaveChanges();
        }

        public Proveedor? ObtenerProveedor(int id)
        {
            return _context.Proveedores.FirstOrDefault(p => p.Id == id);
        }

        public List<Proveedor> ObtenerTodos()
        {
            return _context.Proveedores.ToList();
        }

        public void ActualizarProveedor(Proveedor proveedor)
        {
            _context.Proveedores.Update(proveedor);
            _context.SaveChanges();
        }

        public void EliminarProveedor(int id)
        {
            var proveedor = ObtenerProveedor(id);
            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
                _context.SaveChanges();
            }
        }
        public async Task<bool> ProveedorExisteAsync(int proveedorId)
        {
            return await _context.Proveedores.AnyAsync(p => p.Id == proveedorId);
        }
    }
}
