using Api.Core.Dtos.Common;
using Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Filter
{
    public class FilterDetalleLiquidacionPre : FilterBase
    {
        public int? ClienteId { get; set; }
        public DateTime? Fecha_Desde { get; set; }
        public DateTime? Fecha_Hasta { get; set; }
        public EstadoItem? Estado { get; set; }
    }
}
