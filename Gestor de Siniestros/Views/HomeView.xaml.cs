using Gestor_de_Siniestros.Models.DB;
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
using System.Windows.Shapes;

namespace Gestor_de_Siniestros.Views
{
    public partial class HomeView : Window
    {
        LoginView login = new LoginView();
        Usuarios currentUser = new Usuarios();
        PerfilView perfilView = new PerfilView();
        VehiculosView vehiculosView = new VehiculosView();
        public HomeView()
        {
            InitializeComponent();
        }

        public void LoadData(Usuarios currentUser)
        {
            perfilView.LoadData(currentUser);
            vehiculosView.LoadData(currentUser);
            Container.Child = perfilView;
        }

        private void BtnPerfil(object sender, RoutedEventArgs e)
        {

            Container.Child = perfilView;
        }

        private void BtnVehiculos(object sender, RoutedEventArgs e)
        {
            Container.Child = vehiculosView;
        }

        private void BtnReportes(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnChat(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDictamenes(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPanelControl(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSalir(object sender, RoutedEventArgs e)
        {            
            this.Close();
            login.ShowDialog();
        }
    }
}
