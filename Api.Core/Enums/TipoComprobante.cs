using System.ComponentModel;

namespace Api.Core.Enums
{
    public enum  TipoComprobante
    {
        [Description("Factura")]
        Factura = 1,
        [Description("Caja")]
        Caja  = 2,
        [Description("Nota de Credito")]
        NotaCredito = 3,
        [Description("Nota de Debito")]
        NotaDebito = 4
    }
}
