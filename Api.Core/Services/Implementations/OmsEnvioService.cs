using Api.Core.Dtos.Filter.Oms;
using Api.Core.Dtos.Oms;
using Api.Core.Entities;
using Api.Core.Enums;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class OmsEnvioService : IOmsEnvioService
    {
        private readonly MyContext _dbContext;
        private readonly IOmsService _omsService;
        private readonly IOmsSyncLogService _omsSyncLogService;
        private static CultureInfo _culture = CultureInfo.CreateSpecificCulture("en-US");
        private static NumberStyles _style = NumberStyles.Number;
        //private static DateTime FechaLiquidable;

        public OmsEnvioService(MyContext dbContext, IOmsService omsService, IOmsSyncLogService omsSyncLogService)
        {
            _dbContext = dbContext;
            _omsService = omsService;
            _omsSyncLogService = omsSyncLogService;
        }

        public async Task Sync(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            await _omsSyncLogService.AddLogAsync($"Oms Envios Sync Inicio el proceso", OsmJobType.Shipping);

            try
            {
                var clients = await _dbContext.Clientes
                    .AsNoTracking()
                    .Where(x => x.TipoCliente == TipoCliente.Pospago &&
                                !x.Deleted)
                    .ToListAsync();

                var shippings = new List<DetalleLiquidacionPos>();

                foreach (var client in clients)
                {
                    var deliveryModes = client.GetMetodosEnvio();

                    foreach (var deliveryMode in deliveryModes)
                    {
                        foreach (var state in deliveryMode.EstadosFacturacion)
                        {
                            int EstadoFacturable = state;
                            int ModoDeEnvio = deliveryMode.MetodoEnvio;

                            var clientShippings = await _omsService.GetAllShippingsAsync(new OmsShippingRequestFilter
                            {
                                ClientId = client.OmsId,
                                StateId = state,
                                DeliveryMode = deliveryMode.MetodoEnvio,
                                StateDateFrom = dateFrom,
                                StateDateTo = dateTo
                            });
                            
                            

                            foreach (var clientShipping in clientShippings)
                            {
                                
                                String FechaL =  clientShipping.registered;

                                if (ModoDeEnvio == 1) //FIX004
                                {
                                    // Condiciones para el Puerta a Puerta

                                    switch (EstadoFacturable)
                                        {
                                            case 10:
                                                FechaL = clientShipping.discharged;
                                                break;
                                            case 25:
                                                FechaL = clientShipping.delivered;
                                                break;
                                            case 26:
                                                FechaL = clientShipping.not_delivered;
                                                break;
                                            case 51:
                                                FechaL = clientShipping.discharged;
                                                break;
                                            case 69:
                                                FechaL = clientShipping.registered;
                                                break;
                                        }
                                }
                                if (ModoDeEnvio == 4) //FIX004
                                    {
                                        // Condiciones para la Logistica Inversa

                                        switch (EstadoFacturable)
                                        {
                                            case 10:
                                                FechaL = clientShipping.discharged;
                                                break;
                                            case 25:
                                                FechaL = clientShipping.delivered;
                                                break;
                                            case 26:
                                                FechaL = clientShipping.not_delivered;
                                                break;
                                            case 51:
                                                FechaL = clientShipping.discharged;
                                                break;
                                            case 69:
                                                FechaL = clientShipping.registered;
                                                break;
                                        }
                                    }
                                if (ModoDeEnvio == 6)  //FIX004
                                        {
                                            // Condiciones para el Puerta a Puerta JU

                                            switch (EstadoFacturable)
                                            {
                                                case 10:
                                                    FechaL = clientShipping.discharged;
                                                    break;
                                                case 25:
                                                    FechaL = clientShipping.delivered;
                                                    break;
                                                case 26:
                                                    FechaL = clientShipping.not_delivered;
                                                    break;
                                                case 51:
                                                    FechaL = clientShipping.discharged;
                                                    break;
                                                case 69:
                                                    FechaL = clientShipping.registered;
                                                    break;
                                            }
                                        }
                                 
                                if (ModoDeEnvio == 8)  //RF000
                                    {
                                            // Condiciones para el FulfillmentRefrigerado

                                            switch (EstadoFacturable)
                                            {
                                                case 10:
                                                    FechaL = clientShipping.discharged;
                                                    break;
                                                case 25:
                                                    FechaL = clientShipping.delivered;
                                                    break;
                                                case 26:
                                                    FechaL = clientShipping.not_delivered;
                                                    break;
                                                case 51:
                                                    FechaL = clientShipping.discharged;
                                                    break;
                                                case 69:
                                                    FechaL = clientShipping.registered;
                                                    break;
                                            }
                                     }



                                if (!decimal.TryParse(clientShipping.value, _style, _culture, out decimal value) ||
                                    !decimal.TryParse(clientShipping.shipping_cost_no_tax, _style, _culture, out decimal shipping_cost_no_tax))
                                    continue;

                                DateTime fechaFormateada = DateTime.ParseExact(FechaL, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                


                                
                                shippings.Add(new DetalleLiquidacionPos
                                {
                                    ClienteId = client.Id,
                                    OmsId = clientShipping.id,
                                    Etiqueta = clientShipping.shipment_id,
                                    Cantidad = clientShipping.items_quantity,
                                    Valoritems = value,
                                    Peso = clientShipping.weight,
                                    Volumen = clientShipping.volume,
                                    Ancho = clientShipping.width,
                                    Largo = clientShipping.length,
                                    Alto = clientShipping.height,
                                    ValorSinImpuesto = shipping_cost_no_tax,
                                    Estado = EstadoItem.PendienteLiquidar,
                                    Fecha = fechaFormateada, //FIX003
                                    Enabled = true,
                                    Deleted = false,
                                    CreateDate = DateTime.Now,
                                    CreatedBy = "EnviosJob-fix004"
                                });
                            }
                        }
                    }
                }
                // Verifico si hay algun envio en shippings
                if (shippings.Any())
                {
                    var uniqueShippings = shippings.GroupBy(x => x.OmsId)
                        .Select(x => x.First())
                        .ToList();

                    var shippingIds = uniqueShippings.Select(x => x.OmsId).ToHashSet();

                    var existingIds = await _dbContext.DetalleLiquidacionPos
                        .AsNoTracking()
                        .Where(x => shippingIds.Contains(x.OmsId))
                        .Select(x => x.OmsId)
                        .ToListAsync();

                    var newShippings = uniqueShippings.Where(x => !existingIds.Contains(x.OmsId)).ToList();
                    // Arriba hice la verificacion si el envio ya existia en la base de datos.

                    if (newShippings.Any())
                        await _omsSyncLogService.AddLogAsync($"{newShippings.Count} Envio(s) encontrados", OsmJobType.Shipping);
                    
                    // Sino exite en la base lo grabo.
                    await _dbContext.AddRangeAsync(newShippings); // inserto los datos a la base
                    await _dbContext.SaveChangesAsync(); //Grabo async los datos en la base
                }

                await _omsSyncLogService.AddLogAsync("Oms Envios Sync a finalizado", OsmJobType.Shipping);

            }
            catch (Exception ex)
            {
                await _omsSyncLogService.AddLogAsync($"Error when running Oms Shipping Sync process - {ex.Message}", OsmJobType.Shipping);
            }
        }

        //psd
        //private List<DetalleLiquidacionPos> GetShippings(List<OmsShippingDto> clientShippings, Cliente client, DateTime date)
        //{
        //    var shippings = new List<DetalleLiquidacionPos>();

        //    foreach (var clientShipping in clientShippings)
        //    {
        //        if (!decimal.TryParse(clientShipping.value, _style, _culture, out decimal value) ||
        //            !decimal.TryParse(clientShipping.shipping_cost_no_tax, _style, _culture, out decimal shipping_cost_no_tax))
        //            continue;

        //        shippings.Add(new DetalleLiquidacionPos
        //        {
        //            ClienteId = client.Id,
        //            OmsId = clientShipping.id,
        //            Etiqueta = clientShipping.shipment_id,
        //            Cantidad = clientShipping.items_quantity,
        //            Valoritems = value,
        //            Peso = clientShipping.weight,
        //            Volumen = clientShipping.volume,
        //            Ancho = clientShipping.width,
        //            Largo = clientShipping.length,
        //            Alto = clientShipping.height,
        //            ValorSinImpuesto = shipping_cost_no_tax,
        //            Estado = EstadoItem.PendienteLiquidar,
        //            Fecha = date,
        //            Enabled = true,
        //            Deleted = false,
        //            CreateDate = DateTime.Now,
        //            CreatedBy = "ShippingJob"
        //        });
        //    }

        //    return shippings;
        //}

        //private List<DateTime> GetDateTimes(DateTime start, DateTime end)
        //{
        //    var dates = new List<DateTime>();

        //    for (DateTime date = start; date <= end; date = date.AddDays(1))
        //    {
        //        dates.Add(date);
        //    }

        //    return dates;
        //}

        //psd

    }
}
