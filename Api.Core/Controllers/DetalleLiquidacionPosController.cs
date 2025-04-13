using Api.Core.Admin;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Dtos.Request;
using Api.Core.Helpers;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleLiquidacionPosController : BaseController<DetalleLiquidacionPosAdmin, int, Entities.DetalleLiquidacionPos, Dtos.DetalleLiquidacionPos, FilterDetalleLiquidacionPos>
    {
        private readonly IOmsEnvioService _omsEnvioService;
        public DetalleLiquidacionPosController(MyContext context, AppSettings appSettings, IOmsEnvioService omsEnvioService) : base(context, appSettings)
        {
            _omsEnvioService = omsEnvioService;
        }

        [HttpGet("liquidacion")]
        public async Task<ActionResult<List<Dtos.DetalleLiquidacionPos>>> GetAsync([FromQuery] int id)
        {
            return await _admin.GetByLiquidacionAsync(id);
        }


        [HttpPost("GeneraLiquidacion")]
        public async Task<IActionResult> GeneraLiquidacionAsync([FromBody] GeneraLiquidacionEnviosRequest generaLiquidacionEnviosRequest)
        {

            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);
            await _admin.GeneraLiquidacionesByDeliveries(generaLiquidacionEnviosRequest);

            return Ok();
        }

        [HttpPost("GeneraAllLiquidaciones")]
        public async Task<IActionResult> GeneraAllLiquidacionAsync([FromBody] GeneraAllLiquidacionesEnviosRequest request)
        {

            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);
            await _admin.GeneraLiquidacionesByAllDeliveries(request);

            return Ok();
        }

        [HttpPost("Excel")]
        public async Task<IActionResult> ExportToExcelAsync([FromBody] IList<int> liquidacionesPosIds)
        {
            var data = await _admin.GetAllForReportAsync(liquidacionesPosIds);

            return File(data.GetExcelContent("Liquidaciones Clientes Pospagos"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Liquidaciones Clientes Pospagos.xlsx");
        }


        [HttpPost("ExportAllToExcel")]
        public async Task<IActionResult> ExportAllToExcelAsync([FromBody] ExportAllEnviosRequest request)
        {
            var data = await _admin.GetAllForReportAsync(request);

            return File(data.GetExcelContent("Liquidaciones Clientes Pospagos"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Liquidaciones Clientes Pospagos.xlsx");
        }

        [HttpPost("SyncOms")]
        public async Task<IActionResult> SyncOms([FromBody] SyncOmsEnvioRequest request)
        {
            //PSD validacion de la fecha
            if (request.DateTimeTo != null && request.DateTimeTo < request.DateTimeFrom)
                return BadRequest("La fecha hasta no puede ser menor que la fecha desde");

            await _omsEnvioService.Sync(request.DateTimeFrom, request.DateTimeTo);

            return Ok();
        }

        [HttpPut("{id}/Cancel")]
        public async Task<IActionResult> CancelAsync(int id)
        {
            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);

            await _admin.CancelAsync(id);

            return NoContent();
        }
    }
}
