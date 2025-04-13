using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Request
{
    public class ConceptoLiquidacionRequest
    {
        public Dtos.Concepto Concepto { get; set; }
        public string Observacion { get; set; }

        public decimal Monto { get; set; }
    }
}
