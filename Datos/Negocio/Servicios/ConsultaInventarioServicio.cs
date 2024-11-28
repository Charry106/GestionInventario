/*using GestionInventario.DTOS;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ConsultaInventarioServicio
    {
        private readonly MyDbContext _context;

        public ConsultaInventarioServicio(MyDbContext context)
        {
            _context = context;
        }

        // Consulta todo el inventario, calculando la cantidad disponible de cada producto
        public async Task<List<ProductoInventarioDto>> ObtenerInventarioCompletoAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.Movimientos)
                .Select(p => new ProductoInventarioDto
                {
                    Nombre = p.Nombre,
                    Categoria = p.Categoria,
                    PrecioUnitario = CalcularCostoUnitarioPromedio(p.Movimientos),
                    CantidadDisponible = CalcularCantidadDisponible(p.Movimientos)
                })
                .ToListAsync();
            return productos;
        }

        // Consulta el inventario filtrando por Nombre o Categoria
        public async Task<List<ProductoInventarioDto>> ConsultarPorCriterioAsync(string nombre, string categoria)
        {
            var productos = await _context.Productos
                .Include(p => p.Movimientos)
                .Where(p => (string.IsNullOrEmpty(nombre) || p.Nombre.Contains(nombre))
                && (string.IsNullOrEmpty(categoria) || p.Categoria.Contains(categoria)))
                .Select(p => new ProductoInventarioDto
                {
                    Nombre = p.Nombre,
                    Categoria = p.Categoria,
                    PrecioUnitario = CalcularCostoUnitarioPromedio(p.Movimientos),
                    CantidadDisponible = CalcularCantidadDisponible(p.Movimientos)
                })
                .ToListAsync();
            
            return productos;
        }

        // Calcula la cantidad disponible sumando entradas y restando salidas
        private int CalcularCantidadDisponible(IEnumerable<MovimientoInventario> movimientos)
        {
            int CantidadDisponible = movimientos.Sum(m => m.TipoMovimiento == "Compra" ? m.Cantidad : -m.Cantidad);
            return CantidadDisponible;
        }

        private decimal CalcularCostoUnitarioPromedio(IEnumerable<MovimientoInventario> movimientos)
        {
            var compras = movimientos.Where(m => m.TipoMovimiento == "Compra");
            int cantidadTotalComprada = compras.Sum(m => m.Cantidad);

            if (cantidadTotalComprada == 0)
                return 0;

            decimal costoTotalComprado = compras.Sum(m => m.Cantidad * m.PrecioUnitario);
            return costoTotalComprado / cantidadTotalComprada;
        }
    }
}*/
using GestionInventario.DTOS;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ConsultaInventarioServicio
    {
        private readonly MyDbContext _context;

        public ConsultaInventarioServicio(MyDbContext context)
        {
            _context = context;
        }

        // Consulta todo el inventario, calculando la cantidad disponible de cada producto
        public async Task<List<ProductoInventarioDto>> ObtenerInventarioCompletoAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.Movimientos)
                .Select(p => new ProductoInventarioDto
                {
                    Nombre = p.Nombre,
                    Categoria = p.Categoria,
                    PrecioUnitario = CalcularCostoUnitarioPromedioStatic(p.Movimientos),
                    CantidadDisponible = CalcularCantidadDisponibleStatic(p.Movimientos)
                })
                .ToListAsync();

            return productos;
        }

        // Consulta el inventario filtrando por Nombre o Categoria
        public async Task<List<ProductoInventarioDto>> ConsultarPorCriterioAsync(string nombre, string categoria)
        {
            var productos = await _context.Productos
                .Include(p => p.Movimientos)
                .Where(p => (string.IsNullOrEmpty(nombre) || p.Nombre.Contains(nombre))
                && (string.IsNullOrEmpty(categoria) || p.Categoria.Contains(categoria)))
                .Select(p => new ProductoInventarioDto
                {
                    Nombre = p.Nombre,
                    Categoria = p.Categoria,
                    PrecioUnitario = CalcularCostoUnitarioPromedioStatic(p.Movimientos),
                    CantidadDisponible = CalcularCantidadDisponibleStatic(p.Movimientos)
                })
                .ToListAsync();

            return productos;
        }

        // Método estático para calcular la cantidad disponible sumando entradas y restando salidas
        private static int CalcularCantidadDisponibleStatic(IEnumerable<MovimientoInventario> movimientos)
        {
            int CantidadDisponible = movimientos.Sum(m => m.TipoMovimiento == "Compra" || m.TipoMovimiento == "Adicion" ? m.Cantidad : -m.Cantidad);
            return CantidadDisponible;
        }

        // Método estático para calcular el costo unitario promedio
        private static decimal CalcularCostoUnitarioPromedioStatic(IEnumerable<MovimientoInventario> movimientos)
        {
            var compras = movimientos.Where(m => m.TipoMovimiento == "Compra" || m.TipoMovimiento == "Adicion");
            int cantidadTotalComprada = compras.Sum(m => m.Cantidad);

            if (cantidadTotalComprada == 0)
                return 0;

            decimal costoTotalComprado = compras.Sum(m => m.Cantidad * m.PrecioUnitario);
            return costoTotalComprado / cantidadTotalComprada;
        }
    }
}
