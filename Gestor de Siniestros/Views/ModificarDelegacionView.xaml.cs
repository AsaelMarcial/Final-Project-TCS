using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace Gestor_de_Siniestros.Views
{
    public partial class ModificarDelegacionView : Window
    {
        DelegacionesService delegacioneService;
        Delegaciones currentDelegacion;
        ObserverRespuesta notificacion;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public ModificarDelegacionView()
        {
            InitializeComponent();
            delegacioneService = new DelegacionesService();
            currentDelegacion = new Delegaciones();
        }

        public ModificarDelegacionView(ObserverRespuesta notificacion) : this()
        {
            this.notificacion = notificacion;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Siguiente_Click(object sender, RoutedEventArgs e)
        {
            String nombre = txtBoxNombre.Text;
            String calle = txtBoxCalle.Text;
            String numero = txtBoxNumero.Text;
            String colonia = txtBoxColonia.Text;
            String codigoPostal = txtBoxCP.Text;
            String municipio = txtBoxMunicipio.Text;
            String celular = txtBoxTelefono.Text;
            String correo = txtBoxCorreo.Text;

            if (celular.Length == 0)
            {
                currentDelegacion.telefono = null;
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxTelefono.Text, "[^0-9]") || celular.Length > 7)
                {
                    MessageBox.Show("El campo Celular solo acepta numeros y un maximo 7 digitos.");
                    txtBoxTelefono.Text = txtBoxTelefono.Text.Remove(txtBoxTelefono.Text.Length - 1);
                }
                else
                {
                    celular = txtBoxTelefono.Text;
                    currentDelegacion.telefono = Convert.ToInt32(celular);
                }
            }

            if (nombre.Length > 0 && calle.Length > 0 && colonia.Length > 0 && municipio.Length > 0 && codigoPostal.Length > 0)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxCP.Text, "[^0-9]"))
                {
                    MessageBox.Show("El campo Codigo Postal solo acepta numeros.");
                    txtBoxCP.Text = txtBoxCP.Text.Remove(txtBoxCP.Text.Length - 1);
                }
                else
                {
                    codigoPostal = txtBoxCP.Text;
                    currentDelegacion.codigoPostal = Convert.ToInt32(codigoPostal);
                }


                currentDelegacion.nombre = nombre;
                currentDelegacion.calle = calle;
                currentDelegacion.numero = numero;
                currentDelegacion.colonia = colonia;
                currentDelegacion.municipio = municipio;
                currentDelegacion.correo = correo;
                if(delegacioneService.Update(currentDelegacion) == true)
                {
                    MessageBox.Show("Delegacion modificada correctamente.");
                    notificacion.actualizaInformacion("Delegacion modificada.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error Al modificar delegacion.");
                }

            }
            else
            {
                MessageBox.Show("Verificar campos vacios.");
            }
        }

        internal void LoadData(Delegaciones idSelected)
        {
            currentDelegacion = idSelected;
            txtBoxNombre.Text = idSelected.nombre;
            txtBoxCalle.Text = idSelected.calle;
            txtBoxNumero.Text = idSelected.numero;
            txtBoxColonia.Text = idSelected.colonia;
            txtBoxCP.Text = idSelected.codigoPostal.ToString();
            txtBoxMunicipio.Text = idSelected.municipio;
            txtBoxTelefono.Text = idSelected.telefono.ToString();
            txtBoxCorreo.Text = idSelected.correo;
        }
    }
}
