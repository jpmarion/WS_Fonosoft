namespace WS_Fonosoft.Src.Auth.Dominio.Interface
{
    public interface IUsuario
    {
        public int Id { get; set; }
        string Email { get; set; }
        string NombreUsuario { get; set; }
        string Password { get; set; }
        DateTime FechaCreacion { get; set; }
        bool Confirmacion { get; set; }
    }
}
