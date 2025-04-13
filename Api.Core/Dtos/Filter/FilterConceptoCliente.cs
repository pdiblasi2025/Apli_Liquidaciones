using System;

namespace Api.Core.Dtos.Filter
{
    public class FilterConceptoCliente : FilterBase
    {
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
