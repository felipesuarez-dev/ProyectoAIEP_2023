using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Data
{
    public class Db
    {
        private SqlConnection connection;

        public SqlConnection ObtenerConexion()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bdAIEP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

    }
}
