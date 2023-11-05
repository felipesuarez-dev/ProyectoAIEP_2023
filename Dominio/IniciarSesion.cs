using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Dominio
{
    public class IniciarSesion
    {
        private SqlConnection connection;

        public IniciarSesion(SqlConnection connection)
        {
            this.connection = connection;
        }

        public bool Validacion(string username, string password)
        {
            using (SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM usuario WHERE userName = @username AND passUser = @password", connection))
            {
                comando.Parameters.AddWithValue("@username", username);
                comando.Parameters.AddWithValue("@password", password);
                connection.Open();
                int count = (int)comando.ExecuteScalar();
                connection.Close();
                return count > 0;
            }
        }
    }
}
