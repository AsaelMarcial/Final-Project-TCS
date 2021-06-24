using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class ImagenService
    {
        public String Nombre { get; set; }
        public String Ubicacion { get; set; }
        public byte[] Foto { get; set; }

        public ImagenService()
        {

        }

        public ImagenService(string nombre, string ubicacion, byte[] foto)
        {
            Nombre = nombre;
            Ubicacion = ubicacion;
            Foto = foto;
        }
    }
}
