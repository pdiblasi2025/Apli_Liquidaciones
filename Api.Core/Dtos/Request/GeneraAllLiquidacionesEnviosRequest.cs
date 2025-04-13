using System;

namespace Api.Core.Dtos.Request
{
    public class GeneraAllLiquidacionesEnviosRequest
    {
        public string Descripcion { get; set; }

        public int[] UncheckedIds { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        public int? ClienteId { get; set; }

        public string Search { get; set; }
    }
}
