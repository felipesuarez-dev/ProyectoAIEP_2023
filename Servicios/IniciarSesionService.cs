﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Data;
using System.Security.Cryptography;

namespace Servicios
{
    public class IniciarSesionService
    {
        private SqlConnection connection;
        private readonly RegistrarEventosService _registrarEventosService;
        private readonly UsuarioService _usuarioService;

        public IniciarSesionService(SqlConnection connection)
        {
            this.connection = connection;
            _registrarEventosService = new RegistrarEventosService(connection);
            _usuarioService = new UsuarioService(connection);
        }

        public class ValidationResult
        {
            public bool IsSuccessful { get; set; }
            public bool IsBlocked { get; set; }
            public bool IsExist { get; set; }
        }

        public ValidationResult Validacion(string username, string password)
        {
            ValidationResult result = new ValidationResult();

            try
            {
                // Consulta para verificar las credenciales y el estado del usuario
                // Consulta para actualizar los datos del usuario
                string selectQuery = "SELECT pass_user, intentos_fallidos, id_estado, bol_activo FROM Usuarios WHERE username = @username";
                string updateQuery = "UPDATE usuarios SET intentos_fallidos = @intentos_fallidos, id_estado = @id_estado, bol_activo = @bol_activo WHERE username = @username";

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
                        bool isActive = reader.GetBoolean(reader.GetOrdinal("bol_activo"));

                        var passDesencriptada = _usuarioService.DesEncriptarContraseña(storedPassword);

                        result.IsSuccessful = false;
                        result.IsBlocked = false;

                        if (idEstado == Dominio.Usuario.Bloqueado)
                        {
                            connection.Close();
                            result.IsBlocked = true; // guardar resultado como usuario bloqueado
                        }
                        else if (password == passDesencriptada)
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

                            connection.Close();
                            result.IsSuccessful = true; //guardar resultado como usuario validado
                        }
                        else
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                            intentosFallidos++;

                            // Verificar si se superó el límite de intentos fallidos
                            if (intentosFallidos > 2)
                            {
                                // Bloquear la cuenta
                                result.IsBlocked = true;
                                isActive = false;
                                idEstado = Dominio.Usuario.Bloqueado; // Cambia el estado a "Bloqueado"
                            }

                            //Actualizar los intentos fallidos e id_estado en la base de datos
                            using (SqlCommand actualizarComando = new SqlCommand(updateQuery, connection))
                            {
                                if (connection.State != ConnectionState.Open)
                                {
                                    connection.Open();
                                }
                                actualizarComando.Parameters.AddWithValue("@intentos_fallidos", intentosFallidos);
                                actualizarComando.Parameters.AddWithValue("@id_estado", idEstado);
                                actualizarComando.Parameters.AddWithValue("@username", username);
                                actualizarComando.Parameters.AddWithValue("@bol_activo", isActive);
                                actualizarComando.ExecuteNonQuery();
                            }

                            result.IsExist = true;
                            connection.Close();
                        }
                    }
                    else
                    {
                        result.IsSuccessful = false;
                    }

                    connection.Close();
                    return result; // Devuelve el resultado
                }
            }
            catch (Exception ex)
            {
                _registrarEventosService.RegistrarEvento(null, "Error al iniciar sesión", ex);
                throw;
            }
        }

        //public static string EncodePassword(string originalPassword)
        //{
        //    SHA1 sha1 = new SHA1CryptoServiceProvider();

        //    byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
        //    byte[] hash = sha1.ComputeHash(inputBytes);

        //    return Convert.ToBase64String(hash);
        //}
        
    }
}

