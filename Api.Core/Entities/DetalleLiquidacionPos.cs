using Api.Core.Enums;
using System;

namespace Api.Core.Entities
{
    public class DetalleLiquidacionPos : EntityBase<int>
    {
		public int OmsId { get; set; }
		public int? LiquidacionId { get; set; }
		public Liquidacion Liquidacion { get; set; }
		public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
		public DateTime Fecha { get; set; }
		public string Etiqueta { get; set; }
		public string Cantidad { get; set; }
		public decimal Valoritems { get; set; }
		public string Peso { get; set; }
		public string Volumen { get; set; }
		public string Ancho { get; set; }
		public string Largo { get; set; }
		public string Alto { get; set; }
        public decimal ValorSinImpuesto { get; set; }
		public EstadoItem Estado { get; set; }
	}
}