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
using Dominio;
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
            //var clave = _iniciarSesionService.Encriptar("123456");
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = null;
                var resultado = _iniciarSesionService.Validacion(txtUser.Text, txtPass.Text); //método para validar al usuario ingresado
                if (resultado.IsSuccessful || resultado.IsBlocked || resultado.IsExist)
                {
                    user = _usuarioService.ObtenerDatosUsuarioPorUsername(txtUser.Text); //Obtenemos todos los datos del usuario en BD
                }

                //mensajes
                var mensajeExito =  "Inicio de sesión exitoso.";
                var mensajeBloqueado = " Su cuenta fue bloqueada. Comuníquese con el soporte.";
                var mensajeFallido = "Nombre de usuario o contraseña incorrectos. ";
                var mjeIntentosFallido = "\n\nNúmero de intentos fallidos: ";
                var mjeContraseñaIncorrecta = "Contraseña Incorrecta. ";
                var advertenciaBloqueo = "\n\nEl próximo intento fallido bloqueará la cuenta.";

                //Manejo de inicio de sesión según resultado de la validación
                if (resultado.IsSuccessful)
                {                   
                    MessageBox.Show(mensajeExito);
                    _registrarEventosService.RegistrarEvento(user.IdUsuario, user.Nombre + " " + user.Apellido + ": " + mensajeExito);
                    Index indexForm = new Index(); //abrir siguiente ventana
                    indexForm.Show();
                    this.Hide();
                }
                if (resultado.IsBlocked)
                {
                    MessageBox.Show(mensajeBloqueado);
                    _registrarEventosService.RegistrarEvento(user.IdUsuario, user.Nombre + " " + user.Apellido + ": " + "Inicio de sesión fallido." + mensajeBloqueado);
                    txtUser.Text = "";
                    txtPass.Text = "";
                }
                if (resultado.IsExist == true && resultado.IsBlocked == false)
                {
                    if (user.IntentosFallidos == 2)
                        MessageBox.Show("Inicio de sesión fallido: " + mensajeFallido + mjeIntentosFallido + user.IntentosFallidos + advertenciaBloqueo);
                    else
                        MessageBox.Show("Inicio de sesión fallido: " + mjeContraseñaIncorrecta + mjeIntentosFallido + user.IntentosFallidos);
                    _registrarEventosService.RegistrarEvento(user.IdUsuario, user.Nombre + " " + user.Apellido + ": " + mjeContraseñaIncorrecta);
                    txtPass.Text = "";
                }
                if (!resultado.IsSuccessful && !resultado.IsBlocked  && !resultado.IsExist)
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
