using Gestor_de_Siniestros.Models.DB;
using System;
using System.Windows;

namespace Gestor_de_Siniestros.Views
{
    /// <summary>
    /// Lógica de interacción para DictaminarView.xaml
    /// </summary>
    public partial class DictaminarView : Window
    {

        DataBaseEntities DataBase;
        int id;
        int idPerito;

        public DictaminarView()
        {
            InitializeComponent();
            DataBase = new DataBaseEntities();
        }

        private void Siguiente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentDictamen = DataBase.Dictamenes.Find(id);
                currentDictamen.perito = idPerito;
                currentDictamen.descripcion = txtBoxDescripcion.Text;
                currentDictamen.estado = comboEstado.Text.ToString();
                DataBase.SaveChanges();
                MessageBox.Show("Dictamen completado.");
                this.Close();
            }catch(Exception ex)
            {
                throw ex;
            }
        }


        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadData(int idDictamen , int idPerito)
        {
            id = idDictamen;
            this.idPerito = idPerito;
        }
    }
}
