using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Request
{
    public class LiquidacionDatosFacturacionRequest
    {
        public string OtrosComprobantes { get; set; }
        public string NumeroFactura { get; set; }
        public decimal MontoTotalImpuestos { get; set; }
        public decimal MontoFinalFactura { get; set; }
    }
}
