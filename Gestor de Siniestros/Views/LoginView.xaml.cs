using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Views;
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

namespace Gestor_de_Siniestros
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
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
                                switch (currentUser.tipoUsuario)
                                {
                                    case 1:
                                        HomeView homeView = new HomeView();
                                        homeView.LoadData(currentUser);
                                        this.Close();
                                        homeView.ShowDialog();
                                        break;
                                    case 2:
                                        Console.WriteLine("Case 2");
                                        break;
                                    default:
                                        
                                        break;
                                }
                                

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

        private void Registro_Click(object sender, RoutedEventArgs e)
        {
            RegistroView registro = new RegistroView();
            registro.ShowDialog();
        }
    }
}
