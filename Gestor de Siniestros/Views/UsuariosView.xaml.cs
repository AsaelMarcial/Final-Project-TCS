using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Gestor_de_Siniestros.Views
{
    public partial class UsuariosView : UserControl, ObserverRespuesta
    {
        Usuarios _currentUser;
        RegistroView registroView;
        List<Usuarios> UsuariosList;
        UsuariosService usuariosService;
        RegistroVehiculoView registroVehiculo;
        List<ConductorModel> ObjUsuarios;

        public UsuariosView()
        {
            InitializeComponent();
            _currentUser = new Usuarios();
            UsuariosList = new List<Usuarios>();
            usuariosService = new UsuariosService();
            ObjUsuarios = new List<ConductorModel>();
            LoadData();
        }

        internal void LoadData()
        {
            try
            {
                var usuarios = usuariosService.GetAll();
                foreach (var usuario in usuarios)
                {
                    DateTime fecha = usuario.fechaNacimiento;
                    ConductorModel conductor = new ConductorModel();
                    conductor.id = usuario.idUsuario;
                    conductor.Nombre = usuario.nombre;
                    conductor.Apellido = usuario.aPaterno + " " + usuario.aMaterno;
                    conductor.Fecha = fecha.ToString("MMMM dd, yyyy");
                    conductor.Licencia = usuario.idLicencia;
                    conductor.Celular = usuario.celular.ToString();
                    ObjUsuarios.Add(conductor);
                }
                dgConductores.ItemsSource = ObjUsuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            registroView = new RegistroView(this);
            registroView.ShowDialog();
        }

        private void RegistroVehiculo_Click(object sender, RoutedEventArgs e)
        {
            ConductorModel idSelecterUser = (ConductorModel)dgConductores.SelectedItem;
            if(idSelecterUser == null)
            {
                MessageBox.Show("Selecciona elemento de la lista.");
            }
            else
            {
                registroVehiculo = new RegistroVehiculoView();
                registroVehiculo.LoadUser(idSelecterUser.id);
                registroVehiculo.Show();
            }
        }

        public void actualizaInformacion(string operacion)
        {
            dgConductores.ItemsSource = null;
            this.LoadData();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            String texto = textBusqueda.Text;
            var listaFiltrada = new List<ConductorModel>();
            if (textBusqueda.Text.Length == 0)
            {
                dgConductores.ItemsSource = null;
                dgConductores.ItemsSource = ObjUsuarios;
            }
            else
            {
                foreach (var conductor in ObjUsuarios)
                {
                    if (conductor.Nombre.ToLower().Contains(textBusqueda.Text.ToLower())){
                        listaFiltrada.Add(conductor);
                    }
                }
                dgConductores.ItemsSource = null;
                dgConductores.ItemsSource = listaFiltrada; 
            }
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            ConductorModel idSelectedUser = (ConductorModel)dgConductores.SelectedItem;
            VerUsuarioView usuarioView = new VerUsuarioView();
            if(idSelectedUser == null)
            {
                MessageBox.Show("Selecciona elemento de la lista.");

            }
            else
            {
                usuarioView.LoadData(idSelectedUser);
                usuarioView.ShowDialog();
            }
            
        }
    }
}
