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
    /// Lógica de interacción para DicatamenesView.xaml
    /// </summary>
    public partial class DicatamenesView : UserControl
    {

        RegistroReporteView registroView;
        Usuarios _currentUser;
        DataBaseEntities DataBase;
        VerReporteView verReporte;
        ReportesService reporteService;

        public DicatamenesView()
        {
            InitializeComponent();
            _currentUser = new Usuarios();
            DataBase = new DataBaseEntities();
            reporteService = new ReportesService();
        }
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            registroView = new RegistroReporteView();
            registroView.LoadData(_currentUser);
            registroView.ShowDialog();
        }

        internal void LoadData(Usuarios currentUser)
        {
            var reportes = new List<ReporteModel>();
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
            verReporte = new VerReporteView();
            verReporte.LoadData(idSelecteReporte.id, _currentUser);
            verReporte.ShowDialog();

        }

    }
}
