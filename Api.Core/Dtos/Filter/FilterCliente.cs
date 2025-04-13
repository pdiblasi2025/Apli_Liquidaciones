using Api.Core.Enums;

namespace Api.Core.Dtos.Filter
{
    public class FilterCliente : FilterBase
    {
        public EstadoFacturacion? EstadoFacturacion { get; set; }
        public TipoCliente? TipoCliente { get; set; }
    }
}
