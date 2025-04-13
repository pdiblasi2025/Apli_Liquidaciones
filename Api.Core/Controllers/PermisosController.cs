using Api.Core.Admin;
using Api.Core.Helpers;
using Api.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/permisos/")]
    [ApiController]
    public class PermisosController : ControllerBase
    {

        private Admin.AsignacionAdmin _admin2;
        private Admin.PermisosAdmin _admin;
        public readonly IOptions<AppSettings> _appSettings;
        private string _domain;
        private string _token;
        private string _audience;
         
        public PermisosController(IOptions<AppSettings> appSettings, MyContext context)
        {
            _appSettings = appSettings;
            _domain = _appSettings.Value.Authority;
            _audience = _appSettings.Value.Audience;
            //_token = _appSettings.Value.APIManagementToken;
            AuthAdmin auth = new AuthAdmin(appSettings.Value, context);
            _token = auth.GetManagementAPIToken();
            _admin = new PermisosAdmin(_token, _domain, _audience);
            _admin2 = new AsignacionAdmin(_token, _domain);
        }

        [PermissionFilterAttribute(permission = "write:permisos")]
        [Route("roles/{roleId}/permissions")]
        [HttpPost]
        public IActionResult AsignPermissionToRole([FromBody] Dtos.PreviousPermission dto, [FromRoute] string roleId)
        {

            var response = _admin.AssignPermissionToRol(roleId,dto); 

            if (response.Count>0)
            {
                return Ok(response );
            }
            else
            {
                return BadRequest("Error en Asignación");
            }
        }

        [PermissionFilterAttribute(permission = "write:permisos")]
        [Route("roles/{roleId}/permissions")]
        [HttpPut]
        public IActionResult UnasignPermissionToRole([FromBody] Dtos.PreviousPermission dto, [FromRoute] string roleId)
        {

            var response = _admin.UnassignPermissionToRol(roleId, dto);

            if (response.Count > 0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest("Error en Asignación");
            }
        }
        [PermissionFilterAttribute(permission = "read:permisos")]
        [HttpGet("roles/{roleId}/permissions")]
        public IActionResult GetOne([FromRoute] string roleId )
        {

            var response = _admin.GetPermissionsFromRole(roleId );

            if (response !=null)
            {
                return Ok(response );
            }
            else
            {
                return BadRequest("Error Interno");
            }
        }

        [PermissionFilterAttribute(permission = "read:permisos")]
        [HttpGet("roles")]
        public IActionResult GetAll()
        { 

            var response = _admin2.getRolesCombo();


            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest("Error Interno");
            }
        }


    }
}
