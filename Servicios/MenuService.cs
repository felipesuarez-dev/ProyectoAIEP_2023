using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class MenuService
    {
        private SqlConnection connection;
        private readonly RegistrarEventosService _registrarEventosService;

        public MenuService(SqlConnection connection)
        {
            this.connection = connection;
            _registrarEventosService = new RegistrarEventosService(connection);
        }

        public DataTable ConsultarMenu(int IdUsuario)
        {
            SqlDataReader leer;
            DataTable tabla = new DataTable();
            SqlCommand comando = new SqlCommand();

            // ejecutar al procedimiento almacenado
            connection.Open();
            comando.Connection = connection;
            comando.CommandText = "ConsultarMenuUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@id_usuario", IdUsuario);
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            connection.Close();
            return tabla;
        }

        public DataTable ConsultarSubmenu(int IdUsuario, int menuPrincipal)
        {
            SqlDataReader leer;
            DataTable tabla = new DataTable();
            SqlCommand comando = new SqlCommand();

            // ejecutar al procedimiento almacenado
            connection.Open();
            comando.Connection = connection;
            comando.CommandText = "ConsultarSubMenuUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@id_usuario", IdUsuario);
            comando.Parameters.AddWithValue("@id_menu_principal", menuPrincipal);
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            connection.Close();
            return tabla;
        }
    }
}
