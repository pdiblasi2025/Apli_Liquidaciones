using Api.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class LiquidacionReport
    {
        [ExportableToExcel("Id", 0)]
        public int Id { get; set; }

        [ExportableToExcel("Fecha", 1)]
        public string Fecha { get; set; }

        [ExportableToExcel("Razón Social", 2)]
        public string Cliente { get; set; }

        [ExportableToExcel("Descripción", 3)]
        public string Descripcion { get; set; }

        [ExportableToExcel("Saldo", 4)]
        public decimal Saldo { get; set; }

        [ExportableToExcel("Estado", 5)]
        public string Estado { get; set; }

        [ExportableToExcel("Factura", 6)]
        public string Factura { get; set; }

        [ExportableToExcel("Otros comprobantes", 7)]
        public string OtrosComprobantes { get; set; }

        [ExportableToExcel("Número factura", 8)]
        public string NumeroFactura { get; set; }

        [ExportableToExcel("Monto total impuestos", 9)]
        public decimal MontoTotalImpuestos { get; set; }

        [ExportableToExcel("Monto final factura", 10)]
        public decimal MontoFinalFactura { get; set; }

        [ExportableToExcel("Concepto observación", 11)]
        public string ConceptoObservacion { get; set; }

        [ExportableToExcel("Concepto monto", 12)]
        public decimal ConceptoMonto { get; set; }
    }
}
