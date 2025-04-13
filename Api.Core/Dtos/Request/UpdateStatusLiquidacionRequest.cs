using System.Collections.Generic;

namespace Api.Core.Dtos.Request
{
    public class UpdateStatusLiquidacionRequest
    {
        public int[] Ids { get; set; }
        public int EstadoId { get; set; }
    }
}
