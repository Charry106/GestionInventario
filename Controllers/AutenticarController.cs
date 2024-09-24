using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos.Repositorio;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.Controllers
{
    
    [Route("api/autenticacion")]
    [ApiController]
    public class AutenticarController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public AutenticarController(IUsuarioServicio usuarioServicio, IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioServicio = usuarioServicio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost("Validar")]

        public IActionResult ValidarUsuario([FromBody] LoginRequest request)
        {
            bool autenticado = _usuarioServicio.ValidarUsuario(request.Email, request.Password);
            
            if (autenticado)
            {
                return Ok(new
                {
                    AutenticacionExitosa = false,
                    Jwt = string.Empty,
                    Mensaje = "Error al autenticar el usuario"
                });
            }

            var usuario = _usuarioRepositorio.ObtenerUsuario(request.Email);

            var token = Guid.NewGuid().ToString();
            return Ok(new
            {
                AutenticacionExitosa = true,
                Jwt = token,
                Mensaje = $"Bienvenido {usuario.Nombre}{usuario.Apellido}"
            });
        }
    }
        
}
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set;}
        //public string Nombre { get; set; }
        //public string Apellido { get; set; }
    }
