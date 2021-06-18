using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para ReporteView.xaml
    /// </summary>
    public partial class ReporteView : UserControl
    {
        RegistroReporteView registroView;
        Usuarios _currentUser;
        DataBaseEntities DataBase;
        VerReporteView verReporte;

        public ReporteView()
        {
            InitializeComponent();
            _currentUser = new Usuarios();
            DataBase = new DataBaseEntities();
            
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            registroView = new RegistroReporteView();
            registroView.LoadData(_currentUser);
            registroView.ShowDialog();
        }

        internal void LoadData(Usuarios currentUser)
        {
            var reportes = new List<ReporteCompletoService>();

            _currentUser = currentUser;
            if (currentUser.tipoUsuario != 3)
            {
                btnNuevoReporte.Visibility = Visibility.Visible;
            }
            
            try
            {
                var reportesUsuario = DataBase.ReportesUsuarios.Where(u => u.idUsuario == currentUser.idUsuario).ToList();

                foreach (var report in reportesUsuario)
                {
                    ReporteCompletoService currentReporte = new ReporteCompletoService();
                    currentReporte.IdReporte = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.idReporte).FirstOrDefault();
                    currentReporte.Direccion = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.direccion).FirstOrDefault();
                    currentReporte.FechaReporte = DataBase.Reportes.Where(r => r.idReporte == report.idReporte).Select(x => x.fechaReporte).FirstOrDefault();
                    reportes.Add(currentReporte);
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
            ReporteCompletoService idSelecteReporte = (ReporteCompletoService)dgReportes.SelectedItem;
            verReporte = new VerReporteView();
            verReporte.LoadData(idSelecteReporte.IdReporte);
            verReporte.ShowDialog();
            
        }

    }
}
