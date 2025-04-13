using Api.Core.Enums;
using System.Collections.Generic;

namespace Api.Core.Entities
{
    public class Liquidacion_detalles : EntityBase<int>
    {
        //aca debe traer los campos de la vista.

        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public string Descripcion { get; set; }
        public decimal Saldo { get; set; }
        public EstadoLiquidacion Estado { get; set; }
        public string Factura { get; set; }
        public string OtrosComprobantes { get; set; }
        public string NumeroFactura { get; set; }
        public decimal MontoTotalImpuestos { get; set; }
        public decimal MontoFinalFactura { get; set; }
        public int ErpId { get; set; }
        public IList<ConceptoLiquidacion> Conceptos { get; set; } = new List<ConceptoLiquidacion>();
        public IList<DetalleLiquidacionPre> DetalleLiquidacionPre { get; set; } = new List<DetalleLiquidacionPre>();
        public IList<DetalleLiquidacionPos> DetalleLiquidacionPos { get; set; } = new List<DetalleLiquidacionPos>();
        public IList<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();
        public IList<LiquidacionPago> LiquidacionPagos { get; set; } = new List<LiquidacionPago>();

    }
}