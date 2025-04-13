using System;

namespace Api.Core.Entities
{
    public class ClienteImpuesto : EntityBase<int>
    {
        public int ClienteId { get; set; }
        public int ImpuestoId {get; set;}
        public decimal? PorcentajeExencion { get; set; }
        public DateTime? ExencionDesde { get; set; }
        public DateTime? ExencionHasta { get; set; }
        public Cliente Cliente { get; set; }
        public Impuesto Impuesto { get; set; }
    }
}
