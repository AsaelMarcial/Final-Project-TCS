using Gestor_de_Siniestros.Models.DB;
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
    /// Lógica de interacción para ReporteView.xaml
    /// </summary>
    public partial class ReporteView : UserControl
    {
        RegistroReporteView registroView;
        Usuarios _currentUser;
        DataBaseEntities DataBase;

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
            _currentUser = currentUser;
            if (currentUser.tipoUsuario != 3)
            {
                btnNuevoReporte.Visibility = Visibility.Visible;
            }
        }
    }
}
