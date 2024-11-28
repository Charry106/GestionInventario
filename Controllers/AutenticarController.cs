using GestionInventario.Datos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AutenticarController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyDbContext _myDbContext;

        public AutenticarController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] RegistroRequest registroRequest)
        {
            
          var user = new ApplicationUser
          {
            UserName = registroRequest.Email,
            Email = registroRequest.Email,
            Nombre = registroRequest.Nombre,
            Apellido = registroRequest.Apellido
          };
          var result = await _userManager.CreateAsync(user, registroRequest.Password);
          if (result.Succeeded)
          {
            var nuevoUsuario = new Usuario
            {
                Email = registroRequest.Email,
                Nombre = registroRequest.Nombre,
                Apellido = registroRequest.Apellido,
                
                EstadoActivo = true 
            };

            _myDbContext.Usuarios.Add(nuevoUsuario);
            await _myDbContext.SaveChangesAsync();
            return Ok("Usuario registrado exitosamente");
          }
          return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                var token = Guid.NewGuid().ToString(); // Token simulado para ejemplo

                return Ok(new
                {
                    AutenticacionExitosa = true,
                    Jwt = token,
                    Mensaje = $"Bienvenido {user.Nombre} {user.Apellido}"
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

    public class RegistroRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
      

    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
