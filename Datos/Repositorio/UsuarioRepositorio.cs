using GestionInventario.Datos;

namespace GestionInventario.Datos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public static readonly List<Usuario> usuarios = new List<Usuario>();

        static UsuarioRepositorio()
        {
            for (int i = 0; i<=3; i++)
            {
                var nombre = GeneradorDatos.NombreAleatorio();
                var apellido = GeneradorDatos.ApellidoAleatorio();
                usuarios.Add(new Usuario
                {
                    Id = i,
                    Nombre = nombre,
                    Apellido = apellido,
                    TipoDocumento = GeneradorDatos.TipoAleatorio(),
                    NumeroDocumento = GeneradorDatos.DocumentoAleatorio(),
                    Direccion = GeneradorDatos.DireccionAleatoria(),
                    Telefono = GeneradorDatos.TelefonoAleatorio(),
                    Email = GeneradorDatos.EmailAleatorio(nombre, apellido),
                    Contraseña = GeneradorDatos.ContraseñaAleatorio(),
                    EstadoActivo = true
                });

            }
        }

        public void CrearUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public void ModificarUsuario(Usuario usuario)
        {
            var usuarioExistente = usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Apellido = usuario.Apellido;
                usuarioExistente.TipoDocumento = usuario.TipoDocumento;
                usuarioExistente.NumeroDocumento = usuario.NumeroDocumento;
                usuarioExistente.Direccion = usuario.Direccion;
                usuarioExistente.Telefono = usuario.Telefono;
                usuarioExistente.EstadoActivo = usuario.EstadoActivo;
            }
        }
        public Usuario? ObtenerUsuario(string email)
        {
            return usuarios.FirstOrDefault(u => u.Email == email);
        }
        public List<Usuario> ObtenerTodos()
        {
            return usuarios;
        }
        public void ActivarUsuario(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)usuario.EstadoActivo = true;
        }
        
        public void InactivarUsuario(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)usuario.EstadoActivo = false;
        }
        
    }
}