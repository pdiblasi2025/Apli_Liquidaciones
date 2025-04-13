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
    public class TipoImpuestoController : ControllerBase
    {
            private readonly ITipoImpuestoService _tipoImpuestoService;

        public TipoImpuestoController(ITipoImpuestoService tipoImpuestoService)
        {
            _tipoImpuestoService = tipoImpuestoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListResponse<TipoImpuesto>>> GetAsync([FromQuery] FilterTipoImpuesto filter)
        {
            return await _tipoImpuestoService.GetAllAsync(filter);
        }

    }
}