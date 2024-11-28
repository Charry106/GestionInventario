namespace GestionInventario.Datos
{
    public class Proveedor
    {
        public int Id { get; set; }
        public int Nit { get; set; }
        public string Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; }

        public ICollection<Producto> Productos { get; set; }

    }
}