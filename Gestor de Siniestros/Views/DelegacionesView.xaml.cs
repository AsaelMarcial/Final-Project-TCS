using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System.Windows;
using System.Windows.Controls;

namespace Gestor_de_Siniestros.Views
{

    public partial class DelegacionesView : UserControl, ObserverRespuesta
    {
        RegistroDelegacionView registroDelegacionView;
        DataBaseEntities DataBase;
        DelegacionesService delegacionesService;

        public DelegacionesView()
        {
            DataBase = new DataBaseEntities();
            delegacionesService = new DelegacionesService();
            
            InitializeComponent();
            LoadData();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            registroDelegacionView = new RegistroDelegacionView(this);
            registroDelegacionView.ShowDialog();
        }

        public void LoadData()
        {
            dgDelegaciones.ItemsSource = delegacionesService.GetAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Delegaciones idSelected = (Delegaciones)dgDelegaciones.SelectedItem;
            ModificarDelegacionView modificarDelegacionView = new ModificarDelegacionView(this);
            if (idSelected == null)
            {
                MessageBox.Show("Selecciona elemento de la lista.");

            }
            else
            {
                modificarDelegacionView.LoadData(idSelected);
                modificarDelegacionView.ShowDialog();
            }
            
        }

        public void actualizaInformacion(string operacion)
        {
            dgDelegaciones.ItemsSource = null;
            LoadData();
        }
    }
}
