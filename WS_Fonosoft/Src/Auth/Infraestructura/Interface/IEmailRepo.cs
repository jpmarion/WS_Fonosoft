namespace WS_Fonosoft.Src.Auth.Infraestructura.Interface
{
    public interface IEmailRepo
    {
        void EnviarEmail(ICollection<string> EmailDestino, string Asunto, string Cuerpo, bool EsBodyHtml);
    }
}
