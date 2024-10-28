using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos.Repositorio;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    
    public class AutenticarController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        //private readonly IUsuarioRepositorio _usuarioRepositorio;
        public AutenticarController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
            //_usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost("Validar")]
        public IActionResult ValidarUsuario([FromBody] LoginRequest loginRequest)
        {
            //bool autenticado = _usuarioServicio.ValidarUsuario(request.Email, request.Password);
            var esValido = _usuarioServicio.ValidarUsuario(loginRequest.Email, loginRequest.Password);
            if (esValido)
            {
                var usuario = _usuarioServicio.ObtenerUsuarioPorEmail(loginRequest.Email);

                var token = Guid.NewGuid().ToString();
                return Ok(new
                {
                    AutenticacionExitosa = true,
                    Jwt = token,
                    Mensaje = $"Bienvenido {usuario.Nombre}{usuario.Apellido}"
                }); 
            }

            return BadRequest(new
            {
                AutenticacionExitosa = false,
                Jwt = string.Empty,
                Mensaje = "Error al autenticar el usuario"
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
