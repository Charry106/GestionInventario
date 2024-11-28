namespace GestionInventario.Datos
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int ProveedorId { get; set; }

        // Nueva propiedad para la cantidad disponible
        public int CantidadDisponible { get; set; } 
    }
}
