using Api.Core.Admin;
using Api.Core.Dtos;
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
    public class DetalleLiquidacionPreController : BaseController<DetalleLiquidacionPreAdmin, int, Entities.DetalleLiquidacionPre, Dtos.DetalleLiquidacionPre, FilterDetalleLiquidacionPre>
    {
        private readonly IOmsOrdenService _omsOrdenService;
        public DetalleLiquidacionPreController(MyContext context, AppSettings appSettings, IOmsOrdenService omsOrdenService) : base(context, appSettings)
        {
            _omsOrdenService = omsOrdenService;
        }


        [HttpGet("liquidacion")]
        public async Task<ActionResult<List<DetalleLiquidacionPre>>> GetAsync([FromQuery] int id)
        {
            return await _admin.GetByLiquidacionAsync(id);
        }
        


        [HttpPost("GeneraLiquidacion")]
        public async Task<IActionResult> GeneraLiquidacionAsync([FromBody] GeneraLiquidacionOrdenesRequest generaLiquidacionOrdenesRequest)
        {

            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);
            await _admin.GeneraLiquidacionesByOrders(generaLiquidacionOrdenesRequest);

            return Ok();
        }

        [HttpPost("GeneraAllLiquidaciones")]
        public async Task<IActionResult> GeneraAllLiquidacionAsync([FromBody] GeneraAllLiquidacionesOrdenesRequest request)
        {

            _admin.CurrentUser = _admin.GetCurrentUser(HttpContext.GetTokenAsync("access_token").Result, _appSettings);
            await _admin.GeneraLiquidacionesByAllOrders(request);

            return Ok();
        }

        [HttpPost("Excel")]
        public async Task<IActionResult> ExportToExcelAsync([FromBody] IList<int> liquidacionesPreIds)
        {
            var data = await _admin.GetAllForReportAsync(liquidacionesPreIds);

            return File(data.GetExcelContent("Liquidaciones Clientes Prepagos"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Liquidaciones Clientes Prepagos.xlsx");
        }

        [HttpPost("ExportAllToExcel")]
        public async Task<IActionResult> ExportAllToExcelAsync([FromBody] ExportAllOrdenesRequest request)
        {
            var data = await _admin.GetAllForReportAsync(request);

            return File(data.GetExcelContent("Liquidaciones Clientes Prepagos"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Liquidaciones Clientes Prepagos.xlsx");
        }

        [HttpPost("SyncOms")]
        public async Task<IActionResult> SyncOms([FromBody] SyncOmsOrdenRequest request)
        {
            await _omsOrdenService.Sync(request.DateTimeFrom, request.DateTimeTo);

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
