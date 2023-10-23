using MySql.Data.MySqlClient;
using System.Data;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Infraestructura
{
    public class FonosoftObraSocialRepo : IRepositorioObraSocial
    {
        private readonly string _connectionString;
        private MySqlConnection _conexion;
        MySqlTransaction? _mySqlTransaction = null;
        public FonosoftObraSocialRepo(string server, string user, string database, string port, string password)
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
        public IObraSocial AgregarObraSocial(IObraSocial obraSocial)
        {
            MySqlCommand cmd = new MySqlCommand("ObraSocial_I", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", obraSocial.Nombre);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtObraSocial = new DataTable();

            adapter.Fill(dtObraSocial);

            if (dtObraSocial.Rows.Count != 0)
            {
                DataRow drObraSocial = dtObraSocial.Rows[0];
                obraSocial.Id = int.Parse(drObraSocial["Id"].ToString());
                return obraSocial;
            }
            return null;
        }
        public IList<IObraSocial> BuscarObrasSociales()
        {
            MySqlCommand cmd = new MySqlCommand("ObraSocial_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtObraSocial = new DataTable();

            adapter.Fill(dtObraSocial);

            if (dtObraSocial.Rows.Count != 0)
            {
                IList<IObraSocial> lstObraSocial = new List<IObraSocial>();
                foreach (DataRow item in dtObraSocial.Rows)
                {
                    IObraSocial obraSocial = new Dominio.Entidades.ObraSocial();
                    obraSocial.Id = item.Field<int>("Id");
                    obraSocial.Nombre = item.Field<string>("Nombre");
                    obraSocial.Periodo.FechaInicio = item.Field<DateTime>("FechaInicio");
                    obraSocial.Periodo.FechaFin = item.Field<DateTime>("FechaFin");
                    if (DateTime.Now >= obraSocial.Periodo.FechaInicio && DateTime.Now <= obraSocial.Periodo.FechaFin)
                    {
                        obraSocial.Periodo.Estado = true;
                    }

                    lstObraSocial.Add(obraSocial);
                }
                return lstObraSocial;
            }
            return null;
        }
        public IList<IObraSocial> BuscarObrasSocialesHabilitadas()
        {
            MySqlCommand cmd = new MySqlCommand("ObraSocialHabilitadas_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtObraSocial = new DataTable();

            adapter.Fill(dtObraSocial);

            if (dtObraSocial.Rows.Count != 0)
            {
                IList<IObraSocial> lstObraSocial = new List<IObraSocial>();
                foreach (DataRow item in dtObraSocial.Rows)
                {
                    IObraSocial obraSocial = new Dominio.Entidades.ObraSocial();
                    obraSocial.Id = item.Field<int>("Id");
                    obraSocial.Nombre = item.Field<string>("Nombre");
                    obraSocial.Periodo.FechaInicio = item.Field<DateTime>("FechaInicio");
                    obraSocial.Periodo.FechaFin = item.Field<DateTime>("FechaFin");
                    if (DateTime.Now >= obraSocial.Periodo.FechaInicio && DateTime.Now <= obraSocial.Periodo.FechaFin)
                    {
                        obraSocial.Periodo.Estado = true;
                    }

                    lstObraSocial.Add(obraSocial);
                }
                return lstObraSocial;
            }
            return null;
        }
        public IObraSocial BuscarObraSocialXId(int Id)
        {
            MySqlCommand cmd = new MySqlCommand("ObraSocialXId_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", Id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtObraSocial = new DataTable();

            adapter.Fill(dtObraSocial);

            if (dtObraSocial.Rows.Count != 0)
            {
                DataRow drObraSocial = dtObraSocial.Rows[0];
                IObraSocial obraSocial = new Dominio.Entidades.ObraSocial();
                obraSocial.Id = int.Parse(drObraSocial["Id"].ToString());
                obraSocial.Nombre = drObraSocial["Nombre"].ToString();
                obraSocial.Periodo.FechaInicio = DateTime.Parse(drObraSocial["FechaInicio"].ToString());
                obraSocial.Periodo.FechaFin = DateTime.Parse(drObraSocial["FechaFin"].ToString());
                if (DateTime.Now >= obraSocial.Periodo.FechaInicio && DateTime.Now <= obraSocial.Periodo.FechaFin)
                {
                    obraSocial.Periodo.Estado = true;
                }

                return obraSocial;
            }
            return null;
        }
        public IObraSocial BuscarObraSocialXNombre(string nombre)
        {
            MySqlCommand cmd = new MySqlCommand("ObraSocialXDescripcion_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", nombre);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtObraSocial = new DataTable();

            adapter.Fill(dtObraSocial);

            if (dtObraSocial.Rows.Count != 0)
            {
                DataRow drObraSocial = dtObraSocial.Rows[0];
                IObraSocial obraSocial = new Dominio.Entidades.ObraSocial();
                obraSocial.Id = int.Parse(drObraSocial["Id"].ToString());
                obraSocial.Nombre = drObraSocial["Nombre"].ToString();
                obraSocial.Periodo.FechaInicio = DateTime.Parse(drObraSocial["FechaInicio"].ToString());
                obraSocial.Periodo.FechaFin = DateTime.Parse(drObraSocial["FechaFin"].ToString());
                return obraSocial;
            }
            return null;
        }
        public void ModificarObraSocialXId(IObraSocial obraSocial)
        {
            MySqlCommand cmd = new MySqlCommand("ObraSocial_U", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", obraSocial.Id);
            cmd.Parameters.AddWithValue("@Nombre", obraSocial.Nombre);

            cmd.ExecuteNonQuery();
        }
        public void HabilitarObraSocialXId(IObraSocial obraSocial)
        {
            MySqlCommand cmd = new MySqlCommand("ObraSocialHabilitar_U", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", obraSocial.Id);

            cmd.ExecuteNonQuery();
        }
        public void DesHabilitarObraSocialXId(IObraSocial obraSocial)
        {
            MySqlCommand cmd = new MySqlCommand("ObraSocialDeshabilitar_U", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", obraSocial.Id);

            cmd.ExecuteNonQuery();
        }
    }
}
