using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Gestor_de_Siniestros.Views
{
    /// <summary>
    /// Lógica de interacción para RegistroReporteView.xaml
    /// </summary>
    public partial class RegistroReporteView : Window
    {
        private DataBaseEntities DataBase;
        DelegacionesService delegacionesService;
        List<byte[]> ObjImagesList;
        List<Vehiculos> ObjVehiculosList;
        List<Usuarios> ObjUsuariosList;
        List<ImagenService> images;
        Usuarios _currentUser;
        ReportesService reportesService;


        public RegistroReporteView()
        {
            InitializeComponent();
            DataBase = new DataBaseEntities();
            delegacionesService = new DelegacionesService();
            reportesService = new ReportesService();
            ObjImagesList = new List<byte[]>();
            images = new List<ImagenService>();
            ObjVehiculosList = new List<Vehiculos>();
            _currentUser = new Usuarios();
            ObjUsuariosList = new List<Usuarios>();
            LoadCombo();
        }

        internal void LoadData(Usuarios currentUser)
        {
            _currentUser = currentUser;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void LoadCombo()
        {
            comboBoxDelegacion.ItemsSource = delegacionesService.GetDelegacionesNames();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Siguiente_Click(object sender, RoutedEventArgs e)
        {
            var currentReporte = new Reportes();
            currentReporte.direccion = txtBoxAddress.Text;
            currentReporte.creador = _currentUser.nombre + " " + _currentUser.aPaterno;

            foreach (var conductor in ObjVehiculosList)
            {
                var oUser = new Usuarios();
                oUser = DataBase.Usuarios.Where(u => u.idUsuario == conductor.idDueño).FirstOrDefault();
                ObjUsuariosList.Add(oUser);
            }
            int idDelegacion = DataBase.Delegaciones.Where(u => u.nombre == comboBoxDelegacion.SelectedValue.ToString()).Select(u => u.idDelegacion).FirstOrDefault();

            if(ObjImagesList.Count < 3)
            {
                MessageBox.Show("Seleccionar minimo 3 imagenes!");
            }
            else
            {
                reportesService.Add(currentReporte, ObjUsuariosList, ObjVehiculosList, ObjImagesList, idDelegacion);
                MessageBox.Show("Reporte registrado correctamente!");
                this.Close();

            }
        }

        private void AgregarFoto_Click(object sender, RoutedEventArgs e)
        {
            ImagenService currentImage;
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.InitialDirectory = "c:\\";
            fileChooser.Multiselect = false;
            fileChooser.Filter = "Todas las imagenes|*jpg;*png;*bmp|JPG|*jpg|PNG|*png|BMP|*bmp";
            fileChooser.FilterIndex = 1;
            fileChooser.RestoreDirectory = true;
            if(fileChooser.ShowDialog() == true)
            {
                byte[] imageFile = null;
                Stream myStream = fileChooser.OpenFile();
                using (MemoryStream ms = new MemoryStream())
                {
                    myStream.CopyTo(ms);
                    imageFile = ms.ToArray();
                }
                ObjImagesList.Add(imageFile);
                String fileUbication = fileChooser.FileName;
                currentImage = new ImagenService(fileUbication);
                images.Add(currentImage);
                updateData();
            }
            else
            {
                Console.WriteLine("Error al agregar imagen.");
            }
        }

        private void AgregarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vehiculo = new Vehiculos();
                vehiculo = DataBase.Vehiculos.Where(v => v.placa == txtBoxPlaca.Text).FirstOrDefault();
                if(vehiculo == null)
                {
                    MessageBox.Show("Verificar numero de placa.");
                }
                else
                {
                    ObjVehiculosList.Add(vehiculo);
                    txtBoxPlaca.Text = "";
                }

            }
            catch (Exception ex )
            {
                throw ex;
            }
            
            updateData();
        }

        private void updateData()
        {
            dgVehiculos.ItemsSource = null;
            dgImages.ItemsSource = null;
            dgVehiculos.ItemsSource = ObjVehiculosList;
            dgImages.ItemsSource = images;
        }

    }
}
