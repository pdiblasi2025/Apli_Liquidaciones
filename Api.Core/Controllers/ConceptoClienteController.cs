using Microsoft.AspNetCore.Mvc;
using Api.Core.Admin;
using Api.Core.Repositories;
using Api.Core.Dtos.Filter;

namespace Api.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoClienteController : BaseController<ConceptoClienteAdmin, int, Entities.ConceptoCliente, Dtos.ConceptoCliente, FilterConceptoCliente>
    {
        public ConceptoClienteController(MyContext context, AppSettings appSettings) : base(context, appSettings)
        {

        }
    }
}

