using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class VehiculoModel
    {
        public int id { get; set; }
        public String Placa { get; set; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public int Año { get; set; }
        public String Color { get; set; }
        public String Aseguradora { get; set; }
        public int idDueño { get; set; }

        public VehiculoModel(string placa, string marca, string modelo, int año, string color, string aseguradora)
        {
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Año = año;
            Color = color;
            Aseguradora = aseguradora;
        }

        public VehiculoModel()
        {
        }

        public VehiculoModel(int id, string placa, string marca, string modelo, int año, string color, string aseguradora, int idDueño)
        {
            this.id = id;
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Año = año;
            Color = color;
            Aseguradora = aseguradora;
            this.idDueño = idDueño;
        }
    }
}
