using GestionInventario.Datos.Repositorio;
using Microsoft.AspNetCore.Identity;

namespace GestionInventario.Datos.Negocio.Servicios
{
    /*
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        
        public bool ValidarUsuario(string email, string password)
        {
            var usuario = _usuarioRepositorio.ObtenerUsuario(email);

            if (usuario != null && usuario.Contraseña == password)
            {
                return true;
            }

            return false;
        } 

        public Usuario? ObtenerUsuarioPorEmail(string email)
        {
            return _usuarioRepositorio.ObtenerUsuario(email);
        }
    }
    */
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsuarioServicio(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }        

        public async Task<bool> ValidarUsuario(string email, string password)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null) return false;

            return await _userManager.CheckPasswordAsync(usuario, password);
        }

        public async Task<ApplicationUser?> ObtenerUsuarioPorEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
