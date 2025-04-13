using Microsoft.AspNetCore.Mvc;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using System.Threading.Tasks;
using Api.Core.Services.Interfaces;
using Api.Core.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Api.Core.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService _tipoDocumentoService;

        public TipoDocumentoController(ITipoDocumentoService tipoDocumentoService)
        {
            _tipoDocumentoService = tipoDocumentoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListResponse<TipoDocumento>>> GetAsync([FromQuery] FilterTipoDocumento filter)
        {
            return await _tipoDocumentoService.GetAllAsync(filter);
        }

    }
}