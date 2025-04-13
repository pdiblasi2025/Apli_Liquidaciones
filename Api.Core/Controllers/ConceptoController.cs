using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Core.Admin;
using Api.Core.Dtos.Common;
using Api.Core.Repositories;
using Api.Core.Dtos.Filter;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoController : BaseController<ConceptoAdmin, int, Entities.Concepto, Dtos.Concepto, FilterConcepto>
    {
        public ConceptoController(MyContext context, AppSettings appSettings) : base(context, appSettings)
        {

        }

        [HttpGet("SinAsociar")]
        public async Task<ActionResult<IEnumerable<Dtos.Concepto>>> GetConceptosSinAsociarAsync()
        {
            return Ok(await _admin.GetConceptosSinAsociarAsync());
        }

    }
}