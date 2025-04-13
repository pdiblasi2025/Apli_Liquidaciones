using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using System.Threading.Tasks;


namespace Api.Core.Services.Interfaces
{
    public interface ITipoDocumentoService
    {
        Task<PagedListResponse<TipoDocumento>> GetAllAsync(FilterTipoDocumento filter);
    }
}
