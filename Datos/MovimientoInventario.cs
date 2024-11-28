namespace GestionInventario.Datos
{
    public class MovimientoInventario
    {
        public int Id { get; set; }
        public int ProductoId { get; set; } // Vinculacion con el producto afectado
        public Producto? Producto { get; set; } // Relacion con la entidad producto
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string? TipoMovimiento { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal CantidadTotal { get; set; }
        public decimal CostoTotalCompras { get; set; }
        public string? Motivo { get; set; }

        public decimal CostoUnitarioPromedio { get; set; }
    }
}