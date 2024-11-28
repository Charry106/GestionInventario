using Microsoft.AspNetCore.Identity;

namespace GestionInventario.Datos
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        
    }
}