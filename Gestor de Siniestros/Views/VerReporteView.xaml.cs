using Gestor_de_Siniestros.Models;
using Gestor_de_Siniestros.Models.DB;
using Gestor_de_Siniestros.Models.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Gestor_de_Siniestros.Views
{

    public partial class VerReporteView : Window
    {
        DataBaseEntities DataBase;
        ReportesService reportesService;
        int id;
        int idPerito;

        public VerReporteView()
        {
            InitializeComponent();
            DataBase = new DataBaseEntities();
            reportesService = new ReportesService();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        internal void LoadData(int idReporte, Usuarios currentUser)
        {
            int tipoUsuario = currentUser.tipoUsuario;
            var peritoActual = new Usuarios();
            string nombre;

            if (tipoUsuario == 2)
            {
                Dictaminar.Visibility = Visibility.Visible;
            }

            var ObjReporte = DataBase.Reportes.Where(r => r.idReporte == idReporte).FirstOrDefault();
            var ObjDicatamen = DataBase.Dictamenes.Where(d => d.idDictamen == ObjReporte.idDictamen).FirstOrDefault();
            var ObjDelegacion = DataBase.Delegaciones.Where(d => d.idDelegacion == ObjDicatamen.delegacion).FirstOrDefault();
            var listaUsuarios = new List<Usuarios>();
            var listaVehiculos = new List<Vehiculos>();

            id = ObjDicatamen.idDictamen;
            idPerito = currentUser.idUsuario;
            peritoActual = DataBase.Usuarios.Where(x => x.idUsuario == idPerito).FirstOrDefault();
            nombre = peritoActual.nombre + " " + peritoActual.aPaterno + " " + peritoActual.aMaterno;

            txtBoxCreador.Text = ObjReporte.creador;
            txtBoxDireccion.Text = ObjReporte.direccion;
            dateReporte.Text = ObjReporte.fechaReporte.ToString();
            txtBoxDelegacion.Text = ObjDelegacion.nombre;
            txtBoxEstado.Text = ObjDicatamen.estado;
            txtBoxDescripcion.Text = ObjDicatamen.descripcion;
            dateFechaDictamen.Text = ObjDicatamen.fechaHora.ToString();
            
            txtBoxDescripcion.Text = ObjDicatamen.descripcion;
            if (ObjDicatamen.estado == "Pendiente")
            {
                txtBoxFolio.Text = " ";
                txtBoxPerito.Text = " ";
            }
            else
            {
                txtBoxFolio.Text = ObjDicatamen.idDictamen.ToString();
                txtBoxPerito.Text = nombre;
            }


            var ObjInvolucrados = DataBase.ReportesUsuarios.Where(i => i.idReporte == idReporte).ToList();

            foreach (var involucrado in ObjInvolucrados)
            {
                listaUsuarios.Add(DataBase.Usuarios.Where(u => u.idUsuario == involucrado.idUsuario).FirstOrDefault());
            }

            dgConductores.ItemsSource = listaUsuarios;

            var ObjVehiculos = DataBase.ReportesVehiculos.Where(v => v.idReporte == idReporte).ToList();
            foreach (var coche in ObjVehiculos)
            {
                listaVehiculos.Add(DataBase.Vehiculos.Where(u => u.idVehiculo == coche.idVehiculo).FirstOrDefault());
            }

            dgVehiculos.ItemsSource = listaVehiculos;

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
