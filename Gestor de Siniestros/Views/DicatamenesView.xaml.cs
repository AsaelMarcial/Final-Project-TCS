using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gestor_de_Siniestros.Views
{

    public partial class DicatamenesView : UserControl, ObserverRespuesta
    {

        RegistroReporteView registroView;
        Usuarios _currentUser;
        DataBaseEntities DataBase;
        VerReporteView verReporte;
        ReportesService reporteService;
        List<ReporteModel> reportes;

        public DicatamenesView()
        {
            InitializeComponent();
            _currentUser = new Usuarios();
            DataBase = new DataBaseEntities();
            reporteService = new ReportesService();
            reportes = new List<ReporteModel>();
        }

        internal void LoadData(Usuarios currentUser)
        {
            
            _currentUser = currentUser;
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
                    if (currentReporte.Estado.Equals("Pendiente"))
                    {
                        reportes.Add(currentReporte);
                    }
                }
                dgReportes.ItemsSource = reportes;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReporteModel idSelecteReporte = (ReporteModel)dgReportes.SelectedItem;
            verReporte = new VerReporteView(this);
            verReporte.LoadData(idSelecteReporte.id, _currentUser);
            verReporte.ShowDialog();

        }

        public void actualizaInformacion(string operacion)
        {
            reportes.Clear();
            dgReportes.ItemsSource = null;
            LoadData(_currentUser);
        }
    }
}
