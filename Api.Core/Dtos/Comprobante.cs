using Microsoft.AspNetCore.Http;

namespace Api.Core.Dtos
{
    public class Comprobante
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int Posicion { get; set; }
    }

    public class ComprobanteDownload
    {
        public string Nombre { get; set; }
        public byte[] Contenido { get; set; }
    }

    public class ComprobantePost
    {
        public IFormFile Archivo { get; set; }
        public int Posicion { get; set; }
    }
}
