
using Api.Core.Admin;
using Api.Core.Dtos;
using Api.Core.Dtos.Filter;
using Api.Core.Helpers;
using Api.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/asignacion")]
    [ApiController]
    public class AsignacionController : ControllerBase
    {


        private Admin.AsignacionAdmin _admin;
        public readonly IOptions<AppSettings> _appSettings;
        private string _domain;
        private string _token;

        public AsignacionController(IOptions<AppSettings> appSettings, MyContext context)
        {
            _appSettings = appSettings;
            _domain = _appSettings.Value.Authority;
            AuthAdmin auth = new AuthAdmin(appSettings.Value, context);
            _token = auth.GetManagementAPIToken();

            _admin = new Admin.AsignacionAdmin(_token, _domain);

        }
        [PermissionFilterAttribute(permission = "write:roles")]
        [Route("users/{userId}/roles")]
        [HttpPost]
        public IActionResult AsignRoleToUser([FromBody] Dtos.AsignRoleToUserAuth0 dto,[FromRoute]string userId)
        {
            
            var response = _admin.AsignRoleToUser(dto,userId);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.ErrorException);
            }
        }
        [PermissionFilterAttribute(permission = "write:roles")]
        [Route("users/{userId}/roles")]
        [HttpPut]
        public IActionResult UnasignRoleToUser([FromBody] Dtos.AsignRoleToUserAuth0 dto, [FromRoute] string userId)
        {

            var response = _admin.UnasignRoleToUser(dto, userId);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.ErrorException);
            }
        }

        [PermissionFilterAttribute(permission = "read:roles")]
        [Route("users")]
        [HttpGet]
        public IActionResult getUsers( )
        {
             
            var users = _admin.GetUsers( );

             

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UsuarioAuth>>(users.Content);

            List<DtoUser> simpleUsers = new List<DtoUser>();
            

            foreach (UsuarioAuth usu in response)
            {
                simpleUsers.Add(new DtoUser()
                {
                     UserEmail = usu.email,
                      userId = usu.user_id,
                       userName = usu.name,
                        text = usu.email,
                        value = usu.user_id,
                });
            }

            if (simpleUsers != null)
            {
                return Ok(simpleUsers);
            }
            else
            {
                return BadRequest("Error Interno");
            }
        }


        [PermissionFilterAttribute(permission = "read:roles")]
        [Route("roles/users/{userId}")]
        [HttpGet]
        public IActionResult getRoles([FromRoute]string  userId)
        {

            var response = _admin.GetRolesFromUserTable(userId);
            
             

            if (response!=null)
            {
                return Ok(response  );
            }
            else
            {
                return BadRequest("Error Interno");
            }


        }

       
    }
}
