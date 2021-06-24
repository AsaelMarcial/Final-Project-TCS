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
    /// <summary>
    /// Lógica de interacción para VerUsuarioView.xaml
    /// </summary>
    public partial class VerUsuarioView : Window
    {

        DataBaseEntities DataBase;
        VehiculosService vehiculoService;
        ReportesService reporteService;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public VerUsuarioView()
        {
            InitializeComponent();
            DataBase = new DataBaseEntities();
            vehiculoService = new VehiculosService();
            reporteService = new ReportesService();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        internal void LoadData(ConductorModel idSelectedUser)
        {
            var delegacion = DataBase.Usuarios.Where(u => u.idUsuario == idSelectedUser.id).FirstOrDefault();

            txtFecha.Text = idSelectedUser.Fecha;
            txtBoxNombre.Text = idSelectedUser.Nombre + " " + idSelectedUser.Apellido;
            txtBoxLicencia.Text = idSelectedUser.Licencia;
            txtBoxDelegacion.Text = DataBase.Delegaciones.Where(d => d.idDelegacion == delegacion.delegacion).Select(s => s.nombre).FirstOrDefault();
            txtBoxTelefono.Text = idSelectedUser.Celular;
            txtBoxCorreo.Text = delegacion.email;

            var ObjVehiculo = new List<VehiculoModel>();
            try
            {
                var vehiculos = vehiculoService.GetAll();
                foreach (var vehiculo in vehiculos)
                {
                    if(vehiculo.idDueño == idSelectedUser.id)
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
                   
                }
                dgVehiculos.ItemsSource = ObjVehiculo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

            var reportes = new List<ReporteModel>();
            try
            {
                var reportesUsuario = DataBase.ReportesUsuarios.Where(u => u.idUsuario == idSelectedUser.id).ToList();

                foreach (var report in reportesUsuario)
                {
                    DateTime fecha = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.fechaReporte).FirstOrDefault();
                    ReporteModel currentReporte = new ReporteModel();
                    currentReporte.Fecha = fecha.ToString("MMMM dd, yyyy");
                    currentReporte.Direccion = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.direccion).FirstOrDefault();
                    currentReporte.Creador = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.creador).FirstOrDefault();
                    reportes.Add(currentReporte);
                }
                dgReportes.ItemsSource = reportes;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void LoadData(int idUsuario)
        {
            var idSelectedUser = new Usuarios();
            idSelectedUser = DataBase.Usuarios.Where(x => x.idUsuario == idUsuario).FirstOrDefault();

            var delegacion = DataBase.Usuarios.Where(u => u.idUsuario == idSelectedUser.idUsuario).FirstOrDefault();

            txtFecha.Text = idSelectedUser.fechaNacimiento.ToString("MMMM dd, yyyy");
            txtBoxNombre.Text = idSelectedUser.nombre + " " + idSelectedUser.aPaterno;
            txtBoxLicencia.Text = idSelectedUser.idLicencia;
            txtBoxDelegacion.Text = DataBase.Delegaciones.Where(d => d.idDelegacion == delegacion.delegacion).Select(s => s.nombre).FirstOrDefault();
            txtBoxTelefono.Text = idSelectedUser.celular.ToString();
            txtBoxCorreo.Text = delegacion.email;

            var ObjVehiculo = new List<VehiculoModel>();
            try
            {
                var vehiculos = vehiculoService.GetAll();
                foreach (var vehiculo in vehiculos)
                {
                    if (vehiculo.idDueño == idSelectedUser.idUsuario)
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

                }
                dgVehiculos.ItemsSource = ObjVehiculo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

            var reportes = new List<ReporteModel>();
            try
            {
                var reportesUsuario = DataBase.ReportesUsuarios.Where(u => u.idUsuario == idSelectedUser.idUsuario).ToList();

                foreach (var report in reportesUsuario)
                {
                    DateTime fecha = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.fechaReporte).FirstOrDefault();
                    ReporteModel currentReporte = new ReporteModel();
                    currentReporte.Fecha = fecha.ToString("MMMM dd, yyyy");
                    currentReporte.Direccion = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.direccion).FirstOrDefault();
                    currentReporte.Creador = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.creador).FirstOrDefault();
                    reportes.Add(currentReporte);
                }
                dgReportes.ItemsSource = reportes;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
