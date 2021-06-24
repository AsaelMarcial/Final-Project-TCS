using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models
{
    class InvolucradoModel
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public String Nombre { get; set; }
        public String Licencia { get; set; }
        public int idVehiculo { get; set; }
        public String Placas { get; set; }
        public String Marca { get; set;  }
        public String Modelo { get; set; }

        public InvolucradoModel(int id, int idUsuario, string nombre, string licencia, int idVehiculo, string placas, string marca, string modelo)
        {
            this.id = id;
            this.idUsuario = idUsuario;
            Nombre = nombre;
            Licencia = licencia;
            this.idVehiculo = idVehiculo;
            Placas = placas;
            Marca = marca;
            Modelo = modelo;
        }

        public InvolucradoModel()
        {
        }
    }

}
