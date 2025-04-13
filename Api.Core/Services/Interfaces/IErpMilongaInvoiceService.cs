using E = Api.Core.Entities;
using System.Threading.Tasks;
using Api.Core.Dtos.ErpMilonga;

namespace Api.Core.Services.Interfaces
{
    public interface IErpMilongaInvoiceService
    {
        InvoiceOrderDto GenerateInvoice(E.Liquidacion liquidacion);

        Task<ErpInvoiceResponseDto> Sync(InvoiceOrderDto liquidacion);
    }
}
