namespace Api.Core.Dtos.ErpMilonga
{
    public class InvoiceBillingAddressDto
    {
        public string BusinessName { get; set; }
        public string TaxTypeID { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationCode { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public string Door { get; set; }
        public string Appartment { get; set; }
        public string ZipCode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
