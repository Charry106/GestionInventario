using Microsoft.AspNetCore.Mvc;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos;
using GestionInventario.Datos.DTOs;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Common;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] UserCreateDTO userDto)
        {
            var usuario = new Usuario
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                Nombre = userDto.Nombre,
                Apellido = userDto.Apellido,
                TipoDocumento = userDto.TipoDocumento,
                NumeroDocumento = userDto.NumeroDocumento,
                Direccion = userDto.Direccion,
                Telefono = userDto.Telefono,
                EstadoActivo = userDto.EstadoActivo
            }; 

           var result = await _usuarioServicio.CrearUsuarioAsync(usuario, userDto.Password);
            if (result.Succeeded)
            return Ok("Usuario creado exitosamente");

            return BadRequest(result.Errors);
        }

        [HttpGet("BuscarPorEmail")]
        public async Task<IActionResult> BuscarUsuarioPorEmail(string email)
        {
            var usuario = await _usuarioServicio.ObtenerUsuarioPorEmailAsync(email);
            return usuario == null ? NotFound("Usuario no encontrado") : Ok(usuario);
        }
    }
}