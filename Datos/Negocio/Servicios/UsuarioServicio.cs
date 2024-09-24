using GestionInventario.Datos.Repositorio;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        
        public bool ValidarUsuario(string Email, string Password)
        {
            var usuario = _usuarioRepositorio.ObtenerUsuario(Email);

            if (usuario != null && usuario.Contraseña == Password)
            {
                return true;
            }

            return false;
        } 
    }
}