namespace WS_Fonosoft.Dto.Paciente
{
    public class RspPacienteGet
    {
        public int IdPaciente { get; set; }
        public string  Apellido { get; set; }
        public string Nombre { get; set; }
        public string  TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
    }
}
