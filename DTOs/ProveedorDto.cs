namespace GestionInventario.DTOS
{


    public class ProveedorDto
    {
        public int Id { get; set; }
        public int Nit { get; set; }
        public string Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; }
    }
}