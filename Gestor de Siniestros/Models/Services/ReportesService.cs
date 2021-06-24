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

                throw ex;
            }
            return ObjReportesList;
        }

        public bool Add(Reportes ObjNewReport, List<Usuarios> ObjUsedUsers, List<VehiculoModel> ObjUsedVehicles, List<byte[]> ObjUsedImages, int idDelegacion)
        {
            bool IsAdded = false;
            int totalImages = ObjUsedImages.Count;
            DateTime today = DateTime.Today;

            try
            {
                var ObjDictamen = new Dictamenes();
                ObjDictamen.delegacion = idDelegacion;
                ObjDictamen.estado = "Pendiente";
                ObjDictamen.descripcion = "Pendiente de dictaminar!";
                ObjDictamen.fechaHora = today;
                DataBase.Dictamenes.Add(ObjDictamen);
                DataBase.SaveChanges();

                var ObjReporte = new Reportes();
                ObjReporte.direccion = ObjNewReport.direccion;
                ObjReporte.idDictamen = ObjDictamen.idDictamen;
                ObjReporte.fechaReporte = today;
                ObjReporte.creador = ObjNewReport.creador;
                DataBase.Reportes.Add(ObjReporte);
                DataBase.SaveChanges();

                var ObjFotografias = new ReportesFotografias();
                ObjFotografias.idReporte = ObjReporte.idReporte;
                ObjFotografias.fotoUno = ObjUsedImages[0];
                ObjFotografias.fotoDos = ObjUsedImages[1];
                ObjFotografias.fotoTres = ObjUsedImages[2];
                if (totalImages == 4)
                {
                    ObjFotografias.fotoCuatro = ObjUsedImages[3];
                }
                if (totalImages == 5)
                {
                    ObjFotografias.fotoCinco = ObjUsedImages[4];
                }
                if (totalImages ==  6)
                {
                    ObjFotografias.fotoSeis = ObjUsedImages[5];
                }
                if (totalImages == 7)
                {
                    ObjFotografias.fotoSiete = ObjUsedImages[6];
                }

                if (totalImages == 8)
                {
                    ObjFotografias.fotoOcho = ObjUsedImages[7];
                }
                DataBase.ReportesFotografias.Add(ObjFotografias);

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
                    ObjVehiculo.idVehiculo = vehiculo.id;
                    ObjVehiculo.idReporte = ObjReporte.idReporte;
                    DataBase.ReportesVehiculos.Add(ObjVehiculo);
                }

                var NoOfRowsAffected = DataBase.SaveChanges();
                if(NoOfRowsAffected > 0)
                {
                    IsAdded = true;
                }
                else
                {
                    IsAdded = false;
                    DataBase.Dictamenes.Remove(ObjDictamen);
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine("Error al registrar reporte: " + ex);
                throw;
            }

            return IsAdded;
        }
    }
}
