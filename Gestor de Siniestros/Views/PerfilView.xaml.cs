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
    /// Lógica de interacción para PerfilView.xaml
    /// </summary>
    public partial class PerfilView : UserControl
    {
        public PerfilView()
        {
            InitializeComponent();
        }

        public void LoadData(Usuarios currentUser)
        {
            txtBoxName.Text = currentUser.nombre;
            txtBoxAPaterno.Text = currentUser.aPaterno;
            txtBoxAMaterno.Text = currentUser.aMaterno;
            dateFechaNac.Text = Convert.ToString(currentUser.fechaNacimiento);
            txtBoxLicencia.Text = currentUser.idLicencia;
            txtBoxEmail.Text = currentUser.email;
            txtBoxPhone.Text = Convert.ToString(currentUser.celular);

        }
    }
}
