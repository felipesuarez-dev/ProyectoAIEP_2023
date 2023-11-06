using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Data;

namespace Servicios
{
    public class RegistrarEventosService
    {
        private SqlConnection connection;

        public RegistrarEventosService(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void RegistrarEvento(int? idUsuario, string evento, Exception ex = null)
        {
            // Insertar el evento en la tabla RegistroEventos
            //el usuario puede ir nulo
            string insertQuery = "INSERT INTO RegistroEventos (id_usuario, evento, fecha_evento) VALUES (@id_usuario, @evento, @fecha_evento)";
            using (SqlCommand comando = new SqlCommand(insertQuery, connection))
            {
                comando.Parameters.AddWithValue("@id_usuario",  idUsuario);
                comando.Parameters.AddWithValue("@evento", evento);
                comando.Parameters.AddWithValue("@fecha_evento", DateTime.Now);

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                comando.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
