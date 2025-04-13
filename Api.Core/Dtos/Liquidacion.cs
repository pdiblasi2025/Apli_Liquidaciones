using Api.Core.Enums;
using System.Collections.Generic;

namespace Api.Core.Dtos
{
    public class Liquidacion : DtoBase<int?>
    {
        public int? IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public string Descripcion { get; set; }
        public decimal Saldo { get; set; }
        public EstadoLiquidacion Estado { get; set; }
 	    public string Factura { get; set; }
 	    public string OtrosComprobantes { get; set; }
        public string NumeroFactura { get; set; }
        public decimal MontoTotalImpuestos { get; set; }
        public decimal MontoFinalFactura { get; set; }
        public IList<ConceptoLiquidacion> Conceptos { get; set; }
        public IList<Comprobante> Comprobantes { get; set; }
        public IList<DetalleLiquidacionPre> Ordenes { get; set; }
        public IList<DetalleLiquidacionPos> Envios { get; set; }
    }
}
