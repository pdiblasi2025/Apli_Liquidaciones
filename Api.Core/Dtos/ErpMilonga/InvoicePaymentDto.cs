namespace Api.Core.Dtos.ErpMilonga
{
    public class InvoicePaymentDto
    {
        public string PaymentMethodID { get; set; }
        public string PaymentTransactionID { get; set; }
        public decimal Amount { get; set; }
        public int PaymentTerm { get; set; }
    }
}
