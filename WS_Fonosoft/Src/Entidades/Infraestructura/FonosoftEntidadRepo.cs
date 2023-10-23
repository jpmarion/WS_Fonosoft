using MySql.Data.MySqlClient;
using System.Data;
using WS_Fonosoft.Src.Entidades.Dominio.Agregados;
using WS_Fonosoft.Src.Entidades.Dominio.Entidades;
using WS_Fonosoft.Src.Entidades.Dominio.Factoria;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;
using WS_Fonosoft.Src.Entidades.Infraestructura.Interface;
using System.Linq;

namespace WS_Fonosoft.Src.Entidades.Infraestructura
{
    public class FonosoftEntidadRepo : IRepositorioEntidad
    {
        private readonly string _connectionString;
        private MySqlConnection _conexion;
        MySqlTransaction? _mySqlTransaction = null;

        public FonosoftEntidadRepo(string server, string user, string database, string port, string password)
        {
            _connectionString = $"server={server};user={user};database={database};port={port};password={password}";
            _conexion = new MySqlConnection(_connectionString);
            _conexion.Open();
        }
        private MySqlConnection getConexion()
        {
            return _conexion;
        }
        public void BeginTransaction()
        {
            _mySqlTransaction = _conexion.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _mySqlTransaction.Commit();
            _conexion.Close();
        }
        public void RollbackTransaction()
        {
            _mySqlTransaction.Rollback();
            _conexion.Close();
        }
        public IEntidad AgregarPaciente(IEntidad paciente)
        {
            MySqlCommand cmdPaciente = new MySqlCommand("Entidad_I", getConexion());
            cmdPaciente.CommandType = CommandType.StoredProcedure;

            cmdPaciente.Parameters.AddWithValue("@Apellido", paciente.DatosPersona.Apellido);
            cmdPaciente.Parameters.AddWithValue("@Nombre", paciente.DatosPersona.Nombre);

            MySqlDataAdapter adapterPaciente = new MySqlDataAdapter();
            adapterPaciente.SelectCommand = cmdPaciente;

            DataTable dtPaciente = new DataTable();
            adapterPaciente.Fill(dtPaciente);

            if (dtPaciente.Rows.Count != 0)
            {
                DataRow drPaciente = dtPaciente.Rows[0];
                paciente.Id = int.Parse(drPaciente["Id"].ToString());

                IEntidadTipoEntidad entidadTipoEntidad = new EntidadTipoEntidad();
                entidadTipoEntidad.IdEntidad = paciente.Id;
                entidadTipoEntidad.IdTipoEntidad = (int)TipoEntidad.EnumTipoEntidad.Paciente;
                AgregarEntidadTipoEntidad(entidadTipoEntidad);

                foreach (IEntidadObraSocial item in paciente.ObrasSociales)
                {
                    item.IdEntidad = paciente.Id;
                    AgregarEntidadObraSocial(item);
                }

                foreach (IEntidadTipoContacto item in paciente.Contactos)
                {
                    item.IdEntidad = paciente.Id;
                    AgregarEntidadTipoContacto(item);
                }

                foreach (IEntidadTipoDocumento item in paciente.Documentos)
                {
                    item.IdEntidad = paciente.Id;
                    AgregarEntidadTipoDocumento(item);
                }

                return paciente;
            }
            return null;
        }


        public void AgregarEntidadTipoEntidad(IEntidadTipoEntidad entidadTipoEntidad)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadTipoEntidad_I", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", entidadTipoEntidad.IdEntidad);
            cmd.Parameters.AddWithValue("@IdTipoEntidad", entidadTipoEntidad.IdTipoEntidad);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            cmd.ExecuteNonQuery();
        }
        public void AgregarEntidadObraSocial(IEntidadObraSocial entidadObraSocial)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadObraSocial_I", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", entidadObraSocial.IdEntidad);
            cmd.Parameters.AddWithValue("@IdObraSocial", entidadObraSocial.IdObraSocial);
            cmd.Parameters.AddWithValue("@NroObraSocial", entidadObraSocial.NroObraSocial);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            cmd.ExecuteNonQuery();
        }
        public void AgregarEntidadTipoContacto(IEntidadTipoContacto entidadTipoContacto)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadTipoContacto_I", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", entidadTipoContacto.IdEntidad);
            cmd.Parameters.AddWithValue("@IdTipoContacto", entidadTipoContacto.IdTipoContacto);
            cmd.Parameters.AddWithValue("@Contacto", entidadTipoContacto.Contacto);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            cmd.ExecuteNonQuery();
        }
        public void AgregarEntidadTipoDocumento(IEntidadTipoDocumento entidadTipoDocumento)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadTipoDocumento_I", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", entidadTipoDocumento.IdEntidad);
            cmd.Parameters.AddWithValue("@IdTipoDocumento", entidadTipoDocumento.IdTipoDocumento);
            cmd.Parameters.AddWithValue("@NroDocumento", entidadTipoDocumento.NroDocumento);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            cmd.ExecuteNonQuery();
        }

        public IList<IEntidad> BuscarPacientes()
        {
            MySqlCommand cmd = new MySqlCommand("Entidad_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdTipoEntidad", (int)TipoEntidad.EnumTipoEntidad.Paciente);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtPaciente = new DataTable();
            adapter.Fill(dtPaciente);

            if (dtPaciente.Rows.Count != 0)
            {
                IList<IEntidad> lstPacientes = new List<IEntidad>();
                foreach (DataRow item in dtPaciente.Rows)
                {
                    IEntidad paciente = EntidadFactory.GetEntidad(TipoEntidad.EnumTipoEntidad.Paciente);
                    paciente.Id = item.Field<int>("Id");
                    paciente.DatosPersona.Apellido = item.Field<string>("Apellido");
                    paciente.DatosPersona.Nombre = item.Field<string>("Nombre");
                    IEntidadTipoDocumento entidadTipoDocumento = new EntidadTipoDocumento();
                    entidadTipoDocumento.TipoDocumento = item.Field<string>("TipoDocumento");
                    entidadTipoDocumento.NroDocumento = item.Field<string>("NroDocumento");
                    paciente.Documentos.Add(entidadTipoDocumento);
                    lstPacientes.Add(paciente);
                }
                return lstPacientes;
            }
            return null;
        }
        public IEntidad BuscarPacienteXId(int IdPaciente)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadXId_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", IdPaciente);
            cmd.Parameters.AddWithValue("@IdTipoEntidad", (int)TipoEntidad.EnumTipoEntidad.Paciente);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtPaciente = new DataTable();
            adapter.Fill(dtPaciente);

            if (dtPaciente.Rows.Count != 0)
            {
                DataRow item = dtPaciente.Rows[0];

                IEntidad paciente = EntidadFactory.GetEntidad(TipoEntidad.EnumTipoEntidad.Paciente);
                paciente.Id = item.Field<int>("Id");
                paciente.DatosPersona.Apellido = item.Field<string>("Apellido");
                paciente.DatosPersona.Nombre = item.Field<string>("Nombre");
                paciente.TiposEntidades = BuscarTiposEntidadesXIdEntidad(paciente.Id);
                paciente.ObrasSociales = BuscarObrasSocialesXIdEntidad(paciente.Id);
                paciente.Documentos = BuscarDocumentosXIdEntidad(paciente.Id);
                paciente.Contactos = BuscarContactosXIdEntidad(paciente.Id);

                return paciente;
            }
            return null;
        }
        public IList<IEntidadTipoContacto> BuscarContactosXIdEntidad(int IdEntidad)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadTipoContactoXIdEntidad_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", IdEntidad);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtEntidadContacto = new DataTable();
            adapter.Fill(dtEntidadContacto);

            if (dtEntidadContacto.Rows.Count != 0)
            {
                IList<IEntidadTipoContacto> lstEntidadContacto = new List<IEntidadTipoContacto>();
                foreach (DataRow item in dtEntidadContacto.Rows)
                {
                    IEntidadTipoContacto entidadObraSocial = new EntidadTipoContacto();
                    entidadObraSocial.IdEntidad = item.Field<int>("IdEntidad");
                    entidadObraSocial.IdTipoContacto = item.Field<int>("IdTipoContacto");
                    entidadObraSocial.TipoContacto = item.Field<string>("TipoContacto");
                    entidadObraSocial.Contacto = item.Field<string>("Contacto");

                    lstEntidadContacto.Add(entidadObraSocial);
                }
                return lstEntidadContacto;
            }
            return null;
        }
        public IList<IEntidadTipoDocumento> BuscarDocumentosXIdEntidad(int IdEntidad)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadDocumentoXIdEntidad_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", IdEntidad);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtEntidaDocumentos = new DataTable();
            adapter.Fill(dtEntidaDocumentos);

            if (dtEntidaDocumentos.Rows.Count != 0)
            {
                IList<IEntidadTipoDocumento> lstEntidaDocumento = new List<IEntidadTipoDocumento>();
                foreach (DataRow item in dtEntidaDocumentos.Rows)
                {
                    IEntidadTipoDocumento entidadDocumento = new EntidadTipoDocumento();
                    entidadDocumento.IdEntidad = item.Field<int>("IdEntidad");
                    entidadDocumento.IdTipoDocumento = item.Field<int>("IdTipoDocumento");
                    entidadDocumento.TipoDocumento = item.Field<string>("TipoDocumento");
                    entidadDocumento.NroDocumento = item.Field<string>("NroDocumento");

                    lstEntidaDocumento.Add(entidadDocumento);
                }
                return lstEntidaDocumento;
            }
            return null;
        }
        public IList<IEntidadTipoEntidad> BuscarTiposEntidadesXIdEntidad(int IdEntidad)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadTipoEntidadxIdentidad_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", IdEntidad);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtEntidadTipoEntidad = new DataTable();
            adapter.Fill(dtEntidadTipoEntidad);

            if (dtEntidadTipoEntidad.Rows.Count != 0)
            {
                IList<IEntidadTipoEntidad> lstEntidadTipoEntidad = new List<IEntidadTipoEntidad>();
                foreach (DataRow item in dtEntidadTipoEntidad.Rows)
                {
                    IEntidadTipoEntidad entidadTipoEntidad = new EntidadTipoEntidad();
                    entidadTipoEntidad.IdEntidad = item.Field<int>("IdEntidad");
                    entidadTipoEntidad.IdTipoEntidad = item.Field<int>("IdTipoEntidad");
                    entidadTipoEntidad.TipoEntidad = item.Field<string>("TipoEntidad");

                    lstEntidadTipoEntidad.Add(entidadTipoEntidad);
                }
                return lstEntidadTipoEntidad;
            }
            return null;
        }
        public IList<IEntidadObraSocial> BuscarObrasSocialesXIdEntidad(int IdEntidad)
        {
            MySqlCommand cmd = new MySqlCommand("EntidadObraSocialXIdEntidad_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdEntidad", IdEntidad);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtEntidadObraSocial = new DataTable();
            adapter.Fill(dtEntidadObraSocial);

            if (dtEntidadObraSocial.Rows.Count != 0)
            {
                IList<IEntidadObraSocial> lstEntidadObraSocial = new List<IEntidadObraSocial>();
                foreach (DataRow item in dtEntidadObraSocial.Rows)
                {
                    IEntidadObraSocial entidadObraSocial = new EntidadObraSocial();
                    entidadObraSocial.IdEntidad = item.Field<int>("IdEntidad");
                    entidadObraSocial.IdObraSocial = item.Field<int>("IdObraSocial");
                    entidadObraSocial.ObraSocial = item.Field<string>("ObraSocial");
                    entidadObraSocial.NroObraSocial = item.Field<string>("NroObraSocial");

                    lstEntidadObraSocial.Add(entidadObraSocial);
                }
                return lstEntidadObraSocial;
            }
            return null;
        }

        public void ModificarPaciente(IEntidad paciente)
        {
            MySqlCommand cmdPaciente = new MySqlCommand("Entidad_I", getConexion());
            cmdPaciente.CommandType = CommandType.StoredProcedure;

            cmdPaciente.Parameters.AddWithValue("@IdEntidad", paciente.Id);
            cmdPaciente.Parameters.AddWithValue("@Apellido", paciente.DatosPersona.Apellido);
            cmdPaciente.Parameters.AddWithValue("@Nombre", paciente.DatosPersona.Nombre);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmdPaciente;
            cmdPaciente.ExecuteNonQuery();
        }

        private void EliminarEntidadTiposDocumentos(int IdEntidad)
        {
            MySqlCommand cmdPaciente = new MySqlCommand("EntidadTipoContactoXIdEntidad_D", getConexion());
            cmdPaciente.CommandType = CommandType.StoredProcedure;

            cmdPaciente.Parameters.AddWithValue("@IdEntidad", IdEntidad);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmdPaciente;
            cmdPaciente.ExecuteNonQuery();
        }

        private void ModificarEntidadTipoContacto(IEntidadTipoContacto entidadTipoContacto)
        {
            MySqlCommand cmdPaciente = new MySqlCommand("EntidadTipoContacto_U", getConexion());
            cmdPaciente.CommandType = CommandType.StoredProcedure;

            cmdPaciente.Parameters.AddWithValue("@IdEntidad", entidadTipoContacto.IdEntidad );
            cmdPaciente.Parameters.AddWithValue("@IdTipoContacto", entidadTipoContacto.IdTipoContacto);
            cmdPaciente.Parameters.AddWithValue("@Contacto", entidadTipoContacto.Contacto);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmdPaciente;
            cmdPaciente.ExecuteNonQuery();
        }

        private void EliminarEntidadTiposContactos(int IdEntidad)
        {
            MySqlCommand cmdPaciente = new MySqlCommand("EntidadTipoContactoXIdEntidad_D", getConexion());
            cmdPaciente.CommandType = CommandType.StoredProcedure;

            cmdPaciente.Parameters.AddWithValue("@IdEntidad", IdEntidad);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmdPaciente;
            cmdPaciente.ExecuteNonQuery();
        }
    }
}
