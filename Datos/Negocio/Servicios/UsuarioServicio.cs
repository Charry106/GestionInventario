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
}