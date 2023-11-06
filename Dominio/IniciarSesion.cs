using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Dominio
{
    public class IniciarSesion
    {
        private SqlConnection connection;

        public IniciarSesion(SqlConnection connection)
        {
            this.connection = connection;
        }

        public class ValidationResult
        {
            public bool IsSuccessful { get; set; }
            public bool IsBlocked { get; set; }
        }

        //public bool Validacion(string username, string password)
        //{
        //    try
        //    {
        //        // Consulta para verificar las credenciales y el estado del usuario
        //        // Consulta para actualizar los datos del usuario
        //        string selectQuery = "SELECT Password, IntentosFallidos, IdEstado FROM Usuarios WHERE Username = @username AND passUser = @password";
        //        string updateQuery = "UPDATE Usuarios SET IntentosFallidos = @IntentosFallidos, IdEstado = @IdEstado WHERE Username = @Username";

        //        using (SqlCommand comando = new SqlCommand(selectQuery, connection))
        //        {
        //            comando.Parameters.AddWithValue("@username", username);
        //            comando.Parameters.AddWithValue("@password", password);
        //            connection.Open();
        //            SqlDataReader reader = comando.ExecuteReader(); //guardo el resultado de la consulta en un reader

        //            if (reader.Read())
        //            {
        //                //tomo los valores guardados en el reader y transformo sus tipos a string e int, además 
        //                string storedPassword = reader["Password"].ToString();
        //                int intentosFallidos = Convert.ToInt32(reader["IntentosFallidos"]);
        //                int idEstado = Convert.ToInt32(reader["IdEstado"]);

        //                if (idEstado == 2) // 2 es el estado para "Bloqueado"
        //                {
        //                    connection.Close();
        //                    return false; // Usuario bloqueado
        //                }

        //                if (password == storedPassword)
        //                {
        //                    // Restablecer los intentos fallidos en 0, para no bloquear la cuenta posteriormente
        //                    intentosFallidos = 0;
        //                    using (SqlCommand actualizarComando = new SqlCommand(updateQuery, connection))
        //                    {
        //                        actualizarComando.Parameters.AddWithValue("@IntentosFallidos", intentosFallidos);
        //                        actualizarComando.Parameters.AddWithValue("@Username", username);
        //                        actualizarComando.ExecuteNonQuery();
        //                    }
        //                    return true;
        //                }
        //                else
        //                {
        //                    intentosFallidos++;

        //                    // Verificar si se superó el límite de intentos fallidos
        //                    if (intentosFallidos >= 3)
        //                    {
        //                        // Bloquear la cuenta
        //                        idEstado = 2; // Cambia el estado a "Bloqueado"
        //                    }

        //                    // Actualizar los intentos fallidos e IdEstado en la base de datos
        //                    using (SqlCommand actualizarComando = new SqlCommand(updateQuery, connection))
        //                    {
        //                        actualizarComando.Parameters.AddWithValue("@IntentosFallidos", intentosFallidos);
        //                        actualizarComando.Parameters.AddWithValue("@IdEstado", idEstado);
        //                        actualizarComando.Parameters.AddWithValue("@Username", username);
        //                        actualizarComando.ExecuteNonQuery();
        //                    }
        //                    return false;
        //                }
        //            }

        //            connection.Close();
        //        }

        //        return false; // Autenticación fallida
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al validar la sesión", ex);
        //    }
        //}

        //public ValidationResult Validacion(string username, string password)
        //{
        //    ValidationResult result = new ValidationResult();

        //    try
        //    {
        //        // Consulta para verificar las credenciales y el estado del usuario
        //        // Consulta para actualizar los datos del usuario
        //        string selectQuery = "SELECT pass_user, intentos_fallidos, id_estado FROM Usuarios WHERE username = @username AND pass_user = @pass_user";
        //        string updateQuery = "UPDATE usuarios SET intentos_fallidos = @intentos_fallidos, id_estado = @id_estado WHERE username = @username";

        //        using (SqlCommand comando = new SqlCommand(selectQuery, connection))
        //        {
        //            comando.Parameters.AddWithValue("@username", username);
        //            comando.Parameters.AddWithValue("@pass_user", password);
        //            connection.Open();
        //            SqlDataReader reader = comando.ExecuteReader(); //guardo el resultado de la consulta en un reader
        //            connection.Close();

        //            if (reader.Read())
        //            {
        //                //tomo los valores guardados en el reader y transformo sus tipos a string e int, además 
        //                string storedUser= reader["username"].ToString();
        //                string storedPassword = reader["pass_user"].ToString();
        //                int intentosFallidos = Convert.ToInt32(reader["intentos_fallidos"]);
        //                int idEstado = Convert.ToInt32(reader["id_estado"]);
        //                //bool isActive = reader.GetBoolean(reader.GetOrdinal("bol_activo"));

        //                result.IsSuccessful = false;
        //                result.IsBlocked = false;

        //                if (idEstado == 2) // 2 es el estado para "Bloqueado"
        //                {
        //                    result.IsBlocked = true; // Usuario bloqueado
        //                    return result;
        //                }
        //                else if (username != storedUser)
        //                {
        //                    result.IsSuccessful = false;
        //                }
        //                else if (password == storedPassword)
        //                {
        //                    // Restablecer los intentos fallidos en 0, para no bloquear la cuenta posteriormente
        //                    intentosFallidos = 0;
        //                    using (SqlCommand actualizarComando = new SqlCommand(updateQuery, connection))
        //                    {
        //                        actualizarComando.Parameters.AddWithValue("@intentos_fallidos", intentosFallidos);
        //                        actualizarComando.Parameters.AddWithValue("@username", username);
        //                        actualizarComando.ExecuteNonQuery();
        //                    }
        //                    result.IsSuccessful = true;
        //                }
        //                else
        //                {
        //                    intentosFallidos++;

        //                    // Verificar si se superó el límite de intentos fallidos
        //                    if (intentosFallidos >= 3)
        //                    {
        //                        // Bloquear la cuenta
        //                        idEstado = 2; // Cambia el estado a "Bloqueado"
        //                    }

        //                    // Actualizar los intentos fallidos e IdEstado en la base de datos
        //                    using (SqlCommand actualizarComando = new SqlCommand(updateQuery, connection))
        //                    {
        //                        actualizarComando.Parameters.AddWithValue("@intentos_fallidos", intentosFallidos);
        //                        actualizarComando.Parameters.AddWithValue("@id_estado", idEstado);
        //                        actualizarComando.Parameters.AddWithValue("@username", username);
        //                        actualizarComando.ExecuteNonQuery();
        //                    }

        //                    result.IsBlocked = true;
        //                }

        //                return result; // Devuelve el resultado
        //            }

        //            connection.Close();
        //        }

        //        return result; // Autenticación fallida
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al validar la sesión", ex);
        //    }
        //}

        public ValidationResult Validacion(string username, string password)
        {
            ValidationResult result = new ValidationResult();

            try
            {
                // Consulta para verificar las credenciales y el estado del usuario
                // Consulta para actualizar los datos del usuario
                string selectQuery = "SELECT pass_user, intentos_fallidos, id_estado FROM Usuarios WHERE username = @username AND pass_user = @pass_user";
                string updateQuery = "UPDATE usuarios SET intentos_fallidos = @intentos_fallidos, id_estado = @id_estado WHERE username = @username";

                using (SqlCommand comando = new SqlCommand(selectQuery, connection))
                {
                    comando.Parameters.AddWithValue("@username", username);
                    comando.Parameters.AddWithValue("@pass_user", password);
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    SqlDataReader reader = comando.ExecuteReader(); //guardo el resultado de la consulta en un reader

                    if (reader.Read())
                    {
                        //tomo los valores guardados en el reader y transformo sus tipos a string e int, además 
                        string storedPassword = reader["pass_user"].ToString();
                        int intentosFallidos = Convert.ToInt32(reader["intentos_fallidos"]);
                        int idEstado = Convert.ToInt32(reader["id_estado"]);

                        result.IsSuccessful = false;
                        result.IsBlocked = false;

                        if (idEstado == 2) // 2 es el estado para "Bloqueado"
                        {
                            result.IsBlocked = true; // Usuario bloqueado
                        }
                        else if (password == storedPassword)
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                            // Restablecer los intentos fallidos en 0, para no bloquear la cuenta posteriormente
                            intentosFallidos = 0;
                            using (SqlCommand actualizarComando = new SqlCommand(updateQuery, connection))
                            {
                                if (connection.State != ConnectionState.Open)
                                {
                                    connection.Open();
                                }
                                actualizarComando.Parameters.AddWithValue("@intentos_fallidos", intentosFallidos);
                                actualizarComando.Parameters.AddWithValue("@id_estado", idEstado);
                                actualizarComando.Parameters.AddWithValue("@username", username);
                                actualizarComando.Parameters.AddWithValue("@bol_activo", true);
                                actualizarComando.ExecuteNonQuery();
                            }
                            result.IsSuccessful = true;
                        }
                        else
                        {
                            intentosFallidos++;

                            // Verificar si se superó el límite de intentos fallidos
                            if (intentosFallidos >= 3)
                            {
                                // Bloquear la cuenta
                                idEstado = 2; // Cambia el estado a "Bloqueado"
                            }

                            // Actualizar los intentos fallidos e IdEstado en la base de datos
                            //using (SqlCommand actualizarComando = new SqlCommand(updateQuery, connection))
                            //{
                            //    actualizarComando.Parameters.AddWithValue("@intentos_fallidos", intentosFallidos);
                            //    actualizarComando.Parameters.AddWithValue("@id_estado", idEstado);
                            //    actualizarComando.Parameters.AddWithValue("@username", username);
                            //    actualizarComando.Parameters.AddWithValue("@bol_activo", true);
                            //    actualizarComando.ExecuteNonQuery();
                            //}

                            result.IsBlocked = true;
                        }
                    }
                    else
                    {
                        result.IsSuccessful = false;
                    }

                    return result; // Devuelve el resultado
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar la sesión", ex);
            }
        }


    }
}
