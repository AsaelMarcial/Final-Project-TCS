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
    /// <summary>
    /// Lógica de interacción para VehiculosView.xaml
    /// </summary>
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
            registro.LoadUser(_currentUser);
            registro.ShowDialog();
            actualizar.Visibility = Visibility.Visible;
        }

        internal void LoadData(Usuarios currentUser)
        {
            _currentUser.idUsuario = currentUser.idUsuario;
            try
            {
                var vehiculos = DataBase.Vehiculos.Where(v => v.idDueño == currentUser.idUsuario).Select(v => new { v.idVehiculo, v.placa, v.modelo, v.tipoPropiedad }).ToList();
                dgVehiculos.ItemsSource = vehiculos;
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
