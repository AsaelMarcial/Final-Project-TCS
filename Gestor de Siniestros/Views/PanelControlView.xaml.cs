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

        public PanelControlView()
        {
            InitializeComponent();
            userService = new UsuariosService();
            DataBase = new DataBaseEntities();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                var usuarios = DataBase.Usuarios.Select(v => new { v.idUsuario, v.nombre, v.aPaterno, v.aMaterno, v.celular, v.idLicencia, v.tipoUsuario }).ToList();
                dgUsuarios.ItemsSource = usuarios;
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
    }
}
