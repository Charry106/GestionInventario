namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IUsuarioServicio
    {
        Task<bool> ValidarUsuario(string email, string password);
        Task<ApplicationUser?> ObtenerUsuarioPorEmail(string email);
    }
}
