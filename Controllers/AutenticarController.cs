using Microsoft.AspNetCore.Mvc;
using GestionInventario.Datos.Negocio.Servicios;
using System.Threading.Tasks;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/autenticacion")]
    public class AutenticarController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public AutenticarController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginRequest request)
        {
            bool esValido = await _usuarioServicio.ValidarCredencialesAsync(request.Email, request.Password);
            if (esValido)
            {
                return Ok(new { AutenticacionExitosa = true, Mensaje = "Inicio de sesión exitoso" });
            }
            return Unauthorized("Credenciales incorrectas");
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
