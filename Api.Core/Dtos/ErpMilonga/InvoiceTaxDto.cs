using System;

namespace Api.Core.Dtos.ErpMilonga
{
    public class InvoiceTaxDto
    {
        public string TaxCode { get; set; }

        public decimal? ExemptionPercent { get; set; }

        public DateTime? ExemptFrom { get; set; }

        public DateTime? ExemptTo { get; set; }
    }
}
