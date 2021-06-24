using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Gestor_de_Siniestros.Views
{
    public partial class VehiculosView : UserControl
    {
        Usuarios _currentUser;
        RegistroVehiculoView registro;
        List<Vehiculos> VehiculosList;
        VehiculosService vehiculosService;
        DataBaseEntities DataBase;

        public VehiculosView()
        {
            InitializeComponent();
            VehiculosList = new List<Vehiculos>();
            _currentUser = new Usuarios();
            vehiculosService = new VehiculosService();
            DataBase = new DataBaseEntities();
            
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {

            registro = new RegistroVehiculoView();
            registro.LoadUser(_currentUser.idUsuario);
            registro.ShowDialog();
            actualizar.Visibility = Visibility.Visible;
        }

        internal void LoadData(Usuarios currentUser)
        {
            var ObjVehiculo = new List<VehiculoModel>();
            _currentUser.idUsuario = currentUser.idUsuario;
            try
            {
                var vehiculos = vehiculosService.GetAll();
                foreach (var vehiculo in vehiculos)
                {
                    VehiculoModel currentVehiculo = new VehiculoModel();
                    currentVehiculo.id = vehiculo.idVehiculo;
                    currentVehiculo.Placa = vehiculo.placa;
                    currentVehiculo.Marca = vehiculo.marca;
                    currentVehiculo.Modelo = vehiculo.modelo;
                    currentVehiculo.Año = vehiculo.año;
                    currentVehiculo.Color = vehiculo.color;
                    currentVehiculo.Aseguradora = vehiculo.nombreAseguradora;
                    ObjVehiculo.Add(currentVehiculo);
                }
                dgVehiculos.ItemsSource = ObjVehiculo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        private void actualizar_Click(object sender, RoutedEventArgs e)
        {
            LoadData(_currentUser);
            actualizar.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
