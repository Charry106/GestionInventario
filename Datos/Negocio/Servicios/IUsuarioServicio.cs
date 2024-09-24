namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IUsuarioServicio
    {
        bool ValidarUsuario(string Email, string Password);
    }
}