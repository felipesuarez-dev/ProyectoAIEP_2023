using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data
{
    public class Db
    {
        private SqlConnection connection;
        private string connectionString;

        public Db()
        {
            // COLOCAR NUESTRA CONEXION
            connectionString = "Data Source=NombreDelServidor;Initial Catalog=JFM;User ID=Usuario;Password=Contraseña";
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
