using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CodigoProductoController : ControllerBase
    {
        private readonly ICodigoProductoService _codigoProductoService;

        public CodigoProductoController(ICodigoProductoService codigoProductoService)
        {
            _codigoProductoService = codigoProductoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListResponse<CodigoProducto>>> GetAsync([FromQuery] FilterCodigoProducto filter)
        {
            return await _codigoProductoService.GetAllAsync(filter);
        }
    }
}
