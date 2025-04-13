using System.Collections.Generic;

namespace Api.Core.Entities
{
    public class Concepto : EntityBase<int>
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool IsGeneric { get; set; }
        public bool Descuento { get; set; }
        public int? CodigoProductoId { get; set; }
        public ErpMilongaProductCode CodigoProducto { get; set; }
        public IList<ConceptoCliente> Clientes { get; set; } = new List<ConceptoCliente>();
        public IList<ConceptoLiquidacion> Liquidaciones { get; set; } = new List<ConceptoLiquidacion>();
    }
}