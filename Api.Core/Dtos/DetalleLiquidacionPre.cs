using Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class DetalleLiquidacionPre : DtoBase<int?>
    {
        public int LiquidacionId { get; set; }
		public int ClienteId { get; set; }
		public Cliente Cliente { get; set; }
		public DateTime Fecha { get; set; }
		public string CodigoOrdenPago { get; set; }
	    public string IdMercadoPago { get; set; }
	    public string Urlpago { get; set; }
	    public string IdPreferenciaMP { get; set; }
	    public string IdOrdenPago { get; set; }
	    public decimal OtrosGastos { get; set; }
        public EstadoItem Estado { get; set; }
        public decimal ValorSinImpuesto { get; set; }
    }

	//public class DetalleLiquidacionPreOms
	//{
	//	public int IdLiquidacion { get; set; }
 //       public string IdClienteOMS { get; set; }
 //       public string CodigoOrdenPago { get; set; }
	//	public int IdMercadoPago { get; set; }
	//	public string Urlpago { get; set; }
	//	public int IdPreferenciaMP { get; set; }
	//	public string CallbackasociadoMP { get; set; }
	//	public int IdOrdenPago { get; set; }
	//	public decimal OtrosGastos { get; set; }
	//	public EstadoItem Estado { get; set; }
	//	public decimal ValorSinImpuesto { get; set; }
	//}
}
