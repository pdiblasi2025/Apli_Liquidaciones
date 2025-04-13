using Api.Core.Enums;
using System;

namespace Api.Core.Dtos
{             

    public class DetalleLiquidacionPos : DtoBase<int?>
	{
		public int LiquidacionId { get; set; }
		public int ClienteId { get; set; }
		public Cliente Cliente { get; set; }
		public DateTime Fecha { get; set; }
        public DateTime FechaL { get; set; }
        public string Etiqueta { get; set; }
		public string Cantidad { get; set; }
		public decimal Valoritems { get; set; }
		public string Peso { get; set; }
		public string Volumen { get; set; }
		public string Ancho { get; set; }
		public string Largo { get; set; }
		public string Alto { get; set; }
		public EstadoItem Estado { get; set; }
		public decimal ValorSinImpuesto { get; set; }
	}
}
