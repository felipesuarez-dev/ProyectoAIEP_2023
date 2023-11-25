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
using Presentacion.Helpers;
using Servicios;

namespace Presentacion
{
    public partial class Index : Form
    {
        private readonly Db db;
        private readonly IniciarSesionService _iniciarSesionService;
        private readonly RegistrarEventosService _registrarEventosService;
        private readonly UsuarioService _usuarioService;
        private readonly MenuService _menuService;

        public Index(int usuarioId)
        {
            InitializeComponent();
            db = new Db();
            _iniciarSesionService = new IniciarSesionService(db.ObtenerConexion());
            _registrarEventosService = new RegistrarEventosService(db.ObtenerConexion());
            _usuarioService = new UsuarioService(db.ObtenerConexion());
            _menuService = new MenuService(db.ObtenerConexion());
            //
            //
            //Capturar datos del usuario
            Usuario user = new Usuario();
            user = _usuarioService.ObtenerDatosUsuarioPorID(usuarioId);
            txtUserName.Text = user.Nombre;
            txtUserId.Visible = false;
        }

        private void Index_Load(object sender, EventArgs e)
        {
            var userId = txtUserId.Text;
            DibujarMenu();

        }

        private void DibujarMenu()
        {

            //capturar dato usuario
            int IdUsuario = int.Parse(txtUserId.Text);

            DataTable dtMenu = new DataTable();
            dtMenu = _menuService.ConsultarMenu(IdUsuario);

            // dibujar menu con los datos de la base de datyos
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem menuItem;
            ToolStripMenuItem subMenuItem;

            for (int i = 0; i < dtMenu.Rows.Count; i++) // recorre los menu
            {

                int idMenu = int.Parse(dtMenu.Rows[i]["id_menu"].ToString());
                string tituloMenu = dtMenu.Rows[i]["nombre_menu"].ToString();

                menuItem = new ToolStripMenuItem(tituloMenu);
                //menuItem.Tag = "Codigo" + idMenu.ToString();

                DataTable dtSubMenu = new DataTable();
                dtSubMenu = _menuService.ConsultarSubmenu(IdUsuario, idMenu);
                for (int j = 0; j < dtSubMenu.Rows.Count; j++) // recorre submenu 
                {
                    int idSubMenu = int.Parse(dtSubMenu.Rows[j]["id_menu"].ToString());
                    string tituloSubMenu = dtSubMenu.Rows[j]["nombre_menu"].ToString();

                    subMenuItem = new ToolStripMenuItem(tituloSubMenu);
                    subMenuItem.Click += subMenu_Click;
                    subMenuItem.Tag = dtSubMenu.Rows[j]["libreria"].ToString() + "-" + dtSubMenu.Rows[j]["componentes"].ToString();
                    menuItem.DropDownItems.Add(subMenuItem);
                }

                menu.Items.Add(menuItem);
                Controls.Add(menu);
            }
        }

        private void subMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem subMenu = sender as ToolStripMenuItem;
            //MessageBox.Show("Codigo subMenu es:" + subMenu.Tag);

            Control ctrGUI;
            string[] parametros = subMenu.Tag.ToString().Split('-');

            string libreria = parametros[0];
            string componente = parametros[1];

            ctrGUI = SmartControl.LoadSmartControl(libreria, componente);
            //ctrGUI.SuspendLayout();
            ctrGUI.BackColor = Color.White;
            //ctrGUI.ResumeLayout();

            Form f1 = new Form();
            f1 = ctrGUI as Form;
            f1.MdiParent = this;
            f1.Show();

            //var hw = radDockPrincipal.DockControl(ctrGUI, PosicionVentana, TipoVentana);
            //hw.CloseAction = Telerik.WinControls.UI.Docking.DockWindowCloseAction.CloseAndDispose;
            //this.ResumeLayout();
            //this.Cursor = Cursors.Default;

        }

    }
}