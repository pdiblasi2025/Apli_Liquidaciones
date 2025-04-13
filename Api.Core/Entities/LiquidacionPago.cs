
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Entities
{
    public class LiquidacionPago
    {
        public int Id { get; set; }
        public int IdLiquidacion { get; set; }
        public Liquidacion Liquidacion { get; set; }
        public int ReciboId { get; set; }
        public string NumeroRecibo { get; set; }
        public string LinkPdf { get; set; }

    }
}
