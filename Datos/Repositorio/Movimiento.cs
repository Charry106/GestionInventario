namespace GestionInventario.Datos
{
    public class Movimiento
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } // "entrada" o "salida"
        public int Cantidad { get; set; }
        public string Motivo { get; set; }
    }
}
