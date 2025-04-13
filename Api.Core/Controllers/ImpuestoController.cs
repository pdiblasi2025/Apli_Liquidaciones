using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImpuestoController : ControllerBase
    {
        private readonly IImpuestoService _impuestoService;

        public ImpuestoController(IImpuestoService impuestoService)
        {
            _impuestoService = impuestoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListResponse<Impuesto>>> GetAsync([FromQuery] FilterImpuesto filter)
        {
            return await _impuestoService.GetAllAsync(filter);
        }
    }
}
