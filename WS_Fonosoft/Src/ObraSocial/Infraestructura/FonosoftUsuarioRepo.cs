using MySql.Data.MySqlClient;
using System.Data;
using WS_Fonosoft.Src.ObraSocial.Dominio.Entidades;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Infraestructura
{
    public class FonosoftUsuarioRepo : IMysqlRepositorio
    {
        private readonly string _connectionString;
        private MySqlConnection _conexion;
        MySqlTransaction? _mySqlTransaction = null;
        public FonosoftUsuarioRepo(string server, string user, string database, string port, string password)
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
            cmd.Parameters.AddWithValue("@FechaInicio", obraSocial.Periodo.FechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", obraSocial.Periodo.FechaFin);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtObraSocial = new DataTable();

            adapter.Fill(dtObraSocial);

            if (dtObraSocial.Rows.Count != 0)
            {
                DataRow drObraSocial = dtObraSocial.Rows[0];
                obraSocial.Id = int.Parse(drObraSocial["IdObraSocial"].ToString());
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
                obraSocial.Id = int.Parse(drObraSocial["IdObraSocial"].ToString());
                return obraSocial;
            }
            return null;
        }
    }
}
