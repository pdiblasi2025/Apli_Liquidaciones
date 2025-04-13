namespace Api.Core.Dtos.Request
{
    public class LiquidacionPagoRequest
    {
        public int IdLiquidacion { get; set; }
        public int ReciboId { get; set; }
        public string NumeroRecibo { get; set; }
        public string LinkPdf { get; set; }
    }
}
