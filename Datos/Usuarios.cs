using System.Text.Json.Serialization;
namespace GestionInventario.Datos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool EstadoActivo { get; set; }
        //public string Contraseña { get; set; }
        public string Email { get; set; }
    }
}
