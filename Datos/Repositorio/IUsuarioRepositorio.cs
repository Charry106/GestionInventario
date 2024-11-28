namespace GestionInventario.Datos.Repositorio 
{
    public interface IUsuarioRepositorio 
    {
        void CrearUsuario(Usuario usuario);
        void ModificarUsuario(Usuario usuario);
        Task<Usuario?> ObtenerUsuario(string email);
        List<Usuario> ObtenerTodos();
        Usuario ObtenerPorId(int id);
        void ActivarUsuario(int id);
        void InactivarUsuario(int id); 
    }
}
