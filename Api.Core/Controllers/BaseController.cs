using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Api.Core.Admin;
using Api.Core.Dtos;
using Api.Core.Entities;
using Api.Core.Repositories;
using Api.Core.Dtos.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Api.Core.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class BaseController<TA, TID, TE, TD, TF> : ControllerBase
         where TA : BaseAdmin<TID, TE, TD, TF>, new()
         where TF : FilterBase
         where TE : EntityBase<TID>
    {
        #region Properties

        public UserManager<ApplicationUser> _userManager;
        public readonly AppSettings _appSettings;
        protected TA _admin;

        public string UserName
        {
            get
            {
                var claims = User.Claims.Where(c => c.Type == "email");
                return claims?.FirstOrDefault().Value;
            }
        }

        #endregion

        public BaseController(MyContext context, AppSettings appSettings)
        {
            _admin = new TA();
            _admin.MyContext = context;
            _appSettings = appSettings;
        }

        public BaseController(MyContext context, UserManager<ApplicationUser> userManager)
        {
            _admin = new TA();
            _admin.MyContext = context;
            this._userManager = userManager;
        }

        [HttpPost("ValidateCreate")]
        public Dtos.Response.ResultMessage<TD> ValidateCreate([FromBody] TD dto)
        {
            return _admin.Validate(dto, 1);
        }

        [HttpPost("ValidateUpdate")]
        public Dtos.Response.ResultMessage<TD> ValidateUpdate([FromBody] TD dto)
        {
            return _admin.Validate(dto, 2);
        }

        [HttpPost("ValidateDelete")]
        public Dtos.Response.ResultMessage<TD> ValidateDelete([FromBody] TD dto)
        {
            return _admin.Validate(dto, 3);
        }

        [HttpGet]
        public ActionResult<PagedListResponse<TD>> Get([FromQuery] TF filter)
        {
            return _admin.GetByFilter(filter);
        }

        [HttpGet("{id}")]
        public ActionResult<TD> Get(TID id)
        {
            return _admin.GetById(id);
        }

        [HttpPost]
        public TD Post([FromBody] TD dto)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);
            return _admin.Create(dto);
        }

        [HttpPut("{id}")]
        public virtual TD Put(int id, [FromBody] TD request)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);
            return _admin.Update(request);
        }

        [HttpDelete("{id}")]
        public void Delete(TID id)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);
            _admin.Delete(id);
        }

        [HttpGet("init/dataList")]
        public ActionResult<object> DataList()
        {
            return _admin.GetDataList();
        }

        [HttpGet("init/dataEdit")]
        public ActionResult<object> DataEdit()
        {
            return _admin.GetDataEdit();
        }

        private static string GetOfString() {
            return typeof(TD).Name;
        }
    }

    public class ApplicationUser : IdentityUser
    {
    }
}