using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.Core.Dtos.ErpMilonga
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class InvoiceOrderDto
    {
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Remarks { get; set; }
        public string OrderTypeID { get; set; }
        public string StoreID { get; set; }
        //PSD
        public string PaymentMethodID { get; set; }
        //
        public List<InvoicePaymentDto> Payment { get; set; }
        public InvoiceCustomerDto Customer { get; set; }
        public InvoiceBillingAddressDto BillingAddress { get; set; }
        public List<InvoiceTaxDto> Tax { get; set; }
        public List<InvoiceOrderItemDto> OrderItem { get; set; }
    }
}
