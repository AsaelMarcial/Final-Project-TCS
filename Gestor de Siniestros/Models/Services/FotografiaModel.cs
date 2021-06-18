using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Siniestros.Models.Services
{
    class FotografiaModel
    {
        private byte[] foto1;
        private byte[] foto2;
        private byte[] foto3;
        private byte[] foto4;
        private byte[] foto5;
        private byte[] foto6;
        private byte[] foto7;
        private byte[] foto8;

        public byte[] Foto1 { get => foto1; set => foto1 = value; }
        public byte[] Foto2 { get => foto2; set => foto2 = value; }
        public byte[] Foto3 { get => foto3; set => foto3 = value; }
        public byte[] Foto4 { get => foto4; set => foto4 = value; }
        public byte[] Foto5 { get => foto5; set => foto5 = value; }
        public byte[] Foto6 { get => foto6; set => foto6 = value; }
        public byte[] Foto7 { get => foto7; set => foto7 = value; }
        public byte[] Foto8 { get => foto8; set => foto8 = value; }
    }
}
