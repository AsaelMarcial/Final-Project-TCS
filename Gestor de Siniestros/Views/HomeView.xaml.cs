﻿using Gestor_de_Siniestros.Models.DB;
using System.Windows;
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

        public HomeView()
        {
            InitializeComponent();
        }

        public void LoadData(Usuarios currentUser)
        {
            if(currentUser.tipoUsuario == 3)
            {
                delegaciones.Visibility = Visibility.Hidden;
                dicatamenes.Visibility = Visibility.Hidden;
            }
            if(currentUser.tipoUsuario == 2)
            {
                panelControl.Visibility = Visibility.Visible;
            }
            perfilView.LoadData(currentUser);
            vehiculosView.LoadData(currentUser);
            reporteView.LoadData(currentUser);
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
            Container.Child = reporteView;
        }

        private void BtnChat(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnDictamenes(object sender, RoutedEventArgs e)
        {
            
        }


        private void BtnDelegaciones(object sender, RoutedEventArgs e)
        {
            Container.Child = delegacionView;
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
