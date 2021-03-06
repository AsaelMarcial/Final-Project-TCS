using Gestor_de_Siniestros.Models.DB;
using System.Windows;
using System.Windows.Input;

namespace Gestor_de_Siniestros.Views
{
    public partial class HomeView : Window
    {
        LoginView login = new LoginView();
        Usuarios currentUser = new Usuarios();
        PerfilView perfilView = new PerfilView();
        VehiculosView vehiculosView = new VehiculosView();
        ReporteView reporteView = new ReporteView();
        DelegacionesView delegacionView = new DelegacionesView();
        ChatView chatView = new ChatView();
        DicatamenesView dicatamenesView = new DicatamenesView();
        PanelControlView panelControlView = new PanelControlView();
        UsuariosView usuariosView = new UsuariosView();

        public HomeView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public void LoadData(Usuarios currentUser)
        {
            if (currentUser.tipoUsuario == 1)
            {
                panelControl.Visibility = Visibility.Visible;
                vehiculos.Visibility = Visibility.Visible;
            }

            if (currentUser.tipoUsuario == 2)
            {
                dicatamenes.Visibility = Visibility.Visible;
            }

            if (currentUser.tipoUsuario == 4 || currentUser.tipoUsuario == 5)
            {
                delegaciones.Visibility = Visibility.Visible;
            }
            if(currentUser.tipoUsuario == 6)
            {
                conductores.Visibility = Visibility.Visible;
            }
            perfilView.LoadData(currentUser);
            vehiculosView.LoadData(currentUser);
            reporteView.LoadData(currentUser);
            chatView.LoadData(currentUser);
            usuariosView.LoadData();
            dicatamenesView.LoadData(currentUser);
            Container.Child = perfilView;
        }

        private void BtnPerfil(object sender, RoutedEventArgs e)
        {
            Container.Child = perfilView;
        }

        private void BtnConductores(object sender, RoutedEventArgs e)
        {
            Container.Child = usuariosView;
        }

        private void BtnVehiculos(object sender, RoutedEventArgs e)
        {
            Container.Child = vehiculosView;
        }

        private void BtnReportes(object sender, RoutedEventArgs e)
        {
            Container.Child = reporteView;
        }

        private void BtnChat(object sender, RoutedEventArgs e)
        {
            Container.Child = chatView;
        }

        private void BtnDictamenes(object sender, RoutedEventArgs e)
        {
            Container.Child = dicatamenesView;
        }

        private void BtnDelegaciones(object sender, RoutedEventArgs e)
        {
            Container.Child = delegacionView;
        }

        private void BtnPanelControl(object sender, RoutedEventArgs e)
        {
            Container.Child = panelControlView;
        }

        private void BtnSalir(object sender, RoutedEventArgs e)
        {            
            this.Close();
            login.ShowDialog();
        }

    }
}
