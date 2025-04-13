using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Core.Admin;
using Api.Core.Dtos.Common;
using Api.Core.Repositories;
using Api.Core.Dtos.Filter;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/Role/")]
   
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleAdmin _admin;
        public readonly IOptions<AppSettings> _appSettings;
        private string _domain;
        private string _token;

        public RoleController( IOptions<AppSettings> appSettings, MyContext context)
        {
            _appSettings = appSettings;
            _domain = _appSettings.Value.Authority;

            AuthAdmin auth = new AuthAdmin(appSettings.Value, context);
            _token = auth.GetManagementAPIToken();
            //_token = _appSettings.Value.APIManagementToken;

            _admin = new RoleAdmin(_token, _domain);

        }

         
        [HttpPut]
        public IActionResult Update([FromQuery] string id,[FromBody] Dtos.UpdateRole dto)
        { 

            var response = _admin.Update(id, dto);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.ErrorException);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Dtos.CreateRole dto)
        {

            var response = _admin.Create(dto);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.ErrorException);
            }
        }

        [HttpGet]
        public ActionResult<PagedListResponse<GetRoleOuput>> BasicQuery([FromQuery] FilterRole filter)
        {

            var response = _admin.BasicQuery(filter);
            if (response.Count>0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response );
            }

        } 
        [HttpGet("all/")]
        public IActionResult GetAll()
        {

            var response = _admin.GetAll();
            if (response.Count > 0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }


        [HttpDelete("{roleId}")]
        public IActionResult Delete([FromRoute] string roleId)
        {   
            IRestResponse response = _admin.Delete(roleId);

            if (string.IsNullOrEmpty(roleId))
            {
                return BadRequest(new Exception("Role Id es un parámetro obligatorio"));
            }

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.ErrorException);
            }

        }

    }

    public class GetRoleOuput
    {
        public string id { get; set; }

        public string name { get; set; }

        public string description { get; set; }
    }
}