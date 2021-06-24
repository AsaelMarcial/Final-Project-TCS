using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class ReporteModel
    {
        public int id { get; set; }
        public String Fecha { get; set; }
        public String Direccion { get; set; }
        public String Creador { get; set; }
        public String Estado { get; set; }

        public ReporteModel(String fecha, string direccion, string creador, string estado)
        {
            Fecha = fecha;
            Direccion = direccion;
            Creador = creador;
            Estado = estado;
        }

        public ReporteModel()
        {
        }
    }
}
