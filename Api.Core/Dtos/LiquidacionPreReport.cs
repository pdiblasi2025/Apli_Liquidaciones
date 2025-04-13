using Api.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class LiquidacionPreReport
    {
		[ExportableToExcel("Id", 0)]
		public int Id { get; set; }

		[ExportableToExcel("Fecha", 1)]
		public string Fecha { get; set; }

		[ExportableToExcel("Razón social", 2)]
		public string Cliente { get; set; }

		[ExportableToExcel("Código orden pago", 3)]
		public string CodigoOrdenPago { get; set; }

		[ExportableToExcel("ID MercadoPago", 4)]
		public string IdMercadoPago { get; set; }

		[ExportableToExcel("URL de pago (MP)", 5)]
		public string Urlpago { get; set; }

		[ExportableToExcel("ID Preferencia (MP)", 6)]
		public string IdPreferenciaMP { get; set; }

		[ExportableToExcel("ID orden pago", 7)]
		public string IdOrdenPago { get; set; }

		[ExportableToExcel("Otros gastos", 8)]
		public decimal OtrosGastos { get; set; }

		[ExportableToExcel("Valor sin impuesto", 9)]
		public decimal ValorSinImpuesto { get; set; }

		[ExportableToExcel("Estado", 10)]
		public string Estado { get; set; }
	}
}
