using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class IngresosBrutosArchivo
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int Posicion { get; set; }
    }

    public class IngresosBrutosArchivoDownload
    {
        public string Nombre { get; set; }
        public byte[] Contenido { get; set; }
    }

    public class IngresosBrutosArchivoPost
    {
        public IFormFile Archivo { get; set; }
        public int Posicion { get; set; }
    }
}
