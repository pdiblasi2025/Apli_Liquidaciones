using Api.Core.Admin;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Dtos;
using Api.Core.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Api.Core.Dtos.Request;
using Newtonsoft.Json.Linq;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize(Roles = "SuperAdmin")]
    [ApiController]
    public class AuthController : Controller
    {
        public readonly IOptions<AppSettings> _appSettings;
        public readonly AuthAdmin _authAdmin;
        public readonly MyContext _context;

        public AuthController(IOptions<AppSettings> appSettings, MyContext context)
        {
            _appSettings = appSettings;
            _authAdmin = new AuthAdmin(_appSettings.Value, context);
            _context = context;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
                var response = _authAdmin.GetAllUsers();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Json(response.Content);
            }
            return Json($"Status code: {response.StatusCode}. {response.ErrorMessage}");
        }

        //[HttpGet]
        //[Route("GetToken")]
        //public IActionResult GetToken()
        //{
        //    var response = GetManagementAPIToken();

        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        return Json(response.Content);
        //    }
        //    return Json($"Status code: {response.StatusCode}. {response.ErrorMessage}");
        //}



        [HttpGet]
        [Route("GetUsers")]
        public PagedListResponse<Dtos.UsuarioAuth> GetUsers([FromQuery] FilterUsuarioAuth filter)
        {
            try
            {
                var response = filter.company_id == 0 ? _authAdmin.GetAllUsers() : _authAdmin.GetUsersByCompany(filter.company_id);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var usuarios = JsonConvert.DeserializeObject<List<Dtos.UsuarioAuth>>(response.Content);
                    usuarios = usuarios.OrderBy(x => x.name).ToList();

                    if (filter.company_id == 0)
                    {
                        var companias = _context.Companias.Where(x => !x.Deleted && x.Enabled).ToList();
                        foreach (var user in usuarios)
                        {
                            if (user.user_metadata == null)
                            {
                                user.CompanyName = "Sin compañía";
                            }
                            else {
                                var compania = companias.FirstOrDefault(x => x.Id == user.user_metadata.company_id);
                                user.CompanyName = compania == null ? "Sin compañía" : compania.Nombre;
                            }
                        }
                    }
                    else
                    {
                        var compania = _context.Companias.FirstOrDefault(x => x.Id == filter.company_id);
                        foreach (var user in usuarios)
                        {
                            user.CompanyName = compania == null ? "Sin compañía" : compania.Nombre;
                        }
                    }   

                    var pageSize = filter.PageSize ?? 10;
                    var currentPage = filter.CurrentPage ?? 1;   


                    var data = usuarios.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();

                    return new PagedListResponse<Dtos.UsuarioAuth>
                    {
                        Count = usuarios.Count(),
                        Data = data
                    };
                }
                else
                {
                    throw new System.Exception(response.Content);
                }
            }
            catch (System.Exception ex)
            {
                return new PagedListResponse<Dtos.UsuarioAuth>
                {
                    Result = new Result() { HasErrors = true, Messages = new List<string>() { ex.Message } }
                };
            }
        }

        [HttpGet]
        [Route("GetUser/{userId}")]
        public JsonResult GetUser(string userId)
        {
            var response = _authAdmin.GetUser(userId);
            return Json(response.Content);
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public async Task<JsonResult> GetUserProfile()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = _authAdmin.GetUserProfile(accessToken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Json(response.Content);
            }
            return Json($"Status code: {response.StatusCode}. {response.ErrorMessage}");
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        public JsonResult DeleteUser(string userId)
        {
            var response = _authAdmin.DeleteUser(userId);
            return Json(response.Content);

        }

        [HttpPatch]
        [Route("BlockUser")]
        public JsonResult BlockUser(BlockUserRequest blockUserRequest)
        {
            var response = _authAdmin.BlockUser(blockUserRequest);
            return Json(response.Content);
        }

        [HttpPost]
        [Route("CreateUser")]
        public JsonResult CreateUser(CreateUserRequest createUserRequest)
        {
            createUserRequest.password = "Habito@123"; //pasar a config
            createUserRequest.connection = "Username-Password-Authentication"; //pasar a config
            createUserRequest.name = createUserRequest.given_name + " " + createUserRequest.family_name;
            createUserRequest.nickname = createUserRequest.given_name.Substring(0,1) + createUserRequest.family_name;
            createUserRequest.picture = "https://habito1.com/wp-content/uploads/2020/07/logoh1-simbolo-blackdegradeold.svg"; //pasar a config
            createUserRequest.user_id = "";
            createUserRequest.verify_email = true;
            //createUserRequest.email_verified = true;
            var response = _authAdmin.CreateUser(createUserRequest);
            return Json(response.Content);
        }

        [HttpPatch]
        [Route("UpdateUser/{userId}")]
        public JsonResult UpdateUser(string userId, UpdateUserRequest updateUserRequest)
        {
            var response = _authAdmin.UpdateUser(updateUserRequest, userId);
            return Json(response.Content);
        }

        [HttpGet]
        [Route("GetByParent/{userId}")]
        public JsonResult GetByParent(string userId)
        {
            var response = _authAdmin.GetUsersByParent(userId);
            return Json(response.Content);
        }

        [HttpPost]
        [Route("SocialSignUp")]
        public async Task<JsonResult> SocialSignUp([FromBody] Dtos.Compania company)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var result = _authAdmin.FinishSignUp(company, accessToken);
            return Json(result);
        }

        [HttpGet]
        [Route("testToken")]
        public async Task<JsonResult> testToken(string userId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            
            var user = _authAdmin.GetCurrentUser(accessToken);

            return Json(accessToken);
        }
    }
}
