using Api.Core.Enums;
using System.Collections.Generic;

namespace Api.Core.Dtos
{
    public class LiquidacionDetalle : DtoBase<int?>
    {
        public int id { get; set; }
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string Descripcion { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
        public string Factura { get; set; }
        public string OtrosComprobantes { get; set; }
        public string NumeroFactura { get; set; }
        public decimal MontoTotalImpuestos { get; set; }
        public decimal MontoFinalFactura { get; set; }
        public string ConceptoObservacion { get; set; }
        public decimal ConceptoMonto { get; set; }

    }
}
