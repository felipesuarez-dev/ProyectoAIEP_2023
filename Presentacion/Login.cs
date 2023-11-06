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

namespace Presentacion
{
    public partial class Login : Form
    {
        private readonly Db db;
        private readonly IniciarSesion login;

        public Login()
        {
            InitializeComponent();
            db = new Db();
            login = new IniciarSesion(db.ObtenerConexion());
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            var resultado = login.Validacion(txtUser.Text, txtPass.Text);

            //Manejo de inicio de sesión según validación
            if (resultado.IsSuccessful == true)
            {
                MessageBox.Show("Inicio de sesión exitoso.");
                // TODO: arreglar abrir siguiente ventana
                Index indexForm = new Index();
                indexForm.Show();
                this.Close();
            }
            if (resultado.IsBlocked == true)
            {
                MessageBox.Show("La cuenta está bloqueada. Comuníquese con el soporte.");
                txtUser.Text = "";
                txtPass.Text = "";
            }
            if (resultado.IsSuccessful == false && resultado.IsBlocked == false)
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.");
                txtUser.Text = "";
                txtPass.Text = "";
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
