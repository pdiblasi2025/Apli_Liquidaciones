using Api.Core.Enums;
using System;

namespace Api.Core.Dtos.Filter
{
    public class FilterLiquidacion : FilterBase
    {
        public int? ClienteId { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public EstadoLiquidacion? Estado { get; set; }
    }
}
