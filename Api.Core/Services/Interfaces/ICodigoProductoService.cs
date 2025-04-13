using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using System.Threading.Tasks;

namespace Api.Core.Services.Interfaces
{
    public interface ICodigoProductoService
    {
        Task<PagedListResponse<CodigoProducto>> GetAllAsync(FilterCodigoProducto filter);
    }
}
