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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        public ModificarUsuarioView()
        {

            DataBase = new DataBaseEntities();
            usuarioService = new UsuariosService();
            usuarioActualizado = new Usuarios();
            conductor = new ConductorModel();
            currentUser = new Usuarios();
            InitializeComponent();
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
            if (comboTipo.SelectedItem.Equals("Administrativo"))
            {
                usuarioActualizado.tipoUsuario = 1;
            }
            if (comboTipo.SelectedItem.Equals("Perito"))
            {
                usuarioActualizado.tipoUsuario = 2;
            }
            if (comboTipo.SelectedItem.Equals("Conductor"))
            {
                usuarioActualizado.tipoUsuario = 3;
            }
            if (comboTipo.SelectedItem.Equals("Soporte"))
            {
                usuarioActualizado.tipoUsuario = 4;
            }
            if (comboTipo.SelectedItem.Equals("Atención al cliente"))
            {
                usuarioActualizado.tipoUsuario = 5;
            }
            if (comboTipo.SelectedItem.Equals("Agente de transito"))
            {
                usuarioActualizado.tipoUsuario = 6;
            }

            if(usuarioService.Update(usuarioActualizado) == true)
            {
                MessageBox.Show("Usuario modificado correctamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Al modificar usuario.");
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        internal void LoadData(ConductorModel idSelected)
        {            
            /*
            txtBoxEmail.Text = idSelected.email;
            txtBoxFLastName.Text = idSelected.aPaterno;
            txtBoxName.Text = idSelected.nombre;
            txtBoxPhone.Text = idSelected.celular.ToString();
            txtBoxPlaca.Text = idSelected.idLicencia;
            txtBoxSLastName.Text = idSelected.aMaterno;
            comboTipo.SelectedIndex = idSelected.tipoUsuario - 1;*/

        }
    }
}
