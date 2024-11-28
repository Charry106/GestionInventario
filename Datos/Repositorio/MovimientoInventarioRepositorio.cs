
using Microsoft.EntityFrameworkCore;
using GestionInventario.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionInventario.Datos.Repositorio
{
    public class MovimientoInventarioRepositorio : IMovimientoInventarioRepositorio
    {
        private readonly MyDbContext _context;

        public MovimientoInventarioRepositorio(MyDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarMovimientoAsync(MovimientoInventario movimiento)
        {
            // Obtenemos todos los movimientos previos del producto
            var movimientos = await ObtenerMovimientosPorProductoAsync(movimiento.ProductoId);

            // Inicializamos los acumuladores de cantidad total y costo total de compras
            int cantidadTotal = 0;
            decimal costoTotalCompras = 0;

            // Acumular la cantidad total y el costo total de compras de todos los movimientos anteriores
            foreach (var mov in movimientos)
            {
                if (mov.TipoMovimiento == "Compra" || mov.TipoMovimiento == "Adición")
                {
                    cantidadTotal += mov.Cantidad;
                    costoTotalCompras += mov.Cantidad * mov.PrecioUnitario;
                }
            }

            // Primero actualizamos los valores acumulados de este movimiento
            if (movimiento.TipoMovimiento == "Compra" || movimiento.TipoMovimiento == "Adicion")
            {
                cantidadTotal += movimiento.Cantidad;  // Nueva cantidad total incluyendo el movimiento actual
                costoTotalCompras += movimiento.Cantidad * movimiento.PrecioUnitario; // Nuevo costo total de compras
            }
            else if (movimiento.TipoMovimiento == "Disminucion")
            {
                movimiento.CostoTotal = movimiento.Cantidad * movimiento.PrecioUnitario;
            }

            
            // Ahora, calculamos el costo unitario promedio basado en la cantidad total y el costo total acumulado actualizados
            decimal costoUnitarioPromedio = cantidadTotal > 0 ? costoTotalCompras / cantidadTotal : movimiento.PrecioUnitario;

            // Asignamos los valores calculados al movimiento actual
            movimiento.CostoTotal = movimiento.Cantidad * movimiento.PrecioUnitario; // Costo total del movimiento actual
            movimiento.CostoUnitarioPromedio = costoUnitarioPromedio;                // Promedio calculado después de actualizar acumulados
            movimiento.CantidadTotal = cantidadTotal;                                // Cantidad total acumulada hasta este movimiento
            movimiento.CostoTotalCompras = costoTotalCompras;                        // Costo total acumulado de compras hasta este movimiento

            // Guardamos el movimiento en la base de datos
            await _context.MovimientosInventario.AddAsync(movimiento);
            await _context.SaveChangesAsync();
        }
        public async Task<List<MovimientoInventario>> ObtenerMovimientosPorProductoAsync(int productoId)
        {
            return await _context.MovimientosInventario
                        .Where(m => m.ProductoId == productoId)
                        .Select(m => new MovimientoInventario
                        {
                            ProductoId = m.ProductoId,
                            TipoMovimiento = m.TipoMovimiento,
                            Cantidad = m.Cantidad,
                            PrecioUnitario = m.PrecioUnitario,
                            CostoUnitarioPromedio = m.CostoUnitarioPromedio,
                            CostoTotal = m.CostoTotal,
                            CantidadTotal = m.CantidadTotal,
                            CostoTotalCompras = m.CostoTotalCompras,
                            Fecha = m.Fecha
                        })
                         .ToListAsync();
        }
    }
}
