using Api.Core.Admin;
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
    //[Produces("application/json")]
    //[Route("api/[controller]")]
    //[ApiController]
    //public class UsuarioController : BaseController<UsuarioAdmin, int, Entities.Usuario, Dtos.Usuario, FilterUsuario>
    //{
    //    public UsuarioController(MyContext context) : base(context)
    //    {
    //    }

    //    [HttpGet("Compania")]
    //    public ActionResult<PagedListResponse<Dtos.Compania>> GetByCompany([FromQuery] FilterUsuario filter)
    //    {
    //        return Ok(_admin.GetByCompany(filter));
    //    }
    //}
}
