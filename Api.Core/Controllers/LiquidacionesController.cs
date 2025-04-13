using Api.Core.Admin;
using Api.Core.Dtos;
using Api.Core.Dtos.Filter;
using Api.Core.Dtos.Request;
using Api.Core.Dtos.Response;
using Api.Core.Helpers;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LiquidacionesController : BaseController<LiquidacionAdmin, int, Entities.Liquidacion, Dtos.Liquidacion, FilterLiquidacion>
    {
        private const string ApiKey = "X-API-Key";
        private readonly string _apiKeyValue;

        public LiquidacionesController(MyContext context, IOptions<AppSettings> appSettings, IErpMilongaInvoiceService erpMilongaService) : base(context, appSettings.Value)
        {
            _admin.ErpMilongaInvoiceService = erpMilongaService;
            _apiKeyValue = appSettings.Value.ApiKey;
        }

        //[Authorize(Roles = "Administracion,Socio,SuperAdmin")]
        [HttpPut("updateStatus")]
        public LiquidacionUpdateStatusResponse UpdateStatus([FromBody] UpdateStatusLiquidacionRequest updateStatusLiquidacion)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);

            return _admin.UpdateStatus(updateStatusLiquidacion);
        }

        [HttpPut("UpdateAllStatus")]
        public async Task<IActionResult> UpdateAllStatus([FromBody] UpdateAllLiquidacionesRequest request)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);

            await _admin.UpdateAllStatus(request);

            return NoContent();
        }

        [HttpPost("Excel")]
        public async Task<IActionResult> ExportToExcelAsync(IList<int> liquidacionesIds)
        {
            var data = await _admin.GetAllForReportAsync(liquidacionesIds);

            return File(data.GetExcelContent("Liquidaciones"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Liquidaciones.xlsx");
        }

        [HttpPost("ExportAllToExcel")]
        public async Task<IActionResult> ExportAllToExcelAsync(ExportAllLiquidacionesRequest request)
        {
            var data = await _admin.GetAllForReportAsync(request);

            return File(data.GetExcelContent("Liquidaciones"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Liquidaciones.xlsx");
        }

        [HttpPost("{id}/Concepto")]
        public async Task<IActionResult> AddConceptoAsync(int id, [FromBody] ConceptoLiquidacionRequest request)
        {
            await _admin.AddConceptoAsync(id, request);

            return Ok();
        }

        [HttpDelete("{id}/Concepto/{conceptoId}")]
        public async Task<IActionResult> AnularConceptoAsync(int id, int conceptoId)
        {
            await _admin.AnularConceptoAsync(id, conceptoId);

            return Ok();
        }

        [HttpGet("{id}/Comprobante")]
        public async Task<ActionResult<List<Comprobante>>> GetAllComprobantes(int id)
        {
            return await _admin.GetAllComprobantes(id);
        }

        [HttpGet("{id}/Comprobante/{archivoId}")]
        public async Task<IActionResult> GetComprobante(int id, int archivoId)
        {
            var comprobante = await _admin.GetComprobante(id, archivoId);

            return File(comprobante.Contenido, "application/octet-stream", comprobante.Nombre);
        }

        [HttpPost("{id}/Comprobante")]
        public async Task<IActionResult> AddComprobante(int id, [FromForm] ComprobantePost request)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);

            await _admin.AddComprobante(id, request);

            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpDelete("{id}/Comprobante/{archivoId}")]
        public async Task<IActionResult> DeleteComprobante(int id, int archivoId)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);

            await _admin.RemoveComprobante(id, archivoId);

            return NoContent();
        }




        [HttpPut("sendErp")]
        public async Task<IActionResult> SendErp([FromBody] SendErpRequest updateStatusLiquidacion)
        {
            var response = await _admin.SendLiquidacionesToErp(updateStatusLiquidacion);

            return Ok(response);
        }

        [HttpPut("SendAllErp")]
        public async Task<IActionResult> SendAllErp([FromBody] SendAllToErpRequest request)
        {
            var response = await _admin.SendAllLiquidacionesToErp(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPut("{id}/Facturacion")]
        public async Task<IActionResult> DatosFacturacion(int id, [FromBody] LiquidacionDatosFacturacionRequest datosFacturacionRequest)
        {
            if (!HttpContext.Request.Headers.TryGetValue(ApiKey, out var extractedApiKey))
                return Unauthorized();

            if (!_apiKeyValue.Equals(extractedApiKey))
                return Unauthorized();

            var response = await _admin.UpdateDatosFacturacion(id, datosFacturacionRequest);

            if (response == 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Error interno");
            }

        }

        [AllowAnonymous]
        [HttpPost("{id}/Pago")]
        public async Task<IActionResult> Pago([FromBody] LiquidacionPagoRequest liquidacionPagoRequest)
        {
            if (!HttpContext.Request.Headers.TryGetValue(ApiKey, out var extractedApiKey))
                return Unauthorized();

            if (!_apiKeyValue.Equals(extractedApiKey))
                return Unauthorized();

            var response = await _admin.AddLiquidacionPago(liquidacionPagoRequest);

            if (response == 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Error interno");
            }

        }

    }
}
