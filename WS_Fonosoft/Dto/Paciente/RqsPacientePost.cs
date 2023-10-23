namespace WS_Fonosoft.Dto.Paciente
{
    public class RqsPacientePost
    {
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public List<ObraSocial> ObrasSociales { get; set; }
        public List<Contacto> Contactos { get; set; }
        public List<Documento> Documentos { get; set; }
    }
}
