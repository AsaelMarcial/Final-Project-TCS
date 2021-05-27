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
    /// Lógica de interacción para VehiculosView.xaml
    /// </summary>
    public partial class VehiculosView : UserControl
    {
        Usuarios _currentUser;
        RegistroVehiculoView registro;

        public VehiculosView()
        {
            InitializeComponent();
            _currentUser = new Usuarios();
            
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {

            registro = new RegistroVehiculoView();
            registro.LoadUser(_currentUser);
            registro.ShowDialog();
        }

        internal void LoadData(Usuarios currentUser)
        {
            _currentUser.idUsuario = currentUser.idUsuario;

        }
    }
}
