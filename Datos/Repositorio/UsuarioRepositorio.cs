using GestionInventario.Datos;
using GestionInventario.Datos.Negocio.Servicios;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        
        private readonly MyDbContext _context;
        
        public UsuarioRepositorio(MyDbContext context)
        {
            _context = context;
        }

        public void CrearUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void ModificarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public async Task<Usuario?> ObtenerUsuario(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
        public List<Usuario> ObtenerTodos()
        {
            return _context.Usuarios.ToList();
        }

        public void ActivarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                usuario.EstadoActivo = true;
                _context.SaveChanges();
            }
        }

        public Usuario ObtenerPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public void InactivarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                usuario.EstadoActivo = false;
                _context.SaveChanges();
            }
        }
    }
}
