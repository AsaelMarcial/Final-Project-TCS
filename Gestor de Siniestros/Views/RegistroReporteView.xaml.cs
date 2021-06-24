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
    public partial class RegistroReporteView : Window
    {
        private DataBaseEntities DataBase;
        DelegacionesService delegacionesService;
        List<byte[]> ObjImagesList;
        List<VehiculoModel> ObjVehiculosList;
        List<Usuarios> ObjUsuariosList;
        List<ImagenService> images;
        Usuarios _currentUser;
        ReportesService reportesService;
        ObserverRespuesta notificacion;

        public RegistroReporteView()
        {
            InitializeComponent();
            DataBase = new DataBaseEntities();
            delegacionesService = new DelegacionesService();
            reportesService = new ReportesService();
            ObjImagesList = new List<byte[]>();
            images = new List<ImagenService>();
            ObjVehiculosList = new List<VehiculoModel>();
            _currentUser = new Usuarios();
            ObjUsuariosList = new List<Usuarios>();
            LoadCombo();
        }

        public RegistroReporteView(ObserverRespuesta notificacion) : this()
        {
            this.notificacion = notificacion;
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
                oUser = DataBase.Usuarios.Where(x => x.idUsuario == conductor.idDueño).FirstOrDefault(); ;
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
                notificacion.actualizaInformacion("Clic en siguiente");
                this.Close();
            }
        }

        private void AgregarFoto_Click(object sender, RoutedEventArgs e)
        {
            ImagenService currentImage;
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.InitialDirectory = "C:\\Users\\asael\\Imágenes\\Siniestros";
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
                String fileName = fileChooser.SafeFileName;
                currentImage = new ImagenService(fileName, fileUbication, imageFile);
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
                    VehiculoModel current = new VehiculoModel();
                    current.id = vehiculo.idVehiculo;
                    current.Placa = vehiculo.placa;
                    current.Marca = vehiculo.marca;
                    current.Modelo = vehiculo.modelo;
                    current.Año = vehiculo.año;
                    current.Color = vehiculo.color;
                    current.Aseguradora = vehiculo.nombreAseguradora;
                    current.idDueño = vehiculo.idDueño;
                    ObjVehiculosList.Add(current);
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

        private void btnEliminarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            int currentVehiculo = dgVehiculos.SelectedIndex;
            ObjVehiculosList.RemoveAt(currentVehiculo);
            updateData();
        }

        private void btnEliminarImagen_Click(object sender, RoutedEventArgs e)
        {
            int currentImage = dgImages.SelectedIndex;
            images.RemoveAt(currentImage);
            updateData();
        }
    }
}
