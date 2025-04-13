using Api.Core.Enums;
using Newtonsoft.Json;
using System;


namespace Api.Core.Dtos
{
    [JsonObject(IsReference = true)]
    public class ConceptoCliente : DtoBase<int?>
    {
        public int ConceptoId { get; set; }
        public Concepto Concepto { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente{ get; set; }
        public string Observacion { get; set; }
        public decimal Monto { get; set; }
        public EstadoConceptoCliente Estado { get; set; }
    }
}
