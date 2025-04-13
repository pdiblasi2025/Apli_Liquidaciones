using Api.Core.Enums;

namespace Api.Core.Dtos
{
    public class ConceptoLiquidacion : DtoBase<int?>
    {
        public int ConceptoId { get; set; }
        public EstadoConceptoCliente Estado { get; set; }
        public string Detalle { get; set; }
        public decimal Monto { get; set; }
        public string Observacion { get; set; }
        public Concepto Concepto { get; set; }
    }
}
