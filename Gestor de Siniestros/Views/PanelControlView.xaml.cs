using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestor_de_Siniestros.Views
{

    public partial class PanelControlView : UserControl, ObserverRespuesta
    {
        UsuariosService userService;
        DataBaseEntities DataBase;
        List<Usuarios> ObjUsuarios;

        public PanelControlView()
        {
            InitializeComponent();
            userService = new UsuariosService();
            DataBase = new DataBaseEntities();
            ObjUsuarios = new List<Usuarios>();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                ObjUsuarios = userService.GetAll();

                dgUsuarios.ItemsSource = ObjUsuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            RegistroView registro = new RegistroView(this);
            registro.ShowDialog();
        }

        private void actualizar_Click(object sender, RoutedEventArgs e)
        {
            Usuarios idSelected = (Usuarios)dgUsuarios.SelectedItem;
            ModificarUsuarioView modificarView = new ModificarUsuarioView(this);
            if (idSelected == null)
            {
                MessageBox.Show("Selecciona elemento de la lista.");
            }
            else
            {
                modificarView.LoadData(idSelected);
                modificarView.ShowDialog();
            }
        }


        private void visualizar_Click(object sender, RoutedEventArgs e)
        {
            Usuarios idSelectedUser = (Usuarios)dgUsuarios.SelectedItem;
            var usuarioEnvio = new ConductorModel();
            usuarioEnvio.Apellido = idSelectedUser.aPaterno + " " + idSelectedUser.aMaterno;
            usuarioEnvio.Celular = idSelectedUser.celular.ToString();
            usuarioEnvio.Nombre = idSelectedUser.nombre;
            usuarioEnvio.id = idSelectedUser.idUsuario;
            usuarioEnvio.Fecha = idSelectedUser.fechaNacimiento.ToString("MMMM dd, yyyy");
            usuarioEnvio.Licencia = idSelectedUser.idLicencia;

            VerUsuarioView usuarioView = new VerUsuarioView();
            if (idSelectedUser == null)
            {
                MessageBox.Show("Selecciona elemento de la lista.");

            }
            else
            {
                usuarioView.LoadData(usuarioEnvio);
                usuarioView.ShowDialog();
            }
        }

        public void actualizaInformacion(string operacion)
        {
            ObjUsuarios.Clear();
            dgUsuarios.ItemsSource = null;
            LoadData();
        }
    }
}
