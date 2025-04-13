using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Core.Helpers
{
    public class PermissionFilterAttribute : ActionFilterAttribute
    {
        public string permission { get; set; }

        public object role { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _permission = permission;   

            var authorized  = false;
            var token = context.HttpContext.Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value.ToString();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new BadRequestObjectResult( new Exception($@"Token No encontrado {permission} "));                
            }
            else
            {
                var jwt = token.Substring(7);
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken stringtoken = handler.ReadJwtToken(jwt);
                List<System.Security.Claims.Claim> permissions = stringtoken.Claims.Where(claim => claim.Type == "permissions").ToList();

                foreach (System.Security.Claims.Claim c in permissions)
                {
                    if (c.Value == permission)
                    {
                        authorized = true;
                        break;
                    }
                }

                if (authorized == true)
                {
                    await next();
                }
                else
                {
                    context.Result = new BadRequestObjectResult(new Exception($@"Unauthorized {permission} "){});
                }
            }
        }

    }
}
