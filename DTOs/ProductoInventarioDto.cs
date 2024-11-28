namespace GestionInventario.DTOS
{
    public class ProductoInventarioDto
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int CantidadDisponible { get; set; }
    }
}