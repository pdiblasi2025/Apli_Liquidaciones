using System.Collections.Generic;

namespace Api.Core.Dtos.ErpMilonga
{
    public class ErpInvoiceCreateResponse
    {
        public List<ErpInvoiceResponseDto> InvalidRequests { get; set; }
        public int Code { get; set; }
    }
}
