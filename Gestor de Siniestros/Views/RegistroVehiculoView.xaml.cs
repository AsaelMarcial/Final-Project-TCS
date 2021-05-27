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
using System.Windows.Shapes;

namespace Gestor_de_Siniestros.Views
{
    public partial class RegistroVehiculoView : Window
    {
        int _idDueño;
        Vehiculos currentVehiculo;
        VehiculosService vehiculosService;

        public RegistroVehiculoView()
        {
            InitializeComponent();
            vehiculosService = new VehiculosService();
            currentVehiculo = new Vehiculos();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        internal void LoadUser(Usuarios currentUser)
        {
            _idDueño = currentUser.idUsuario;
        }

        private void Siguiente_Click(object sender, RoutedEventArgs e)
        {
            String propiedad = ComboTipoPropiedad.SelectedValue.ToString();
            String marca = txtBoxMarca.Text;
            String modelo = txtBoxModelo.Text;
            int año = Convert.ToInt32(txtBoxAño.Text);
            String color = txtBoxColor.Text;
            String aseguradora = txtBoxAseguradora.Text;
            String poliza = txtBoxPoliza.Text;
            String placa = txtBoxPlaca.Text;
            int idDueño = _idDueño;

            currentVehiculo.tipoPropiedad = propiedad;
            currentVehiculo.marca = marca;
            currentVehiculo.modelo = modelo;
            currentVehiculo.año = año;
            currentVehiculo.color = color;
            currentVehiculo.nombreAseguradora = aseguradora;
            currentVehiculo.idPoliza = poliza;
            currentVehiculo.placa = placa;
            currentVehiculo.idDueño = _idDueño;

            vehiculosService.Add(currentVehiculo);
            this.Close();
            MessageBox.Show("Vehiculo registrado correctamente.");
        }


    }
}
