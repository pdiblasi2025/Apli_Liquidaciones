using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Enums
{
    public enum EstadoHito
    {
        [Description("Pendiente Facturación")]
        PendienteFactración = 1,
        [Description("A Facturar")]
        AFacturar = 2,
        Facturado = 3,
        Cobrado = 4,
        Anulado = 5
    }
}
