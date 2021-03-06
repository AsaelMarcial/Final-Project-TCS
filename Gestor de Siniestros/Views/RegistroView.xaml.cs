using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Gestor_de_Siniestros.Views
{
    public partial class RegistroView : Window
    {
        Usuarios currentUser;
        UsuariosService userService;
        DataBaseEntities DataBase;
        ObserverRespuesta notificacion;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public RegistroView()
        {
            InitializeComponent();
            currentUser = new Usuarios();
            userService = new UsuariosService();
            DataBase = new DataBaseEntities();
        }

        public RegistroView(ObserverRespuesta notificacion) : this()
        {
            this.notificacion = notificacion;
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
            DateTime fechaNacimiento = dateBirth.SelectedDate.Value;
            String celular;
            String email = txtBoxEmail.Text;
            String licencia = txtBoxPlaca.Text;
            if(DataBase.Usuarios.Where(u => u.usuario == txtBoxUsername.Text).Any() == true){
                MessageBox.Show("El nombre de usuario ya existe.");
            }
            else
            {
                String usuario = txtBoxUsername.Text;
                String contraseña = pswBoxPassword.Password;


                if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxPhone.Text, "[^0-9]"))
                {
                    MessageBox.Show("El campo Celular solo acepta numeros.");
                    txtBoxPhone.Text = txtBoxPhone.Text.Remove(txtBoxPhone.Text.Length - 1);
                }
                else
                {
                    celular = txtBoxPhone.Text;
                    currentUser.celular = Convert.ToInt64(celular);
                }

                if (nombre.Length > 0 && aPaterno.Length > 0 && email.Length > 0 && usuario.Length > 0 && contraseña.Length > 0)
                {
                    currentUser.nombre = nombre;
                    currentUser.aPaterno = aPaterno;
                    currentUser.aMaterno = aMaterno;
                    currentUser.fechaNacimiento = fechaNacimiento;
                    currentUser.email = email;
                    currentUser.usuario = usuario;
                    currentUser.contraseña = contraseña;
                    currentUser.idLicencia = licencia;
                    currentUser.tipoUsuario = 3;
                    userService.Add(currentUser);
                    MessageBox.Show("Usuario registrado correctamente.");
                    notificacion.actualizaInformacion("Click en Siguiente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Verificar campos vacios.");
                }
            }
           
   
        }
    }
}
