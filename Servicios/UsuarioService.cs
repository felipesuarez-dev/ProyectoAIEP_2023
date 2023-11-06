using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dominio;

namespace Servicios
{
    public class UsuarioService
    {
        private SqlConnection connection;
        private readonly RegistrarEventosService _registrarEventosService;

        public UsuarioService(SqlConnection connection)
        {
            this.connection = connection;
            _registrarEventosService = new RegistrarEventosService(connection);
        }

        public Usuario ObtenerDatosUsuarioPorUsername(string username)
        {
            try
            {
                Usuario usuario = null;

                string selectQuery = "SELECT * FROM Usuarios WHERE username = @username";

                using (SqlCommand comando = new SqlCommand(selectQuery, connection))
                {
                    comando.Parameters.AddWithValue("@username", username);

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                            Rut = reader["rut"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = reader["pass_user"].ToString(),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            IdRol = Convert.ToInt32(reader["id_rol"]),
                            IntentosFallidos = Convert.ToInt32(reader["intentos_fallidos"]),
                            IdEstado = Convert.ToInt32(reader["id_estado"]),
                            FechaCreacion = reader["fecha_creacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["fecha_creacion"]),
                            FechaModificacion = reader["fecha_modificacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["fecha_modificacion"]),
                            bolActivo = Convert.ToBoolean(reader["bol_activo"])
                        };
                    }

                    connection.Close();
                }

                return usuario;
            }
            catch (Exception ex)
            {
                _registrarEventosService.RegistrarEvento(null, "Error al obtener datos de usuario", ex);
                throw;
            }
        }

        public string EncriptarContraseña(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public string DesEncriptarContraseña(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
