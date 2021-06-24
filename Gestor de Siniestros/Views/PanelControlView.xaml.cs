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

    public partial class PanelControlView : UserControl
    {
        UsuariosService userService;
        DataBaseEntities DataBase;
        List<ConductorModel> ObjUsuarios;

        public PanelControlView()
        {
            InitializeComponent();
            userService = new UsuariosService();
            DataBase = new DataBaseEntities();
            ObjUsuarios = new List<ConductorModel>();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                var usuarios = userService.GetAll();
                foreach (var usuario in usuarios)
                {
                    DateTime fecha = usuario.fechaNacimiento;
                    ConductorModel conductor = new ConductorModel();
                    conductor.id = usuario.idUsuario;
                    conductor.Nombre = usuario.nombre;
                    conductor.Apellido = usuario.aPaterno + " " + usuario.aMaterno;
                    conductor.Fecha = fecha.ToString("MMMM dd, yyyy");
                    conductor.Licencia = usuario.idLicencia;
                    conductor.Celular = usuario.celular.ToString();
                    ObjUsuarios.Add(conductor);
                }
                dgUsuarios.ItemsSource = ObjUsuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            RegistroView registro = new RegistroView();
            registro.ShowDialog();
        }

        private void actualizar_Click(object sender, RoutedEventArgs e)
        {
            ConductorModel idSelected = (ConductorModel)dgUsuarios.SelectedItem;
            ModificarUsuarioView modificarView = new ModificarUsuarioView();
            if (idSelected == null)
            {
                MessageBox.Show("Selecciona elemento de la lista.");
            }
            else
            {
                modificarView.LoadData(idSelected.id);
                modificarView.ShowDialog();
            }
        }


        private void visualizar_Click(object sender, RoutedEventArgs e)
        {
            ConductorModel idSelectedUser = (ConductorModel)dgUsuarios.SelectedItem;
            VerUsuarioView usuarioView = new VerUsuarioView();
            if (idSelectedUser == null)
            {
                MessageBox.Show("Selecciona elemento de la lista.");

            }
            else
            {
                usuarioView.LoadData(idSelectedUser);
                usuarioView.ShowDialog();
            }
        }
    }
}
