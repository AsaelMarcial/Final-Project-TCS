using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace Gestor_de_Siniestros.Views
{

    public partial class ModificarUsuarioView : Window
    {
        DataBaseEntities DataBase;
        UsuariosService usuarioService;
        Usuarios usuarioActualizado;
        Usuarios currentUser;
        ConductorModel conductor;
        ObserverRespuesta notificacion;
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        public ModificarUsuarioView()
        {
            InitializeComponent();
            DataBase = new DataBaseEntities();
            usuarioService = new UsuariosService();
            usuarioActualizado = new Usuarios();
            conductor = new ConductorModel();
            currentUser = new Usuarios();
        }

        public ModificarUsuarioView(ObserverRespuesta notificacion) : this()
        {
            this.notificacion = notificacion;
        }

        internal void LoadData(Usuarios idSelected)
        {
            usuarioActualizado = idSelected;
            txtBoxEmail.Text = idSelected.email;
            txtBoxFLastName.Text = idSelected.aPaterno;
            txtBoxName.Text = idSelected.nombre;
            txtBoxPhone.Text = idSelected.celular.ToString();
            txtBoxPlaca.Text = idSelected.idLicencia;
            txtBoxSLastName.Text = idSelected.aMaterno;
            comboTipo.SelectedIndex = idSelected.tipoUsuario - 1;

        }

        private void Siguiente_Click(object sender, RoutedEventArgs e)
        {
            usuarioActualizado.nombre = txtBoxName.Text;
            usuarioActualizado.aPaterno = txtBoxFLastName.Text;
            usuarioActualizado.aMaterno = txtBoxSLastName.Text;
            if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxPhone.Text, "[^0-9]"))
            {
                MessageBox.Show("El campo Celular solo acepta numeros.");
                txtBoxPhone.Text = txtBoxPhone.Text.Remove(txtBoxPhone.Text.Length - 1);
            }
            else
            {
                usuarioActualizado.celular = Convert.ToInt64(txtBoxPhone.Text);
            }
            usuarioActualizado.email = txtBoxEmail.Text;
            usuarioActualizado.idLicencia = txtBoxPlaca.Text;
            if (comboTipo.SelectedIndex == 0)
            {
                usuarioActualizado.tipoUsuario = 1;
            }
            if (comboTipo.SelectedIndex == 1)
            {
                usuarioActualizado.tipoUsuario = 2;
            }
            if (comboTipo.SelectedIndex == 2)
            {
                usuarioActualizado.tipoUsuario = 3;
            }
            if (comboTipo.SelectedIndex == 3)
            {
                usuarioActualizado.tipoUsuario = 4;
            }
            if (comboTipo.SelectedIndex == 4)
            {
                usuarioActualizado.tipoUsuario = 5;
            }
            if (comboTipo.SelectedIndex == 5)
            {
                usuarioActualizado.tipoUsuario = 6;
            }

            if(usuarioService.Update(usuarioActualizado) == false)
            {
                MessageBox.Show("Error Al modificar usuario.");

            }
            else
            {
                MessageBox.Show("Usuario modificado correctamente.");
                notificacion.actualizaInformacion("Clic en siguiente");
                this.Close();
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
