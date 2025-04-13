using Api.Core.Enums;
using System;

namespace Api.Core.Dtos.Request
{
    public class SendAllToErpRequest
    {
        public int[] UncheckedIds { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        public int? ClienteId { get; set; }

        public string Search { get; set; }
    }
}
