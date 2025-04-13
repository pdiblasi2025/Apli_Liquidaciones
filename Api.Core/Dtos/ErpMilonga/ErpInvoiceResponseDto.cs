using System;

namespace Api.Core.Dtos.ErpMilonga
{
    public class ErpInvoiceResponseDto
    {
        public int? Id { get; set; }
        public string LiquidacionId { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public DateTime SyncDate { get; set; }
    }
}
