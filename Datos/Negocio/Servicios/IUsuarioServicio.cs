namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IUsuarioServicio
    {
        bool ValidarUsuario(string email, string password);
        Usuario ObtenerUsuarioPorEmail(string email);
    }
}