using GestionInventario.Datos;
using GestionInventario.Datos.Repositorio;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventario.Controllers

{
    [ApiController]
    [Route("api/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("ObtenerTodos")]
        public ActionResult<List<Usuario>> GetAllUsuarios()
        {
            return Ok(_usuarioRepositorio.ObtenerTodos());
        }

        [HttpPost("Crear Usuario")]
        public ActionResult CrearUsuario([FromBody] Usuario usuario)
        {
            _usuarioRepositorio.CrearUsuario(usuario);
            return Ok("Usuario creado exitosamente");
        }

        [HttpPut("Modificar Usuario")]
        public ActionResult ModificarUsuario(int id, [FromBody] Usuario usuarioActualizado)
        {
            if (usuarioActualizado == null)
            {
                return BadRequest("Datos de usuario no validos");
            }
            var usuario = _usuarioRepositorio.ObtenerPorId(id);
            if (usuario == null) return NotFound("Usuario no encontrado");

            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.Apellido = usuarioActualizado.Apellido;
            usuario.TipoDocumento = usuarioActualizado.TipoDocumento;
            usuario.NumeroDocumento = usuarioActualizado.NumeroDocumento;
            usuario.Direccion = usuarioActualizado.Direccion;
            usuario.Telefono = usuarioActualizado.Telefono;
            usuario.EstadoActivo = usuarioActualizado.EstadoActivo;
            usuario.Email = usuarioActualizado.Email;

            _usuarioRepositorio.ModificarUsuario(usuario);
            return Ok("Usuario modificado exitosamente");
        }

        [HttpGet("Buscar usuario por email")]
        public async Task<ActionResult<Usuario>> GetUsuarioByEmail(string email)
        {
        var usuario = await _usuarioRepositorio.ObtenerUsuario(email);
        if (usuario == null) 
        {
            return NotFound("Usuario no encontrado");
        }
        return Ok(usuario);
        }
    }
}