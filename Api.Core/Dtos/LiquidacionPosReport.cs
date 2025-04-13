using Api.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class LiquidacionPosReport
    {
		[ExportableToExcel("Id", 0)]
		public int Id { get; set; }

		[ExportableToExcel("Fecha", 1)]
		public string Fecha { get; set; }

		[ExportableToExcel("Razón social", 2)]
		public string Cliente { get; set; }

		[ExportableToExcel("Etiqueta", 3)]
		public string Etiqueta { get; set; }

		[ExportableToExcel("Cantidad", 4)]
		public decimal Cantidad { get; set; }

		[ExportableToExcel("Valor ítems", 5)]
		public decimal Valoritems { get; set; }

		[ExportableToExcel("Peso", 6)]
		public decimal Peso { get; set; }

		[ExportableToExcel("Volumen", 7)]
		public decimal Volumen { get; set; }

		[ExportableToExcel("Ancho", 8)]
		public decimal Ancho { get; set; }

		[ExportableToExcel("Largo", 9)]
		public decimal Largo { get; set; }

		[ExportableToExcel("Alto", 10)]
		public decimal Alto { get; set; }

		[ExportableToExcel("Importe sin impuestos", 11)]
		public decimal ValorSinImpuesto { get; set; }

		[ExportableToExcel("Estado", 12)]
		public string Estado { get; set; }
	}
}
