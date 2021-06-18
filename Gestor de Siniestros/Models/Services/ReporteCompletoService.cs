using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class ReporteCompletoService
    {
        private int idReporte;
        private DateTime fechaReporte;
        private String direccion;

        public int IdReporte { get => idReporte; set => idReporte = value; }
        public DateTime FechaReporte { get => fechaReporte; set => fechaReporte = value; }
        public string Direccion { get => direccion; set => direccion = value; }

        public ReporteCompletoService(int id, DateTime fecha, String direccion)
        {
            this.idReporte = id;
            this.fechaReporte = fecha;
            this.direccion = direccion;
        }

        public ReporteCompletoService()
        {

        }
    }
}
