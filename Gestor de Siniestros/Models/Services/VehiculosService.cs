using Gestor_de_Siniestros.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestor_de_Siniestros.Models.Services
{
    class VehiculosService
    {
        private DataBaseEntities DataBase;

        public VehiculosService()
        {
            DataBase = new DataBaseEntities();
        }

        public List<Vehiculos> GetAll()
        {
            List<Vehiculos> ObjVehiculosList = new List<Vehiculos>();
            try
            {
                var ObjQuery = from vehiculos in DataBase.Vehiculos select vehiculos;
                foreach (var vehiculo in ObjQuery)
                {
                    ObjVehiculosList.Add(vehiculo);
                }
            }catch (Exception ex)
            {
                throw ex;
            }
            return ObjVehiculosList;
        }

        public bool Add(Vehiculos ObjNewVehiculo)
        {
            bool IsAdded = false;
            try
            {
                var ObjVehiculo = new Vehiculos();
                ObjVehiculo.tipoPropiedad = ObjNewVehiculo.tipoPropiedad;
                ObjVehiculo.marca = ObjNewVehiculo.marca;
                ObjVehiculo.modelo = ObjNewVehiculo.modelo;
                ObjVehiculo.año = ObjNewVehiculo.año;
                ObjVehiculo.color = ObjNewVehiculo.color;
                ObjVehiculo.nombreAseguradora = ObjNewVehiculo.nombreAseguradora;
                ObjVehiculo.idPoliza = ObjNewVehiculo.idPoliza;
                ObjVehiculo.placa = ObjNewVehiculo.placa;
                ObjVehiculo.idDueño = ObjNewVehiculo.idDueño;

                DataBase.Vehiculos.Add(ObjVehiculo);
                var NoOfRowsAffected = DataBase.SaveChanges();
                IsAdded = NoOfRowsAffected > 0;
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
