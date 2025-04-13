using Api.Core.Enums;

namespace Api.Core.Entities
{
    public class ConceptoCliente : EntityBase<int>
    {
        public int ConceptoId { get; set; }
        public int ClienteId { get; set; }
        public decimal Monto { get; set; }
        public string Observacion { get; set; }
        public EstadoConceptoCliente Estado { get; set; }
        public Concepto Concepto { get; set; }
        public Cliente Cliente { get; set; }
    }
}
