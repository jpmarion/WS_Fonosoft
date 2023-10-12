using MySql.Data.MySqlClient;
using System.Data;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Infraestructura
{
    public class FonosoftAuthRepo : IRepositorioAuth
    {
        private readonly string _connectionString;
        private MySqlConnection _conexion;
        MySqlTransaction? _mySqlTransaction = null;

        public FonosoftAuthRepo(string server, string user, string database, string port, string password)
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

        #region USUARIO
        public IUsuario RegistrarUsuario(IUsuario usuario)
        {
            MySqlCommand cmd = new MySqlCommand("Usuario_I", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Password", usuario.Password);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dtUsuario = new DataTable();

            adapter.Fill(dtUsuario);

            if (dtUsuario.Rows.Count != 0)
            {
                DataRow drUsuario = dtUsuario.Rows[0];

                usuario.Id = int.Parse(drUsuario["Id"].ToString());

                return usuario;
            }
            return null;
        }
        public IUsuario BuscarUsuarioXNombreUsuario(string nombreUsuario)
        {
            MySqlCommand cmd = new MySqlCommand("UsuarioXNombreUsuario_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataTable dtUsuario = new DataTable();
            adapter.Fill(dtUsuario);

            if (dtUsuario.Rows.Count != 0)
            {
                IUsuario usuario = new Usuario();
                DataRow drUsuario = dtUsuario.Rows[0];

                usuario.Id = int.Parse(drUsuario["Id"].ToString());
                usuario.NombreUsuario = drUsuario["NombreUsuario"].ToString();
                usuario.Password = drUsuario["Password"].ToString();
                usuario.Email = drUsuario["Email"].ToString();
                usuario.FechaCreacion = DateTime.Parse(drUsuario["FechaCreacion"].ToString());
                usuario.Confirmacion = Convert.ToBoolean(drUsuario["Confirmacion"]);

                return usuario;
            }
            return null;
        }
        public void ConfirmarUsuario(int id)
        {
            MySqlCommand cmd = new MySqlCommand("UsuarioConfirmar_U", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
        }
        public IUsuario BuscarUsuarioXNombreUsuarioXPassword(string NombreUsuario, string Password)
        {
            MySqlCommand cmd = new MySqlCommand("UsuarioXNombreUsuarioXPassword_S", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
            cmd.Parameters.AddWithValue("@Password", Password);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataTable dtUsuario = new DataTable();
            adapter.Fill(dtUsuario);

            if (dtUsuario.Rows.Count != 0)
            {
                IUsuario usuario = new Usuario();
                DataRow drUsuario = dtUsuario.Rows[0];

                usuario.Id = int.Parse(drUsuario["Id"].ToString());
                usuario.NombreUsuario = drUsuario["NombreUsuario"].ToString();
                usuario.Email = drUsuario["Email"].ToString();
                usuario.FechaCreacion = DateTime.Parse(drUsuario["FechaCreacion"].ToString());
                usuario.Confirmacion = Convert.ToBoolean(drUsuario["Confirmacion"]);

                return usuario;
            }
            return null;

        }
        public void ResetPasswordUsuario(string NombreUsuario, string Password)
        {
            MySqlCommand cmd = new MySqlCommand("UsuarioResetPassword_U", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
            cmd.Parameters.AddWithValue("@Password", Password);

            cmd.ExecuteNonQuery();
        }
        public void ModificarContrasenia(int IdUsuario, string Password)
        {
            MySqlCommand cmd = new MySqlCommand("UsuarioModificarPassword_U", getConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
            cmd.Parameters.AddWithValue("@Password", Password);

            cmd.ExecuteNonQuery();
        }
        #endregion
    }
}
