using System.Text;
using GestionInventario.Datos.Negocio.Servicios;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace GestionInventario.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly ReporteInventarioServicio _reporteInventarioServicio;

        public InventarioController(ReporteInventarioServicio reporteInventarioServicio)
        {
            _reporteInventarioServicio = reporteInventarioServicio;
        }

        // Endpoint para generar y descargar el reporte en formato CSV
        [HttpGet("reporte-inventario")]
        public async Task<IActionResult> DescargarReporteInventario([FromQuery] int? productoId)
        {
            try
            {
                // Generar el reporte en formato CSV
                var reporteCsv = await _reporteInventarioServicio.GenerarReporteInventarioCsvAsync(productoId);

                // Devolver el archivo CSV como respuesta
                var bytes = Encoding.UTF8.GetBytes(reporteCsv);
                return File(bytes, "text/csv", "reporte_inventario.csv");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al generar el reporte: {ex.Message}");
            }
        }
    }
}