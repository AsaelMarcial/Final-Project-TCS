using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class ImagenService
    {
        private String Imagen;

        public string Ubicacion { get => Imagen; set => Imagen = value; }

        public ImagenService(String imagen)
        {
            this.Imagen = imagen;
        }
    }
}
