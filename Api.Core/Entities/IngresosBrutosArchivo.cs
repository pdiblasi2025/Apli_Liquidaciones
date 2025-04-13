using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Entities
{
    public class IngresosBrutosArchivo : EntityBase<int>
    {
        public string Nombre { get; set; }

        public byte[] Archivo { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        public int Posicion { get; set; }
    }
}
