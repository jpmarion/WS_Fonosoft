using MySql.Data.MySqlClient;
using System.Data;
using WS_Fonosoft.Src.TipoContacto.Dominio.Interface;
using WS_Fonosoft.Src.TipoContacto.Infraestructura.Interface;

namespace WS_Fonosoft.Src.TipoContacto.Infraestructura
{
    public class FonosoftTipoContactoRepo : IRepositorioTipoContacto
    {
        private readonly string _connectionString;
        private MySqlConnection _conexion;
        MySqlTransaction? _mySqlTransaction = null;

        public FonosoftTipoContactoRepo(string server, string user, string database, string port, string password)
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
        public IList<ITipoContacto> BuscarTipoContactos()
        {
            MySqlCommand cmd = new MySqlCommand("TipoContacto_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dtTipoContacto = new DataTable();

            adapter.Fill(dtTipoContacto);

            if (dtTipoContacto.Rows.Count != 0)
            {
                IList<ITipoContacto> lstTipoContacto = new List<ITipoContacto>();
                foreach (DataRow item in dtTipoContacto.Rows)
                {
                    ITipoContacto tipoContacto = new Dominio.Entidad.TipoContacto();
                    tipoContacto.Id = item.Field<int>("Id");
                    tipoContacto.Nombre = item.Field<string>("Nombre");

                    lstTipoContacto.Add(tipoContacto);
                }
                return lstTipoContacto;
            }
            return null;
        }
    }
}
