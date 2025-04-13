using Api.Core.Dtos.Filter.Oms;
using Api.Core.Entities;
using Api.Core.Enums;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class OmsOrdenService : IOmsOrdenService
    {
        private readonly MyContext _dbContext;
        private readonly IOmsService _omsService;
        private readonly IOmsSyncLogService _omsSyncLogService;
        private static CultureInfo _culture = CultureInfo.CreateSpecificCulture("en-US");
        private static NumberStyles _style = NumberStyles.Number;
        private static int _estadoPago = 56;

        public OmsOrdenService(MyContext dbContext, IOmsService omsService, IOmsSyncLogService omsSyncLogService)
        {
            _dbContext = dbContext;
            _omsService = omsService;
            _omsSyncLogService = omsSyncLogService;
        }

        public async Task Sync(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            await _omsSyncLogService.AddLogAsync($"Oms Order Sync process triggered", OsmJobType.Order);

            try
            {
                var clients = await _dbContext.Clientes
                                .AsNoTracking()
                                .Where(x => x.TipoCliente == TipoCliente.Prepago &&
                                            !x.Deleted)
                                .ToListAsync();

                var orders = new List<DetalleLiquidacionPre>();

                foreach (var client in clients)
                {
                    var clientOrders = await _omsService.GetAllOrdersAsync(new OmsOrderRequestFilter
                    {
                        ClientId = client.OmsId,
                        StateId = _estadoPago,
                        DateFrom = dateFrom,
                        DateTo = dateTo
                    });

                    foreach (var clientOrder in clientOrders)
                    {
                        if (!decimal.TryParse(clientOrder.amount_no_tax, _style, _culture, out decimal amountNoTax))
                            continue;

                        var codigosSeguimiento = clientOrder.orders.Where(x => !string.IsNullOrEmpty(x.tracking_id)).Select(x => x.tracking_id).ToList();




                        orders.Add(new DetalleLiquidacionPre
                        {
                            ClienteId = client.Id,
                            OmsId = clientOrder.id,
                            IdOrdenPago = clientOrder.orders.FirstOrDefault()?.order_id,
                            CodigoOrdenPago = codigosSeguimiento.Any() ? string.Join(",", codigosSeguimiento) : null,
                            IdMercadoPago = clientOrder.element_id,
                            Urlpago = clientOrder.init_point,
                            IdPreferenciaMP = clientOrder.preference_id,
                            ValorSinImpuesto = amountNoTax,
                            Estado = EstadoItem.PendienteLiquidar,
                            //Fecha = DateTime.TryParseExact(clientOrder.date, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime fecha) ? fecha : DateTime.Now,
                            //PSD FIX003 - Aca debe guardar la fecha date_approved
                            Fecha = DateTime.TryParseExact(clientOrder.date_approved, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime fecha) ? fecha : DateTime.Now,
                            Enabled = true,
                            Deleted = false,
                            CreateDate = DateTime.Now,
                            CreatedBy = "OrderJobFIX003"
                        });
                    }
                }

                if (orders.Any())
                {
                    var uniqueOrders = orders.GroupBy(x => x.OmsId)
                        .Select(x => x.First())
                        .ToList();

                    var orderIds = uniqueOrders.Select(x => x.OmsId).ToHashSet();

                    var existingIds = await _dbContext.DetalleLiquidacionPre
                        .AsNoTracking()
                        .Where(x => orderIds.Contains(x.OmsId))
                        .Select(x => x.OmsId)
                        .ToListAsync();

                    var newOrders = uniqueOrders.Where(x => !existingIds.Contains(x.OmsId)).ToList();

                    if (newOrders.Any())
                        await _omsSyncLogService.AddLogAsync($"{newOrders.Count} order(s) will be added", OsmJobType.Order);

                    await _dbContext.AddRangeAsync(newOrders);
                    await _dbContext.SaveChangesAsync();
                }

                await _omsSyncLogService.AddLogAsync($"Oms Order Sync process finished", OsmJobType.Order);
            }
            catch (Exception ex)
            {
                await _omsSyncLogService.AddLogAsync($"Error when running Oms Order Sync process - {ex.Message}", OsmJobType.Order);
            }

        }
    }
}
