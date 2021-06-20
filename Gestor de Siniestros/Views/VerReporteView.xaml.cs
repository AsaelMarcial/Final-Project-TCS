using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para VerReporteView.xaml
    /// </summary>
    public partial class VerReporteView : Window
    {
        DataBaseEntities DataBase;
        ReportesService reportesService;
        int id;
        int idPerito;

        public VerReporteView()
        {
            DataBase = new DataBaseEntities();
            reportesService = new ReportesService();

            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        internal void LoadData(int idReporte, Usuarios currentUser)
        {
            int tipoUsuario = currentUser.tipoUsuario;

            if(tipoUsuario == 2)
                Dictaminar.Visibility = Visibility.Visible;

            var ObjReporte = DataBase.Reportes.Where(r => r.idReporte == idReporte).FirstOrDefault();
            var ObjDicatamen = DataBase.Dictamenes.Where(d => d.idDictamen == ObjReporte.idDictamen).FirstOrDefault();
            var ObjDelegacion = DataBase.Delegaciones.Where(d => d.idDelegacion == ObjDicatamen.delegacion).FirstOrDefault();
            var listaUsuarios = new List<Usuarios>();

            id = ObjDicatamen.idDictamen;
            idPerito = currentUser.idUsuario;

            txtBoxCreador.Text = ObjReporte.creador;
            txtBoxDireccion.Text = ObjReporte.direccion;
            dateFechaReporte.Text = ObjReporte.fechaReporte.ToString();
            txtBoxDelegacion.Text = ObjDelegacion.nombre;
            txtBoxEstado.Text = ObjDicatamen.estado;
            txtBoxDescripcion.Text = ObjDicatamen.descripcion;
            dateFechaDictamen.Text = ObjDicatamen.fechaHora.ToString();
            txtBoxPerito.Text = ObjDicatamen.perito.ToString();
            txtBoxDescripcion.Text = ObjDicatamen.descripcion;
            txtBoxFolio.Text = ObjDicatamen.idDictamen.ToString();

            var ObjInvolucrados = DataBase.ReportesUsuarios.Where(i => i.idReporte == idReporte).ToList();

            foreach (var involucrado in ObjInvolucrados)
            {
                listaUsuarios.Add(DataBase.Usuarios.Where(u => u.idUsuario == involucrado.idUsuario).FirstOrDefault());
            }

            dgInvolucrados.ItemsSource = listaUsuarios;

            List<FotografiaModel> listaFotos = new List<FotografiaModel>();
            using (DataBaseEntities db = new DataBaseEntities())
            {
                listaFotos = (from d in db.ReportesFotografias.Where(f => f.idReporte == idReporte)
                              select new FotografiaModel
                              {
                                  Foto1 = d.fotoUno,
                                  Foto2 = d.fotoDos,
                                  Foto3 = d.fotoTres,
                                  Foto4 = d.fotoCuatro,
                                  Foto5 = d.fotoCinco,
                                  Foto6 = d.fotoSeis,
                                  Foto7 = d.fotoSiete,
                                  Foto8 = d.fotoOcho
                              }).ToList();
            }
            dgFotografias.ItemsSource = listaFotos;
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dictaminar_Click(object sender, RoutedEventArgs e)
        {
            DictaminarView dictaminarView = new DictaminarView();
            dictaminarView.LoadData(id, idPerito);
            dictaminarView.ShowDialog();
            this.Close();
        }
    }
}
