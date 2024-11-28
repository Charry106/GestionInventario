using System.ComponentModel.DataAnnotations;

namespace GestionInventario.DTOS
{
    public class MovimientoInventarioDto
    {
        [Required]
        public string TipoMovimiento { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe de ser mayor a cero")]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public string Motivo { get; set; }
    }
}