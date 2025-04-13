using System.ComponentModel;

namespace Api.Core.Enums
{
    public enum EstadoCobro
    {
        [Description("Pendiente de Aprobacion")]
        PendienteAprobacion = 1,
        [Description("Aprobado")]
        Aprobado = 2,
        [Description("Facturado")]
        Facturado = 3,
        [Description("Cobrado")]
        Cobrado = 4
    }
}
