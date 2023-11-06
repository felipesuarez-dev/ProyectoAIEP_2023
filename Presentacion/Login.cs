using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using Servicios;

namespace Presentacion
{
    public partial class Login : Form
    {
        private readonly Db db;
        private readonly IniciarSesionService _iniciarSesionService;
        private readonly RegistrarEventosService _registrarEventosService;
        private readonly UsuarioService _usuarioService;

        public Login()
        {
            InitializeComponent();
            db = new Db();
            _iniciarSesionService = new IniciarSesionService(db.ObtenerConexion());
            _registrarEventosService = new RegistrarEventosService(db.ObtenerConexion());
            _usuarioService = new UsuarioService(db.ObtenerConexion());
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {                
                var resultado = _iniciarSesionService.Validacion(txtUser.Text, txtPass.Text);
                var mensajeExito = "Inicio de sesión exitoso.";
                var mensajeBloqueado = "La cuenta está bloqueada. Comuníquese con el soporte.";
                var mensajeFallido = "Nombre de usuario o contraseña incorrectos.";
                var user = _usuarioService.ObtenerDatosUsuario(txtUser.Text); //Obtenemos todos los datos del usuario en BD

                //Manejo de inicio de sesión según validación
                if (resultado.IsSuccessful == true)
                {                   
                    MessageBox.Show(mensajeExito);
                    _registrarEventosService.RegistrarEvento(user.IdUsuario, mensajeExito);
                    Index indexForm = new Index(); //abrir siguiente ventana
                    indexForm.Show();
                    this.Hide();
                }
                if (resultado.IsBlocked == true)
                {
                    MessageBox.Show(mensajeBloqueado);
                    _registrarEventosService.RegistrarEvento(user.IdUsuario, "Inicio de sesión fallido:"+mensajeBloqueado);
                    txtUser.Text = "";
                    txtPass.Text = "";
                }
                if (resultado.IsSuccessful == false && resultado.IsBlocked == false)
                {
                    _registrarEventosService.RegistrarEvento(null, "Inicio de sesión fallido:"+mensajeFallido);
                    MessageBox.Show(mensajeFallido);
                    txtUser.Text = "";
                    txtPass.Text = "";
                }
            }
            catch (Exception ex)
            {
                _registrarEventosService.RegistrarEvento(null, "Error al iniciar sesión", ex);
                throw;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //limpiar los campos del iniciar sesión
            txtUser.Text = "";
            txtPass.Text = "";
        }
    }
}
