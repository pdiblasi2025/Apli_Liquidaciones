using Api.Core.Admin;
using Api.Core.Dtos;
using Api.Core.Dtos.Filter;
using Api.Core.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    //[Authorize(Roles = "SuperAdmin")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : BaseController<ClienteAdmin, int, Entities.Cliente, Dtos.Cliente, FilterCliente>
    {
        public ClienteController(MyContext context, AppSettings appSettings) : base(context, appSettings)
        {
        }

        [HttpGet("{id}/IngresosBrutos")]
        public async Task<ActionResult<List<IngresosBrutosArchivo>>> GetAllIngresosBrutosArchivo(int id)
        {
            return await _admin.GetAllIngresosBrutosArchivo(id);
        }

        [HttpGet("{id}/IngresosBrutos/{archivoId}")]
        public async Task<IActionResult> GetIngresosBrutosArchivo(int id, int archivoId)
        {
            var ingresosBrutosArchivo = await _admin.GetIngresosBrutosArchivo(id, archivoId);

            return File(ingresosBrutosArchivo.Contenido, "application/octet-stream", ingresosBrutosArchivo.Nombre);
        }

        [HttpPost("{id}/IngresosBrutos")]
        public async Task<IActionResult> PostIngresosBrutosArchivo(int id, [FromForm] IngresosBrutosArchivoPost request)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);

            await _admin.SaveIngresosBrutosArchivo(id, request);

            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpDelete("{id}/IngresosBrutos/{archivoId}")]
        public async Task<IActionResult> DeleteIngresosBrutosArchivo(int id, int archivoId)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);

            await _admin.RemoveIngresosBrutosArchivo(id, archivoId);

            return NoContent();
        }
    }

}
