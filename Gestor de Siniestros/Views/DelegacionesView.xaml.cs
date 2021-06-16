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
    /// Lógica de interacción para DelegacionesView.xaml
    /// </summary>
    public partial class DelegacionesView : UserControl
    {
        RegistroDelegacionView registroDelegacionView;

        public DelegacionesView()
        {
            InitializeComponent();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            registroDelegacionView = new RegistroDelegacionView();
            registroDelegacionView.ShowDialog();
        }
    }
}
