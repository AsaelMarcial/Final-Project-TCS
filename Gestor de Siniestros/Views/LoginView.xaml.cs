using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Gestor_de_Siniestros
{

    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            using (DataBaseEntities DB = new DataBaseEntities())
            {
                String usuario = txtBoxUser.Text;
                String pass = txtBoxPassword.Password;
                if (usuario.Length > 0 && pass.Length > 0)
                {
                    try
                    {
                        Boolean userExist = DB.Usuarios.Where(usr => usr.usuario == usuario).Any();
                        if (userExist == true)
                        {
                            Usuarios currentUser = DB.Usuarios.Where(usr => usr.usuario == usuario).First();
                            if (currentUser.contraseña == pass)
                            {                                
                                MessageBox.Show("Contraseña Correcta, bienvenido: " + currentUser.nombre);
                                HomeView homeView = new HomeView();
                                homeView.LoadData(currentUser);
                                this.Close();
                                homeView.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Contraseña Incorrecta");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El usuario ingresado no existe");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Revisar campos vacios!");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
