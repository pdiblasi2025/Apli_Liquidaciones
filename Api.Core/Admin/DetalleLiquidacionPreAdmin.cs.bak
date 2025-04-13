using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Dtos.Request;
using Api.Core.Entities;
using Api.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Admin
{
    public class DetalleLiquidacionPreAdmin : BaseAdmin<int, Entities.DetalleLiquidacionPre, Dtos.DetalleLiquidacionPre, FilterDetalleLiquidacionPre>
    {
        public override IQueryable GetQuery(FilterDetalleLiquidacionPre filter)
        {
            var query = MyContext.DetalleLiquidacionPre.Include(x => x.Cliente).OrderByDescending(x => x.Fecha).AsQueryable();

            query = query.Where(e =>
               !e.Deleted &&
               (filter.ClienteId == null || e.ClienteId == filter.ClienteId) &&
               (filter.Fecha_Desde == null || e.Fecha.Date >= filter.Fecha_Desde.Value.Date) &&
               (filter.Fecha_Hasta == null || e.Fecha.Date <= filter.Fecha_Hasta.Value.Date) &&
               ((filter.Estado == null || filter.Estado == 0) || e.Estado == filter.Estado) &&
               (string.IsNullOrEmpty(filter.MultiColumnSearchText) || e.CodigoOrdenPago.Contains(filter.MultiColumnSearchText, StringComparison.OrdinalIgnoreCase) ||
               e.IdMercadoPago.Contains(filter.MultiColumnSearchText, StringComparison.OrdinalIgnoreCase)));

            return query;
        }

        public async Task<List<Dtos.LiquidacionPreReport>> GetAllForReportAsync(IList<int> liquidacionesPreIds)
        {
            var liquidacionesPre = await MyContext.DetalleLiquidacionPre.AsNoTracking()
                .Include(MyContext.GetIncludePaths(typeof(DetalleLiquidacionPre)))
                .Where(x => liquidacionesPreIds.Contains(x.Id))
                .ToListAsync();

            return Mapper.Map<List<Dtos.LiquidacionPreReport>>(liquidacionesPre);
        }

        public async Task<List<Dtos.LiquidacionPreReport>> GetAllForReportAsync(ExportAllOrdenesRequest request)
        {
            var ordenes = await MyContext.DetalleLiquidacionPre
                .Where(x => !x.Deleted &&
                   (request.ClienteId == null || x.ClienteId == request.ClienteId) &&
                   (request.FechaDesde == null || x.Fecha.Date >= request.FechaDesde.Value.Date) &&
                   (request.FechaHasta == null || x.Fecha.Date <= request.FechaHasta.Value.Date) &&
                   (request.Estado == null || request.Estado == 0 || x.Estado == request.Estado) &&
                   (string.IsNullOrEmpty(request.Search) || x.CodigoOrdenPago.Contains(request.Search, StringComparison.OrdinalIgnoreCase) ||
                   x.IdMercadoPago.Contains(request.Search, StringComparison.OrdinalIgnoreCase)) &&
                   (request.UncheckedIds == null || request.UncheckedIds.Length == 0 || !request.UncheckedIds.Contains(x.Id)))
                .Include(x => x.Cliente)
                .AsNoTracking()
                .ToListAsync();

            return Mapper.Map<List<Dtos.LiquidacionPreReport>>(ordenes);
        }

        public override DetalleLiquidacionPre ToEntity(Dtos.DetalleLiquidacionPre dto)
        {
            var entity = new Entities.DetalleLiquidacionPre();

            if (dto.Id.HasValue)
            {
                entity = MyContext.DetalleLiquidacionPre.Single(e => e.Id == dto.Id.Value);
            }
            entity.LiquidacionId = dto.LiquidacionId;
            entity.ClienteId = dto.ClienteId;
            //entity.Cliente = MyContext.Clientes.FirstOrDefault(e => e.OmsId == dto.IdClienteOMS);
            entity.CodigoOrdenPago = dto.CodigoOrdenPago;
            entity.IdMercadoPago = dto.IdMercadoPago;
            entity.Urlpago = dto.Urlpago;
            entity.IdPreferenciaMP = dto.IdPreferenciaMP;
            entity.IdOrdenPago = dto.IdOrdenPago;
            entity.OtrosGastos = dto.OtrosGastos;
            entity.Estado = dto.Estado;
            return entity;
        }

        public async Task CancelAsync(int id)
        {
            var orden = await MyContext.DetalleLiquidacionPre.FirstOrDefaultAsync(x => x.Id == id);

            if (orden == null)
            {
                var result = new Result
                {
                    HasErrors = true
                };

                result.Messages.Add($"No existe ninguna orden con Id {id}");

                throw new BadRequestException(result);
            }
            else if (orden.LiquidacionId == null)
            {
                var result = new Result
                {
                    HasErrors = true
                };

                result.Messages.Add($"La orden no se encuentra asociada a ninguna liquidación");

                throw new BadRequestException(result);
            }
            else
            {
                var liquidacion = await MyContext.Liquidaciones
                    .Include(MyContext.GetIncludePaths(typeof(Liquidacion)))
                    .FirstOrDefaultAsync(x => x.Id == orden.LiquidacionId);

                var conceptoGenerico = await MyContext.Conceptos.FirstOrDefaultAsync(x => x.IsGeneric);

                var conceptoLiquidacion = liquidacion.Conceptos.First(x => x.ConceptoId == conceptoGenerico.Id);

                conceptoLiquidacion.Monto -= orden.ValorSinImpuesto;

                liquidacion.Saldo -= orden.ValorSinImpuesto;

                orden.Estado = EstadoItem.PendienteLiquidar;
                orden.LiquidacionId = null;
                orden.UpdateDate = DateTime.Now;
                orden.UpdatedBy = CurrentUser?.user_id ?? "Admin";

                await MyContext.SaveChangesAsync();
            }
        }

        public override Dtos.Response.ResultMessage<Dtos.DetalleLiquidacionPre> Validate(Dtos.DetalleLiquidacionPre dto, int Type)
        {
            // Type: 1-Create, 2-Update, 3-Delete 
            var entity = new Entities.DetalleLiquidacionPre();
            Dtos.Response.ResultMessage<Dtos.DetalleLiquidacionPre> respuesta_resultado = new Dtos.Response.ResultMessage<Dtos.DetalleLiquidacionPre>() { Content = dto, Successful = true, Message = "OK" };

            switch (Type)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }

            return respuesta_resultado;
        }

        public async Task<List<Dtos.DetalleLiquidacionPre>> GetByLiquidacionAsync(int id)
        {
            var query = await MyContext.DetalleLiquidacionPre.Include(c => c.Cliente).Where(e =>
                !e.Deleted &&
                e.LiquidacionId == id).ToListAsync();

            try
            {
                return Mapper.Map<List<Dtos.DetalleLiquidacionPre>>(query);
            }
            catch (Exception ex)
            {
                return new List<Dtos.DetalleLiquidacionPre>();
            }
        }

        public async Task GeneraLiquidacionesByAllOrders(GeneraAllLiquidacionesOrdenesRequest request)
        {
            var ordenes = await MyContext.DetalleLiquidacionPre
                .Where(x => x.Estado == EstadoItem.PendienteLiquidar &&
                    !x.Deleted &&
                   (request.ClienteId == null || x.ClienteId == request.ClienteId) &&
                   (request.FechaDesde == null || x.Fecha.Date >= request.FechaDesde.Value.Date) &&
                   (request.FechaHasta == null || x.Fecha.Date <= request.FechaHasta.Value.Date) &&
                   (string.IsNullOrEmpty(request.Search) || x.CodigoOrdenPago.Contains(request.Search, StringComparison.OrdinalIgnoreCase) ||
                   x.IdMercadoPago.Contains(request.Search, StringComparison.OrdinalIgnoreCase)) &&
                   (request.UncheckedIds == null || request.UncheckedIds.Length == 0 || !request.UncheckedIds.Contains(x.Id)))
                .ToListAsync();

            await GeneraLiquidaciones(ordenes, request.Descripcion);
        }

        private async Task GeneraLiquidaciones(List<DetalleLiquidacionPre> ordenes, string descripcion)
        {
            var detalleGroupedByClient = ordenes.GroupBy(x => x.ClienteId)
                .Select(x => new 
                { 
                    Id = x.Key,
                    ValorSinImpuestos = x.Sum(x => x.ValorSinImpuesto),
                    Detalle = x }
                );

            var conceptoGenerico = await MyContext.Conceptos.FirstOrDefaultAsync(x => x.IsGeneric);

            foreach (var detalleGroup in detalleGroupedByClient)
            {
                var cliente = await MyContext.Clientes
                    .Include(MyContext.GetIncludePaths(typeof(Cliente)))
                    .SingleOrDefaultAsync(x => x.Id == detalleGroup.Id);

                var liquidacion = new Liquidacion();

                if (cliente != null)
                {
                    liquidacion.IdCliente = cliente.Id;
                    liquidacion.Estado = Enums.EstadoLiquidacion.PendienteDeAutorizacion;
                    liquidacion.Descripcion = descripcion ?? "Generada x sistema desde Ordenes de Pago";
                    liquidacion.Enabled = true;
                    liquidacion.CreateDate = DateTime.Now;
                    liquidacion.CreatedBy = CurrentUser?.user_id == null ? "Admin" : CurrentUser.user_id.ToString();
                    liquidacion.Conceptos.Add(new ConceptoLiquidacion
                    {
                        ConceptoId = conceptoGenerico.Id,
                        Monto = detalleGroup.ValorSinImpuestos,
                        Observacion = "Concepto Genérico",
                        Enabled = true,
                        CreateDate = DateTime.Now,
                        Estado = EstadoConceptoCliente.Autorizado,
                        CreatedBy = CurrentUser?.user_id == null ? "Admin" : CurrentUser.user_id.ToString()
                    });
                    liquidacion.Saldo = liquidacion.Conceptos.Sum(x => x.Monto);
                }
                else
                {
                    throw new Exception($"No se encontro cliente OMS ID: {detalleGroup.Id}");
                }

                foreach (var detalle in detalleGroup.Detalle)
                {
                    detalle.Estado = Enums.EstadoItem.Liquidado;
                    detalle.UpdateDate = DateTime.Now;
                    detalle.UpdatedBy = CurrentUser?.user_id ?? "Admin";

                    liquidacion.DetalleLiquidacionPre.Add(detalle);
                }

                await MyContext.Liquidaciones.AddAsync(liquidacion);
                await MyContext.SaveChangesAsync();
            }
        }

        public async Task GeneraLiquidacionesByOrders(GeneraLiquidacionOrdenesRequest generaLiquidacionOrdenesRequest)
        {
            var detalleLiquidacionesPre = await MyContext.DetalleLiquidacionPre
                .Where(x => generaLiquidacionOrdenesRequest.ids_ordenes.Contains(x.Id) &&
                    x.Estado == EstadoItem.PendienteLiquidar)
                .ToListAsync();

            await GeneraLiquidaciones(detalleLiquidacionesPre, generaLiquidacionOrdenesRequest.descripcion);
        }

    }
}