using Api.Core.Enums;
using System;

namespace Api.Core.Entities
{
    public class DetalleLiquidacionPre : EntityBase<int>
    {
        public int OmsId { get; set; }
        public int? LiquidacionId { get; set; }
        public Liquidacion Liquidacion { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoOrdenPago { get; set; }
        public string IdMercadoPago { get; set; }
        public string Urlpago { get; set; }
        public string IdPreferenciaMP { get; set; }
        public string IdOrdenPago { get; set; }
        public decimal OtrosGastos { get; set; }
        public EstadoItem Estado { get; set; }
        public decimal ValorSinImpuesto { get; set; }
    }
}