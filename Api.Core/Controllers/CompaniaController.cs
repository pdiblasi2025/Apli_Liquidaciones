using Api.Core.Admin;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniaController : BaseController<CompaniaAdmin, int, Entities.Compania, Dtos.Compania, FilterCompania>
    {
        public CompaniaController(MyContext context, AppSettings appSettings) : base(context, appSettings)
        {
        }

        [HttpGet("query/basic")]
        public ActionResult<PagedListResponse<Dtos.Compania>> GetBasic([FromQuery] FilterCompania filter)
        {
            return _admin.GetByFilterBasic(filter);
        }

        [HttpPost("registerCompany")]
        public Dtos.Compania RegisterCompany([FromBody] Dtos.Compania dto)
        {
            return _admin.RegisterCompany(dto);
        }
    }
}