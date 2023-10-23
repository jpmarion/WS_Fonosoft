namespace WS_Fonosoft.Src.Entidades.Dominio.Interface
{
    public interface IEntidadTipoDocumento
    {
        int IdEntidad { get; set; }
        int IdTipoDocumento { get; set; }
        string TipoDocumento { get; set; }
        string NroDocumento { get; set; }
    }
}
