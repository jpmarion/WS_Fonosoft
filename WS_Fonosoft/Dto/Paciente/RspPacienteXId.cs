namespace WS_Fonosoft.Dto.Paciente
{
    public class RspPacienteXId
    {
        public int IdPaciente { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public List<EntidadGet> Entidades { get; set; } = new List<EntidadGet>();
        public List<ContactoGet> Contactos { get; set; } = new List<ContactoGet>();
        public List<ObraSocialGet> ObraSociales { get; set; } = new List<ObraSocialGet>();
        public List<DocumentoGet> Documentos { get; set; } = new List<DocumentoGet>();
    }
}
