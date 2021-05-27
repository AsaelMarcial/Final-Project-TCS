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
using System.Windows.Shapes;

namespace Gestor_de_Siniestros.Views
{
    /// <summary>
    /// Lógica de interacción para RegistroView.xaml
    /// </summary>
    public partial class RegistroView : Window
    {
        Usuarios currentUser;
        UsuariosService userService;

        public RegistroView()
        {
            InitializeComponent();
            currentUser = new Usuarios();
            userService = new UsuariosService();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Siguiente_Click(object sender, RoutedEventArgs e)
        {
            String nombre = txtBoxName.Text;
            String aPaterno = txtBoxFLastName.Text;
            String aMaterno = txtBoxSLastName.Text;
            //DateTime fechaNacimiento;
            String celular;
            String email = txtBoxEmail.Text;
            String usuario = txtBoxUsername.Text;
            String contraseña = pswBoxPassword.Password;


            if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxPhone.Text, "[^0-9]"))
            {
                MessageBox.Show("El campo Celular solo acepta numeros.");
                txtBoxPhone.Text = txtBoxPhone.Text.Remove(txtBoxPhone.Text.Length - 1);
            }else
            {
                celular = txtBoxPhone.Text;
                currentUser.celular = Convert.ToInt64(celular);
            }

            if (nombre.Length > 0 && aPaterno.Length > 0 && email.Length > 0 && usuario.Length > 0 && contraseña.Length > 0)
            {
                currentUser.nombre = nombre;
                currentUser.aPaterno = aPaterno;
                currentUser.aMaterno = aMaterno;
                currentUser.email = email;
                currentUser.usuario = usuario;
                currentUser.contraseña = contraseña;
                currentUser.tipoUsuario = 3;
                userService.Add(currentUser);
                MessageBox.Show("Usuario registrado correctamente.");
                this.Close();
            } else
            {
                MessageBox.Show("Verificar campos vacios.");
            }
   
        }

    }
}
