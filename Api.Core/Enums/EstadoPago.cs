using System.ComponentModel;

namespace Api.Core.Enums
{
    public enum EstadoPago
    {
        [Description("Pendiente de Aprobacion")]
        PendienteAprobacion = 1,
        [Description("Aprobado")]
        Aprobado = 2,
        [Description("Pagado")]
        Pagado = 3
    }
}
