using GestionInventario.Datos;
using GestionInventario.Datos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("email/{email}")]
        public ActionResult<Usuario> GetUsuarioByEmail(string email)
        {
            var usuario = _usuarioRepositorio.ObtenerUsuario(email);
            if (usuario == null) return Ok(usuario);
            return NotFound("Usuario no encontrado");
        }
              
        [HttpGet]
        public ActionResult<List<Usuario>> GetAllUsuarios()
        {
           return Ok(_usuarioRepositorio.ObtenerTodos());
        }

        [HttpPost]
        public ActionResult CrearUsuario([FromBody]Usuario usuario)
        {
            _usuarioRepositorio.CrearUsuario(usuario);
            return Ok("Usuario creado exitosamente");
        }

        [HttpPut]
        public ActionResult ModificarUsuario([FromBody] Usuario usuario)
        {
           _usuarioRepositorio.ModificarUsuario(usuario);
           return Ok("Usuario modificado exitosamente");
        }

        [HttpPut("activar/{id}")]
        public ActionResult ActivarUsuario(int id)
        {
            _usuarioRepositorio.ActivarUsuario(id);
            return Ok("Usuario activado exitosamente");
        }

        [HttpPut("inactivar/{id}")]
        public ActionResult InactivarUsuario(int id)
        {
            _usuarioRepositorio.InactivarUsuario(id);
            return Ok("Usuario inactivado exitosamente");
        }
    }
}