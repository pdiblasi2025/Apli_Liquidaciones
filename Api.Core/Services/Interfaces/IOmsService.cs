using Api.Core.Dtos.Filter.Oms;
using Api.Core.Dtos.Oms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Services.Interfaces
{
    public interface IOmsService
    {
        Task<List<OmsClientDto>> GetAllClientsAsync();

        Task<List<OmsOrderDto>> GetAllOrdersAsync(OmsOrderRequestFilter requestFilter);

        Task<List<OmsShippingDto>> GetAllShippingsAsync(OmsShippingRequestFilter requestFilter);
    }
}
