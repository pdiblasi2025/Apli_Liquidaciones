using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Enums
{
    public enum EstadoItem
    {
        [Description("Liquidado")]
        Liquidado = 1,
        [Description("Pendiente a liquidar")]
        PendienteLiquidar = 2
    }
}
