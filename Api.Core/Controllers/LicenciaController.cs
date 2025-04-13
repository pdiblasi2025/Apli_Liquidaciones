using Api.Core.Admin;
using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LicenciaController : BaseController<LicenciaAdmin, int, Entities.Licencia, Dtos.Licencia, FilterLicencia>
    {
        public LicenciaController(MyContext context, AppSettings appSettings) : base(context, appSettings)
        {
        }

        [HttpGet("query/basic")]
        public ActionResult<PagedListResponse<Dtos.Licencia>> GetBasic([FromQuery] FilterLicencia filter)
        {
            return _admin.GetByFilterBasic(filter);
        }

        [HttpPut("expirationDate/{id}/{date}")]
        public void UpdateExpirationDate(DateTime date, int id)
        {
            _admin.UpdateExpirationDate(date, id);
        }

        [HttpGet("compania")]
        public ActionResult<Dtos.Licencia> GetBasic([FromQuery] string idAuth)
        {
            return _admin.GetByCompany(idAuth);
        }
    }
}
