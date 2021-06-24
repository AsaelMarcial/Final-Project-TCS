using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gestor_de_Siniestros.Views
{
    public partial class ReporteView : UserControl, ObserverRespuesta
    {
        RegistroReporteView registroView;
        Usuarios _currentUser;
        DataBaseEntities DataBase;
        VerReporteView verReporte;
        ReportesService reporteService;
        List<ReporteModel> reportes;

        public ReporteView()
        {
            InitializeComponent();
            _currentUser = new Usuarios();
            DataBase = new DataBaseEntities();
            reporteService = new ReportesService();
            reportes = new List<ReporteModel>();

        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            registroView = new RegistroReporteView(this);
            registroView.LoadData(_currentUser);
            registroView.ShowDialog();
        }

        internal void LoadData(Usuarios currentUser)
        {          
            _currentUser = currentUser;
            if (currentUser.tipoUsuario == 6)
            {
                btnNuevoReporte.Visibility = Visibility.Visible;
            }
            
            try
            {
                var ListaReportes = reporteService.GetAll();

                foreach (var report in ListaReportes)
                {
                    ReporteModel currentReporte = new ReporteModel();
                    DateTime fecha = report.fechaReporte;
                    currentReporte.id = report.idReporte;
                    currentReporte.Fecha = fecha.ToString("MMMM dd, yyyy");
                    currentReporte.Direccion = report.direccion;
                    currentReporte.Creador = report.creador;
                    currentReporte.Estado = DataBase.Dictamenes.Where(e => e.idDictamen == report.idDictamen).Select(x => x.estado).FirstOrDefault();
                    reportes.Add(currentReporte);
                }
                dgReportes.ItemsSource = reportes;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String texto = textBusqueda.Text;
            var listaFiltrada = new List<ReporteModel>();
            if (textBusqueda.Text.Length == 0)
            {
                dgReportes.ItemsSource = null;
                dgReportes.ItemsSource = reportes;
            }
            else
            {
                foreach (var reporte in reportes)
                {
                    if (reporte.Direccion.ToLower().Contains(textBusqueda.Text.ToLower()))
                    {
                        listaFiltrada.Add(reporte);
                    }
                }
                dgReportes.ItemsSource = null;
                dgReportes.ItemsSource = listaFiltrada;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReporteModel idSelecteReporte = (ReporteModel)dgReportes.SelectedItem;
            if(idSelecteReporte == null)
            {
                MessageBox.Show("Selecciona elemento de la lista.");
            }
            else
            {
                verReporte = new VerReporteView();
                verReporte.LoadData(idSelecteReporte.id, _currentUser);
                verReporte.ShowDialog();

            }

        }

        public void actualizaInformacion(string operacion)
        {
            reportes.Clear();
            dgReportes.ItemsSource = null;
            LoadData(_currentUser);
        }
    }
}
