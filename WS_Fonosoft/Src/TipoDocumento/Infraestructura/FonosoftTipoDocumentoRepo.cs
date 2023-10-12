using MySql.Data.MySqlClient;
using System.Data;
using WS_Fonosoft.Src.ObraSocial.Dominio.Entidades;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;
using WS_Fonosoft.Src.TipoDocumento.Infraestructura.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WS_Fonosoft.Src.TipoDocumento.Infraestructura
{
    public class FonosoftTipoDocumentoRepo : IRepositorioTipoDocumento
    {
        private readonly string _connectionString;
        private MySqlConnection _conexion;
        MySqlTransaction? _mySqlTransaction = null;

        public FonosoftTipoDocumentoRepo(string server, string user, string database, string port, string password)
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
        public ITipoDocumento AgregarTipoDocumento(ITipoDocumento tipoDocumento)
        {
            MySqlCommand cmd = new MySqlCommand("TipoDocumento_I", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", tipoDocumento.Nombre);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtTipoDocumento = new DataTable();

            adapter.Fill(dtTipoDocumento);

            if (dtTipoDocumento.Rows.Count != 0)
            {
                DataRow drTipoDocumento = dtTipoDocumento.Rows[0];
                tipoDocumento.Id = int.Parse(drTipoDocumento["Id"].ToString());
                return tipoDocumento;
            }
            return null;
        }
        public IList<ITipoDocumento> BuscarTiposDocumentos()
        {
            MySqlCommand cmd = new MySqlCommand("TipoDocumento_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtTipoDocumento = new DataTable();

            adapter.Fill(dtTipoDocumento);

            if (dtTipoDocumento.Rows.Count != 0)
            {
                IList<ITipoDocumento> lstTipoDocumento = new List<ITipoDocumento>();

                foreach (DataRow item in dtTipoDocumento.Rows)
                {
                    ITipoDocumento tipoDocumento = new Dominio.Entidades.TipoDocumento();
                    tipoDocumento.Id = item.Field<int>("Id");
                    tipoDocumento.Nombre = item.Field<string>("Nombre");
                    tipoDocumento.Periodo.FechaInicio = item.Field<DateTime>("FechaInicio");
                    tipoDocumento.Periodo.FechaFin = item.Field<DateTime>("FechaFin");
                    if (DateTime.Now >= tipoDocumento.Periodo.FechaInicio && DateTime.Now <= tipoDocumento.Periodo.FechaFin)
                    {
                        tipoDocumento.Periodo.Estado = true;
                    }
                    lstTipoDocumento.Add(tipoDocumento);
                }
                return lstTipoDocumento;
            }
            return null;
        }
        public ITipoDocumento BuscarTipoDocumentoXId(int id)
        {
            MySqlCommand cmd = new MySqlCommand("TipoDocumentoXId_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtTipoDocumento = new DataTable();

            adapter.Fill(dtTipoDocumento);

            if (dtTipoDocumento.Rows.Count != 0)
            {
                DataRow drTipoDocumento = dtTipoDocumento.Rows[0];
                ITipoDocumento tipoDocumento = new Dominio.Entidades.TipoDocumento();
                tipoDocumento.Id = int.Parse(drTipoDocumento["Id"].ToString());
                tipoDocumento.Nombre = drTipoDocumento["Nombre"].ToString();
                tipoDocumento.Periodo.FechaInicio = DateTime.Parse(drTipoDocumento["FechaInicio"].ToString());
                tipoDocumento.Periodo.FechaFin = DateTime.Parse(drTipoDocumento["FechaFin"].ToString());
                if (DateTime.Now >= tipoDocumento.Periodo.FechaInicio && DateTime.Now <= tipoDocumento.Periodo.FechaFin)
                {
                    tipoDocumento.Periodo.Estado = true;
                }
                return tipoDocumento;
            }
            return null;
        }
        public ITipoDocumento BuscarTipoDocumentoXNombre(string nombre)
        {
            MySqlCommand cmd = new MySqlCommand("TipoDocumentoXNombre_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", nombre);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtTipoDocumento = new DataTable();

            adapter.Fill(dtTipoDocumento);

            if (dtTipoDocumento.Rows.Count != 0)
            {
                DataRow drTipoDocumento = dtTipoDocumento.Rows[0];
                ITipoDocumento tipoDocumento = new Dominio.Entidades.TipoDocumento();
                tipoDocumento.Id = int.Parse(drTipoDocumento["Id"].ToString());
                tipoDocumento.Nombre = drTipoDocumento["Nombre"].ToString();
                tipoDocumento.Periodo.FechaInicio = DateTime.Parse(drTipoDocumento["FechaInicio"].ToString());
                tipoDocumento.Periodo.FechaFin = DateTime.Parse(drTipoDocumento["FechaFin"].ToString());
                if (DateTime.Now >= tipoDocumento.Periodo.FechaInicio && DateTime.Now <= tipoDocumento.Periodo.FechaFin)
                {
                    tipoDocumento.Periodo.Estado = true;
                }
                return tipoDocumento;
            }
            return null;
        }
        public void ModificarTipoDocumentoXId(ITipoDocumento tipoDocumento)
        {
            MySqlCommand cmd = new MySqlCommand("TipoDocumento_U", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", tipoDocumento.Id);
            cmd.Parameters.AddWithValue("@Nombre", tipoDocumento.Nombre);

            cmd.ExecuteNonQuery();
        }
        public void CambiarEstadoTipoDocumentoXId(int IdTipoDocumento)
        {
            ITipoDocumento tipoDocumento = BuscarTipoDocumentoXId(IdTipoDocumento);
            string sp = string.Empty;
            if (tipoDocumento.Periodo.Estado)
            {
                sp = "TipoDocumentoDeshabilitado_U";
            }
            else
            {
                sp = "TipoDocumentoHabilitado_U";
            }

            MySqlCommand cmd = new MySqlCommand(sp, getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", tipoDocumento.Id);

            cmd.ExecuteNonQuery();
        }
    }
}
