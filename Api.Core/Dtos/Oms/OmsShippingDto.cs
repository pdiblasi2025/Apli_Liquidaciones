using System;

namespace Api.Core.Dtos.Oms
{
    public class OmsShippingDto
    {
        public int id { get; set; }
        public string shipment_id { get; set; }
        public string date { get; set; }
        public string updated_at { get; set; }
        public string items_quantity { get; set; }
        //psd
        public string registered { get; set; }
        public string processed { get; set; }
        public string planned { get; set; }
        public string pickedup { get; set; }
        public string notified { get; set; }
        
        public string ready { get; set; }
        public string dispatched { get; set; }
        public string discharged { get; set; }
        public string delivered { get; set; }
        public string not_delivered { get; set; }
        public string cancelled { get; set; }
        public DateTime FechaL { get; set; }
        //
        public string value { get; set; }
        public string shipping_cost { get; set; }
        public string shipping_cost_no_tax { get; set; }
        public string height { get; set; }
        public string width { get; set; }
        public string weight { get; set; }
        public string length { get; set; }
        public string volume { get; set; }
        
    }
}
