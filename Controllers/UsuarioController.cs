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

        [HttpGet("Buscar Usuario por email")]
        public ActionResult<Usuario> GetUsuarioByEmail(string email)
        {
            var usuario = _usuarioRepositorio.ObtenerUsuario(email);

            if (usuario == null) return NotFound("Usuario no encontrado");
            return Ok(usuario);
        }
              
        [HttpGet("Obtener todos los usuarios")]
        public ActionResult<List<Usuario>> GetAllUsuarios()
        {
           return Ok(_usuarioRepositorio.ObtenerTodos());
        }

        [HttpPost("Crear Usuario")]
        public ActionResult CrearUsuario([FromBody]Usuario usuario)
        {
            _usuarioRepositorio.CrearUsuario(usuario);
            return Ok("Usuario creado exitosamente");
        }

        [HttpPut("Modificar Usuario")]
        public ActionResult ModificarUsuario(string email,[FromBody]Usuario usuarioActualizado)
        {
            var usuarioExistente = _usuarioRepositorio.ObtenerUsuario(email);

            if (usuarioExistente == null) 
            {
                return NotFound("Usuario no encontrado");
            }

            usuarioExistente.Nombre = usuarioActualizado.Nombre;
            usuarioExistente.Apellido = usuarioActualizado.Apellido;
            usuarioExistente.TipoDocumento = usuarioActualizado.TipoDocumento;
            usuarioExistente.NumeroDocumento = usuarioActualizado.NumeroDocumento;
            usuarioExistente.Direccion = usuarioActualizado.Direccion;
            usuarioExistente.Telefono = usuarioActualizado.Telefono;

            usuarioExistente.EstadoActivo = usuarioActualizado.EstadoActivo;

            _usuarioRepositorio.ModificarUsuario(usuarioExistente);

            return Ok("Usuario modificado exitosamente");

        }
/*
        [HttpPut("Modificar Usuario")]
        public ActionResult ModificarUsuario([FromBody] Usuario usuario)
        {
           _usuarioRepositorio.ModificarUsuario(usuario);
           return Ok("Usuario modificado exitosamente");
        }

        [HttpPut("Activar")]
        public ActionResult ActivarUsuario(int id)
        {
            _usuarioRepositorio.ActivarUsuario(id);
            return Ok("Usuario activado exitosamente");
        }

        [HttpPut("Inactivar")]
        public ActionResult InactivarUsuario(int id)
        {
            _usuarioRepositorio.InactivarUsuario(id);
            return Ok("Usuario inactivado exitosamente");
        }*/
    }
}