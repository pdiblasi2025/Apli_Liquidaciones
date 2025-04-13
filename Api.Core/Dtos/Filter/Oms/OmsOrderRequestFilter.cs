using System;

namespace Api.Core.Dtos.Filter.Oms
{
    public class OmsOrderRequestFilter
    {
        public int? ClientId { get; set; }

        public int? StateId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? DeliveryMode { get; set; }
    }
}
