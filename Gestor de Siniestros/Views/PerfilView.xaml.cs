using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gestor_de_Siniestros.Views
{
    public partial class PerfilView : UserControl
    {
        LoginView loginView;
        DelegacionesService delegacionesService;
        int idSelectedUser;
        DataBaseEntities DataBase;
        UsuariosService UsuariosService;

        public PerfilView()
        {
            InitializeComponent();
            delegacionesService = new DelegacionesService();
            DataBase = new DataBaseEntities();
            UsuariosService = new UsuariosService();
            LoadCombo();
        }

        public void LoadCombo()
        {
            comboBoxDelegacion.ItemsSource = delegacionesService.GetDelegacionesNames();
        }


        public void LoadData(Usuarios currentUser)
        {
            txtBoxName.Text = currentUser.nombre;
            txtBoxAPaterno.Text = currentUser.aPaterno;
            txtBoxAMaterno.Text = currentUser.aMaterno;
            dateFechaNac.Text = currentUser.fechaNacimiento.ToString("MMMM dd, yyyy");
            txtBoxLicencia.Text = currentUser.idLicencia;
            txtBoxEmail.Text = currentUser.email;
            txtBoxPhone.Text = Convert.ToString(currentUser.celular);
            if (currentUser.delegacion.HasValue)
            {
                comboBoxDelegacion.SelectedIndex = currentUser.delegacion.Value - 1;
            }
            idSelectedUser = currentUser.idUsuario;
        }

        private void Click_Modificar(object sender, RoutedEventArgs e)
        {
            modificar.Visibility = Visibility.Hidden;
            guardar.Visibility = Visibility.Visible;
            cancelar.Visibility = Visibility.Visible;
            comboBoxDelegacion.IsEnabled = true;
            txtBoxEmail.IsReadOnly = false;
            txtBoxPhone.IsReadOnly = false;
            mensajeEstado.Content = "";
            
        }

        private void Click_Cancelar(object sender, RoutedEventArgs e)
        {
            modificar.Visibility = Visibility.Visible;
            guardar.Visibility = Visibility.Hidden;
            cancelar.Visibility = Visibility.Hidden;
            comboBoxDelegacion.IsEnabled = false;
            txtBoxEmail.IsReadOnly = true;
            txtBoxPhone.IsReadOnly = true;
            MessageBox.Show("Información no actualizada, vuelva a iniciar sesion.");
            var window = Window.GetWindow(this);
            window.Close();
            loginView = new LoginView();
            loginView.Show();
        }

        private void Click_Guardar(object sender, RoutedEventArgs e)
        {
            var ObjUser = DataBase.Usuarios.Find(idSelectedUser);
            int idDelegacion = DataBase.Delegaciones.Where(u => u.nombre == comboBoxDelegacion.SelectedValue.ToString()).Select(u => u.idDelegacion).FirstOrDefault();
            ObjUser.delegacion = idDelegacion;
            ObjUser.email = txtBoxEmail.Text;
            if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxPhone.Text, "[^0-9]"))
            {
                MessageBox.Show("El campo Celular solo acepta numeros.");
                txtBoxPhone.Text = txtBoxPhone.Text.Remove(txtBoxPhone.Text.Length - 1);
            }
            else
            {
                ObjUser.celular = Convert.ToInt64(txtBoxPhone.Text);
            }
            if (UsuariosService.Update(ObjUser) == true){

                MessageBox.Show("Información actualizada!");
                txtBoxEmail.Text = ObjUser.email;
                txtBoxPhone.Text = Convert.ToString(ObjUser.celular);
                if (ObjUser.delegacion.HasValue)
                {
                    comboBoxDelegacion.SelectedIndex = ObjUser.delegacion.Value - 1;
                }
                modificar.Visibility = Visibility.Visible;
                guardar.Visibility = Visibility.Hidden;
                cancelar.Visibility = Visibility.Hidden;
                comboBoxDelegacion.IsEnabled = false;
                txtBoxEmail.IsReadOnly = true;
                txtBoxPhone.IsReadOnly = true;

            }
            
        }


    }
}
