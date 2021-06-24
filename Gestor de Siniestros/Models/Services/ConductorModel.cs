using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class ConductorModel
    {
        public int id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Fecha { get; set; }
        public String Licencia { get; set; }
        public String Celular { get; set; }

        public ConductorModel(string nombre, string apellido, String fecha, string licencia, string celular)
        {
            Nombre = nombre;
            Apellido = apellido;
            Fecha = fecha;
            Licencia = licencia;
            Celular = celular;
        }

        public ConductorModel()
        {
        }
    }
}
