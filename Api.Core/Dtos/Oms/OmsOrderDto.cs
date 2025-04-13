namespace Api.Core.Dtos.Oms
{
    public class OmsOrderDto
    {
        public int id { get; set; }
        public string date { get; set; }
        public string element_id { get; set; }
        public string preference_id { get; set; }
        public string init_point { get; set; }
        public Order[] orders { get; set; }
        public bool payed_after_cancel { get; set; }
        public string amount_no_tax { get; set; }
    }

    public class Order
    {
        public string order_id { get; set; }
        public string tracking_id { get; set; }
    }
}
