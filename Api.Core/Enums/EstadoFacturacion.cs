using System.ComponentModel;

namespace Api.Core.Enums
{
    public enum EstadoFacturacion
    {
        [Description("Descargado")]
        Descargado = 10,
        [Description("No Colectado")]
        NoColectado = 69,
        [Description("Entregado")]
        Entregado = 25,
        [Description("No Entregado")]
        NoEntregado = 26,
        [Description("Proceso Devolución")]
        ProcesoDevolución = 51
    }
}
