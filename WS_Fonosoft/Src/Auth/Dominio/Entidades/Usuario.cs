using WS_Fonosoft.Src.Auth.Dominio.Interface;

namespace WS_Fonosoft.Src.Auth.Dominio.Entidades
{
    public class Usuario : IUsuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Confirmacion { get; set; }
    }
}
