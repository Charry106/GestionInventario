using GestionInventario.Datos.Repositorio;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class MovimientoInventarioServicio : IMovimientoInventarioServicio
    {
        private readonly IMovimientoInventarioRepositorio _movimientoRepo;
        private readonly IProductoRepositorio _productoRepo;

        public MovimientoInventarioServicio(IMovimientoInventarioRepositorio movimientoRepo, IProductoRepositorio productoRepo)
        {
            _movimientoRepo = movimientoRepo;
            _productoRepo = productoRepo;
        }

        public async Task AdicionarExistenciaAsync(int productoId, int cantidad, decimal precioUnitario, string motivo)
        {
            var producto = await _productoRepo.ObtenerProductoPorIdAsync(productoId);
            if (producto == null)
                throw new ArgumentException("Producto no encontrado");

            // Obtener todos los movimientos anteriores para el producto
            var movimientos = await _movimientoRepo.ObtenerMovimientosPorProductoAsync(productoId);

            // Variables acumuladoras de cantidad y costo total de compras
            int cantidadTotalExistente = 0;
            decimal costoTotalComprasExistente = 0;

            // Si hay movimientos previos, acumulamos cantidad total y costo total de compras
            foreach (var mov in movimientos)
            {
                if (mov.TipoMovimiento == "Compra" || mov.TipoMovimiento == "Adición")
                {
                    cantidadTotalExistente += mov.Cantidad;
                    costoTotalComprasExistente += mov.Cantidad * mov.PrecioUnitario;
                }
            }

            cantidadTotalExistente += cantidad;
            costoTotalComprasExistente += cantidad * precioUnitario;

            // Calcular el costo unitario promedio con los valores actualizados
            decimal costoUnitarioPromedio = cantidadTotalExistente > 0 ? costoTotalComprasExistente / cantidadTotalExistente : precioUnitario;

            // Crear el nuevo movimiento de adición con los valores calculados
            var movimiento = new MovimientoInventario
            {
                ProductoId = productoId,
                TipoMovimiento = "Adicion",
                Cantidad = cantidad,
                PrecioUnitario = precioUnitario,               // Precio unitario ingresado para este movimiento
                CostoTotal = cantidad * precioUnitario,        // Costo total del movimiento actual
                CostoUnitarioPromedio = costoUnitarioPromedio, // Promedio calculado
                CantidadTotal = cantidadTotalExistente,            // Cantidad total acumulada
                CostoTotalCompras = costoTotalComprasExistente,    // Costo total acumulado de compras
                Motivo = motivo,
                Fecha = DateTime.Now
            };
            // Registrar el movimiento en la base de datos
            await _movimientoRepo.RegistrarMovimientoAsync(movimiento);           
        }
        public async Task DisminuirExistenciaAsync(int productoId, int cantidad, string motivo)
        {
            var producto = await _productoRepo.ObtenerProductoPorIdAsync(productoId);
            if (producto == null)
                throw new ArgumentException("Producto no encontrado");

            var movimientos = await _movimientoRepo.ObtenerMovimientosPorProductoAsync(productoId);

            // Verificación de la cantidad disponible en inventario (solo las compras y adiciones)
            int cantidadDisponible = movimientos
                .Where(m => m.TipoMovimiento == "Compra" || m.TipoMovimiento == "Adición")
                .Sum(m => m.Cantidad);

            if (cantidadDisponible < cantidad)
                throw new InvalidOperationException("Cantidad insuficiente en inventario");

            // Calcular el precio unitario promedio utilizando las compras y adiciones
            decimal precioUnitarioPromedio = movimientos
                .Where(m => m.TipoMovimiento == "Compra" || m.TipoMovimiento == "Adición")
                .Sum(m => m.Cantidad * m.PrecioUnitario)
                / movimientos
                    .Where(m => m.TipoMovimiento == "Compra" || m.TipoMovimiento == "Adición")
                    .Sum(m => m.Cantidad);

            // Crear el movimiento de disminución, el costo total será cero ya que es una salida
            var movimiento = new MovimientoInventario
            {
                ProductoId = productoId,
                TipoMovimiento = "Disminucion",
                Cantidad = cantidad,
                PrecioUnitario = precioUnitarioPromedio,
                CostoTotal = cantidad * precioUnitarioPromedio, // Costo total de la disminución puede ser 0
                CostoUnitarioPromedio = precioUnitarioPromedio,
                Motivo = motivo,
                Fecha = DateTime.Now
            };

            // Registrar el movimiento en la base de datos
            await _movimientoRepo.RegistrarMovimientoAsync(movimiento);
        }
    }
}
