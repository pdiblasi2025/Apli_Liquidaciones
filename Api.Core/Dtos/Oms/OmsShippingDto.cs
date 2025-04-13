namespace Api.Core.Dtos.Oms
{
    public class OmsShippingDto
    {
        public int id { get; set; }
        public string shipment_id { get; set; }
        public string date { get; set; }
        public string value { get; set; }
        public string shipping_cost { get; set; }
        public string height { get; set; }
        public string width { get; set; }
        public string weight { get; set; }
        public string length { get; set; }
        public string volume { get; set; }
        public string items_quantity { get; set; }
    }
}
