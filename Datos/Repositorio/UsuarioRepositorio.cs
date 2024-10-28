using GestionInventario.Datos;
using GestionInventario.Datos.Negocio.Servicios;

namespace GestionInventario.Datos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        
        private static List<Usuario> usuarios;
        
        static UsuarioRepositorio()
        {
            usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    TipoDocumento = "CC",
                    NumeroDocumento = "123456789",
                    Direccion = "Calle 1 # 1-1",
                    Telefono = "1234567",
                    Email = "juan.perez@correo.com",
                    Contraseña = "123456",
                    EstadoActivo = true
                },

                new Usuario
                {
                    Id = 2,
                    Nombre = "Maria",
                    Apellido = "Rodriguez",
                    TipoDocumento = "CC",
                    NumeroDocumento = "987654321",
                    Direccion = "Calle 2 # 2-2",
                    Telefono = "7654321",
                    Email = "maria.rodriguez@correo.com",
                    Contraseña = "987654",
                    EstadoActivo = true
                }
            };
        }

        public void CrearUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public void ModificarUsuario(Usuario usuarioModificado)
        {
            var usuarioExistente = usuarios.FirstOrDefault(u => u.Id == usuarioModificado.Id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuarioModificado.Nombre;
                usuarioExistente.Apellido = usuarioModificado.Apellido;
                usuarioExistente.TipoDocumento = usuarioModificado.TipoDocumento;
                usuarioExistente.NumeroDocumento = usuarioModificado.NumeroDocumento;
                usuarioExistente.Direccion = usuarioModificado.Direccion;
                usuarioExistente.Telefono = usuarioModificado.Telefono;
                usuarioExistente.EstadoActivo = usuarioModificado.EstadoActivo;
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
        /*
        public void ActivarUsuario(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)usuario.EstadoActivo = true;
        }
        
        public void InactivarUsuario(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)usuario.EstadoActivo = false;
        }*/
        
    }
}