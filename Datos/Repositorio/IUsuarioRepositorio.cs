namespace GestionInventario.Datos.Repositorio 
{
    public interface IUsuarioRepositorio 
    {
        void CrearUsuario(Usuario usuario);
        void ModificarUsuario(Usuario usuario);
        Usuario? ObtenerUsuario(string correo);
        List<Usuario> ObtenerTodos();
        //void ActivarUsuario(int id);
        //void InactivarUsuario(int id); 
    }
}