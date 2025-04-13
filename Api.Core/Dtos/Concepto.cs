
using System;

namespace Api.Core.Dtos
{
    public class Concepto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool IsGeneric { get; set; }
        public bool Descuento { get; set; }
        public int? CodigoProductoId { get; set; }
        public CodigoProducto CodigoProducto { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
