namespace GestionInventario.Datos
{
public class Producto
{
    public int Id { get; set; }
    public string CodigoProducto { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? Categoria { get; set; }
    public string? Marca { get; set; }
    public DateTime? FechaExpiracion { get; set; }
    public int ProveedorId { get; set; } // Vinculacion con el proveedor
    public Proveedor Proveedor { get; set; } // Relacion con la entidad Proveedor

    public ICollection<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();

}

}