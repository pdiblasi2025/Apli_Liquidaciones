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
    public class CondicionPagoController : ControllerBase
    {
        private readonly ICondicionPagoService _condicionPagoService;

        public IMapper Mapper;

        public CondicionPagoController(ICondicionPagoService condicionPagoService)
        {
            _condicionPagoService = condicionPagoService;
            Mapper = new Mapper(BootStrapper.MapperConfiguration);
        }

        [HttpGet]
        public async Task<ActionResult<PagedListResponse<CondicionPago>>> GetAsync([FromQuery] FilterCondicionPago filter)
        {
            return await _condicionPagoService.GetAllAsync(filter);
        }
    }
}
