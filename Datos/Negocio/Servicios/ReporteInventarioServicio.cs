using GestionInventario.Datos.Repositorio;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ReporteInventarioServicio
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IMovimientoInventarioRepositorio _movimientoRepositorio;

        public ReporteInventarioServicio(IProductoRepositorio productoRepositorio, IMovimientoInventarioRepositorio movimientoInventarioRepositorio)
        {
            _productoRepositorio = productoRepositorio;
            _movimientoRepositorio = movimientoInventarioRepositorio;
        }

        public async Task<string> GenerarReporteInventarioCsvAsync(int? productoId = null)
        {
            List<Producto> productos;

            if (productoId.HasValue)
            {
                // Obtener un solo producto si se pasa un id
                var producto = await _productoRepositorio.ObtenerProductoAsync(productoId.Value);
                productos = new List<Producto> { producto };
            }
            else
            {
                // Obtener todos los productos si no se pasa un id
                productos = await _productoRepositorio.ObtenerTodosAsync();
            }

            if (productos == null || productos.Count == 0)
                throw new InvalidOperationException("No se encontraron productos.");

            var reporte = new List<string>
            {
                "Producto,CantidadTotal,CostoPromedio,CostoTotal" // Encabezado CSV
            };

            foreach (var producto in productos)
            {
                var movimientos = await _movimientoRepositorio.ObtenerMovimientosPorProductoAsync(producto.Id);

                int cantidadTotal = 0;
                decimal costoTotalCompras = 0;

                foreach (var mov in movimientos)
                {
                    if (mov.TipoMovimiento == "Compra" || mov.TipoMovimiento == "Adición")
                    {
                        cantidadTotal += mov.Cantidad;
                        costoTotalCompras += mov.Cantidad * mov.PrecioUnitario;
                    }
                }

                decimal costoPromedio = cantidadTotal > 0 ? costoTotalCompras / cantidadTotal : 0;

                string lineaReporte = $"{producto.Nombre},{cantidadTotal},{costoPromedio.ToString("F2")},{costoTotalCompras}";
                reporte.Add(lineaReporte);
            }

            return string.Join(Environment.NewLine, reporte);
        }
/*
        public async Task<string> GenerarReporteInventarioCsvAsync(int? productoId = null)
        {
            var productos = productoId.HasValue
                ? new List<Producto> { await _productoRepositorio.ObtenerProductoPorIdAsync(productoId.Value)}
                : await _productoRepositorio.ObtenerProductoAsync();

            if (productos == null || productos.Count == 0)
                throw new InvalidOperationException("No se encontraron productos");

            var reporte = new List<string>
            {
                "Producto,CantidadTotal,CostoPromedio,CostoTotal" // Encabezado CSV
            };

            // Recorremos los productos y generamos los valores del CSV
            foreach (var producto in productos)
            {
                var movimientos = await _movimientoRepositorio.ObtenerMovimientosPorProductoAsync(producto.Id);

                // Acumulamos los datos para cada producto
                int cantidadTotal = 0;
                decimal costoTotalCompras = 0;

                foreach (var mov in movimientos)
                {
                    if (mov.TipoMovimiento == "Compra" || mov.TipoMovimiento == "Adición")
                    {
                        cantidadTotal += mov.Cantidad;
                        costoTotalCompras += mov.Cantidad * mov.PrecioUnitario;
                    }
                }

                // Calcular el costo promedio
                decimal costoPromedio = cantidadTotal > 0 ? costoTotalCompras / cantidadTotal : 0;

                // Crear la línea de reporte para este producto
                string lineaReporte = $"{producto.Nombre},{cantidadTotal},{costoPromedio},{costoTotalCompras}";
                reporte.Add(lineaReporte);
            }

            // Convertir la lista de líneas a un solo string con saltos de línea
            return string.Join(Environment.NewLine, reporte);
        }*/
    }
}