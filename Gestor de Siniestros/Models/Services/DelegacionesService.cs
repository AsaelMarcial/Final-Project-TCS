using Gestor_de_Siniestros.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class DelegacionesService
    {
        private static List<Delegaciones> ObjDelegacionesList;
        private DataBaseEntities DataBase;

        public DelegacionesService()
        {
            DataBase = new DataBaseEntities();
            ObjDelegacionesList = new List<Delegaciones>();
            
        }

        public List<Delegaciones> GetAll()
        {
            try
            {
                var ObjQuery = from delegaciones in DataBase.Delegaciones select delegaciones;
                foreach (var delegacion in ObjQuery)
                {
                    ObjDelegacionesList.Add(delegacion);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ObjDelegacionesList;
        }

        public List<String> GetDelegacionesNames()
        {
            List<String> ObjDelegacionesList = new List<String>();
            ObjDelegacionesList = DataBase.Delegaciones.Select(u => u.nombre).ToList();
            return ObjDelegacionesList;
        }

        public bool Add(Delegaciones ObjNewDelegacion)
        {
            bool IsAdded = false;
            try
            {
                var ObjDelegacion = new Delegaciones();
                ObjDelegacion.calle = ObjNewDelegacion.calle;
                ObjDelegacion.codigoPostal = ObjNewDelegacion.codigoPostal;
                ObjDelegacion.colonia = ObjNewDelegacion.colonia;
                ObjDelegacion.correo = ObjNewDelegacion.correo;
                ObjDelegacion.municipio = ObjNewDelegacion.municipio;
                ObjDelegacion.nombre = ObjNewDelegacion.nombre;
                ObjDelegacion.numero = ObjNewDelegacion.numero;
                ObjDelegacion.telefono = ObjNewDelegacion.telefono;

                DataBase.Delegaciones.Add(ObjDelegacion);
                var NORowsAffected = DataBase.SaveChanges();
                IsAdded = NORowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return IsAdded;
        }
    }
}
