
using Api.Core.Enums;

namespace Api.Core.Entities
{
    public class ConceptoLiquidacion : EntityBase<int>
    {
        public int ConceptoId { get; set; }
        public int? ConceptoClienteId { get; set; }
        public ConceptoCliente ConceptoCliente { get; set; }
        public int LiquidacionId { get; set; }
        public string Detalle { get; set; }
        public decimal Monto { get; set; }
        public string Observacion { get; set; }
        public EstadoConceptoCliente Estado { get; set; }
        public Concepto Concepto { get; set; }
        public Liquidacion Liquidacion { get; set; }
    }
}
