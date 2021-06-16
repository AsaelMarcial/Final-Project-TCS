using Gestor_de_Siniestros.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class ReportesService
    {
        private DataBaseEntities DataBase;
        DelegacionesService delegacionesService;

        public ReportesService()
        {
            DataBase = new DataBaseEntities();
            delegacionesService = new DelegacionesService();
        }

        public List<Reportes> GetAll()
        {
            List<Reportes> ObjReportesList = new List<Reportes>();
            try
            {
                var ObjQuery = from reportes in DataBase.Reportes select reportes;
                foreach (var reporte in ObjQuery)
                {
                    ObjReportesList.Add(reporte);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ObjReportesList;
        }

        public bool Add(Reportes ObjNewReport, List<Usuarios> ObjUsedUsers, List<Vehiculos> ObjUsedVehicles, List<byte[]> ObjUsedImages, int idDelegacion)
        {
            int leftImages = ObjUsedImages.Count;
            DateTime today = DateTime.Today;

            try
            {
                var ObjDictamen = new Dictamenes();
                ObjDictamen.delegacion = idDelegacion;
                ObjDictamen.estado = "Pendiente";
                ObjDictamen.descripcion = "Pendiente de dictaminar!";
                ObjDictamen.fechaHora = today;
                DataBase.Dictamenes.Add(ObjDictamen);

                var ObjReporte = new Reportes();
                ObjReporte.direccion = ObjNewReport.direccion;
                ObjReporte.idDictamen = ObjDictamen.idDictamen;
                ObjReporte.fechaReporte = today;
                ObjReporte.creador = ObjNewReport.creador;
                DataBase.Reportes.Add(ObjReporte);

                /*var ObjFotografias = new ReportesFotografias();
                ObjFotografias.idReporte = ObjReporte.idReporte;
                ObjFotografias.fotoUno = ObjUsedImages.Last();
                ObjUsedImages.RemoveAt(leftImages);
                leftImages -= leftImages;
                ObjFotografias.fotoDos = ObjUsedImages.Last();
                ObjUsedImages.RemoveAt(leftImages);
                leftImages -= leftImages;
                ObjFotografias.fotoTres = ObjUsedImages.Last();
                ObjUsedImages.RemoveAt(leftImages);
                leftImages -= leftImages;
                if (leftImages > 0)
                {
                    ObjFotografias.fotoCuatro = ObjUsedImages.Last();
                    ObjUsedImages.RemoveAt(leftImages);
                    leftImages -= leftImages;
                }
                if (leftImages > 0)
                {
                    ObjFotografias.fotoCinco = ObjUsedImages.Last();
                    ObjUsedImages.RemoveAt(leftImages);
                    leftImages -= leftImages;
                }
                if (leftImages > 0)
                {
                    ObjFotografias.fotoSeis = ObjUsedImages.Last();
                    ObjUsedImages.RemoveAt(leftImages);
                    leftImages -= leftImages;
                }
                if (leftImages > 0)
                {
                    ObjFotografias.fotoSiete = ObjUsedImages.Last();
                    ObjUsedImages.RemoveAt(leftImages);
                    leftImages -= leftImages;
                }

                if (leftImages > 0)
                {
                    ObjFotografias.fotoOcho = ObjUsedImages.Last();
                    ObjUsedImages.RemoveAt(leftImages);
                    leftImages -= leftImages;
                }
                DataBase.ReportesFotografias.Add(ObjFotografias);*/

                foreach (var usuario in ObjUsedUsers)
                {
                    var ObjUsuario = new ReportesUsuarios();
                    ObjUsuario.idUsuario = usuario.idUsuario;
                    ObjUsuario.idReporte = ObjReporte.idReporte;
                    DataBase.ReportesUsuarios.Add(ObjUsuario);
                }
                foreach (var vehiculo in ObjUsedVehicles)
                {
                    var ObjVehiculo = new ReportesVehiculos();
                    ObjVehiculo.idVehiculo = vehiculo.idVehiculo;
                    ObjVehiculo.idReporte = ObjReporte.idReporte;
                    DataBase.ReportesVehiculos.Add(ObjVehiculo);
                }

                DataBase.SaveChanges();
            }
            catch (Exception ex )
            {
                Console.WriteLine("Error al registrar reporte: " + ex);
                throw;
            }

            return true;
        }
    }
}
