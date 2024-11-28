
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionInventario.Datos.Repositorio
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly MyDbContext _context;

        public ProductoRepositorio(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProveedorExisteAsync(int proveedorId)
        {
            return await _context.Proveedores.AnyAsync(p => p.Id == proveedorId);
        }

        public async Task<List<Producto>> ObtenerProductoPorProveedorIdAsync(int proveedorId)
        {
            return await _context.Productos
                .Where(p => p.ProveedorId == proveedorId)
                .ToListAsync();
        }

        public async Task CrearProductoAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarProductoAsync(int id)
        {
            var producto = await ObtenerProductoPorIdAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> ObtenerProductoAsync(int id) // Implementar este método
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
