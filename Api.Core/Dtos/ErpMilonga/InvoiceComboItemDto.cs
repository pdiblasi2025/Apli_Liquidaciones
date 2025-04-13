using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.ErpMilonga
{
    public class InvoiceComboItemDto
    {
        public int ComboItemNumber { get; set; }
        public string ProductTypeID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int RequestedQuantity { get; set; }
        public string UnitOfMeasureID { get; set; }
        public decimal OriginalUnitPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VatPercent { get; set; }
        public decimal TotalTaxIncluded { get; set; }
    }
}
