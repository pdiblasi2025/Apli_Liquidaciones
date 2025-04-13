using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Enums
{
    public enum EstadoLiquidacion
    {
        [Description("Pendiente de autorización")]
        PendienteDeAutorizacion = 1,
        [Description("Autorizada")]
        Autorizada = 2,
        [Description("Cancelada")]
        Cancelada = 3,
        [Description("Pendiente de facturación")]
        PendienteDeFacturacion = 4,
        [Description("Facturada")]
        Facturada = 5,
        [Description("Pago")]
        Pago = 6
    }
}
