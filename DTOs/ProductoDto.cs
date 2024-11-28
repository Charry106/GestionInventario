namespace GestionInventario.DTOS
{
    public class ProductoDto
    {
    public int Id { get; set; }
    public string CodigoProducto { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? Categoria { get; set; }
    public string? Marca { get; set; }
    public DateTime? FechaExpiracion { get; set; }
    public int ProveedorId { get; set; }
    } 
}