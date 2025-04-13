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
    public class DetalleLiquidacionPosAdmin : BaseAdmin<int, Entities.DetalleLiquidacionPos, Dtos.DetalleLiquidacionPos, FilterDetalleLiquidacionPos>
    {
        public override IQueryable GetQuery(FilterDetalleLiquidacionPos filter)
        {
            var query = MyContext.DetalleLiquidacionPos
                .Include(x => x.Cliente)
                .OrderByDescending(x => x.Fecha)
                .AsNoTracking();

            query = query.Where(e =>
                !e.Deleted &&
                (filter.ClienteId == null || e.ClienteId == filter.ClienteId) &&
                (filter.Fecha_Desde == null || e.Fecha.Date >= filter.Fecha_Desde.Value.Date) &&
                (filter.Fecha_Hasta == null || e.Fecha.Date <= filter.Fecha_Hasta.Value.Date) &&
                ((filter.Estado == null || filter.Estado == 0) || e.Estado == filter.Estado) &&
                (string.IsNullOrEmpty(filter.MultiColumnSearchText) || e.Etiqueta.Contains(filter.MultiColumnSearchText, StringComparison.OrdinalIgnoreCase)));

            return query;
        }

        public async Task<List<Dtos.LiquidacionPosReport>> GetAllForReportAsync(IList<int> liquidacionesPosIds)
        {
            var liquidacionesPos = await MyContext.DetalleLiquidacionPos.AsNoTracking()
                .Include(MyContext.GetIncludePaths(typeof(DetalleLiquidacionPos)))
                .Where(x => liquidacionesPosIds.Contains(x.Id))
                .ToListAsync();

            return Mapper.Map<List<Dtos.LiquidacionPosReport>>(liquidacionesPos);
        }

        public async Task<List<Dtos.LiquidacionPosReport>> GetAllForReportAsync(ExportAllEnviosRequest request)
        {
            var envios = await MyContext.DetalleLiquidacionPos
                .Where(x => !x.Deleted &&
                   (request.ClienteId == null || x.ClienteId == request.ClienteId) &&
                   (request.FechaDesde == null || x.Fecha.Date >= request.FechaDesde.Value.Date) &&
                   (request.FechaHasta == null || x.Fecha.Date <= request.FechaHasta.Value.Date) &&
                   (request.Estado == null || request.Estado == 0 || x.Estado == request.Estado) &&
                   (string.IsNullOrEmpty(request.Search) || x.Etiqueta.Contains(request.Search, StringComparison.OrdinalIgnoreCase)) &&
                   (request.UncheckedIds == null || request.UncheckedIds.Length == 0 || !request.UncheckedIds.Contains(x.Id)))
                .Include(x => x.Cliente)
                .AsNoTracking()
                .ToListAsync();

            return Mapper.Map<List<Dtos.LiquidacionPosReport>>(envios);
        }

        public async Task<List<Dtos.DetalleLiquidacionPos>> GetByLiquidacionAsync(int id)
        {
            var query = await MyContext.DetalleLiquidacionPos.Include(c => c.Cliente).Where(e =>
                !e.Deleted &&
                e.LiquidacionId == id).ToListAsync();

            try
            {
                return Mapper.Map<List<Dtos.DetalleLiquidacionPos>>(query);
            }
            catch (Exception ex)
            {
                return new List<Dtos.DetalleLiquidacionPos>();
            }
        }


        public override DetalleLiquidacionPos ToEntity(Dtos.DetalleLiquidacionPos dto)
        {
            var entity = new Entities.DetalleLiquidacionPos();

            if (dto.Id.HasValue)
            {
                entity = MyContext.DetalleLiquidacionPos.Single(e => e.Id == dto.Id.Value);
            }

            entity.LiquidacionId = dto.LiquidacionId;
            entity.ClienteId = dto.ClienteId;
            entity.Etiqueta = dto.Etiqueta;
            entity.Cantidad = dto.Cantidad;
            entity.Valoritems = dto.Valoritems;
            entity.Peso = dto.Peso;
            entity.Volumen = dto.Volumen;
            entity.Ancho = dto.Ancho;
            entity.Largo = dto.Largo;
            entity.Alto = dto.Alto;
            entity.Estado = dto.Estado;
            return entity;
        }

        public async Task CancelAsync(int id)
        {
            var envio = await MyContext.DetalleLiquidacionPos.FirstOrDefaultAsync(x => x.Id == id);

            if (envio == null)
            {
                var result = new Result
                {
                    HasErrors = true
                };

                result.Messages.Add($"No existe ningún envío con Id {id}");

                throw new BadRequestException(result);
            }
            else if (envio.LiquidacionId == null)
            {
                var result = new Result
                {
                    HasErrors = true
                };

                result.Messages.Add($"El envío no se encuentra asociado a ninguna liquidación");

                throw new BadRequestException(result);
            }
            else
            {
                var liquidacion = await MyContext.Liquidaciones
                    .Include(MyContext.GetIncludePaths(typeof(Liquidacion)))
                    .FirstOrDefaultAsync(x => x.Id == envio.LiquidacionId);

                var conceptoGenerico = await MyContext.Conceptos.FirstOrDefaultAsync(x => x.IsGeneric);

                var conceptoLiquidacion = liquidacion.Conceptos.First(x => x.ConceptoId == conceptoGenerico.Id);

                conceptoLiquidacion.Monto -= envio.ValorSinImpuesto;

                liquidacion.Saldo -= envio.ValorSinImpuesto;

                envio.Estado = EstadoItem.PendienteLiquidar;
                envio.LiquidacionId = null;
                envio.UpdateDate = DateTime.Now;
                envio.UpdatedBy = CurrentUser?.user_id ?? "Admin";

                await MyContext.SaveChangesAsync();
            }
        }

        public override Dtos.Response.ResultMessage<Dtos.DetalleLiquidacionPos> Validate(Dtos.DetalleLiquidacionPos dto, int Type)
        {
            // Type: 1-Create, 2-Update, 3-Delete 
            var entity = new Entities.DetalleLiquidacionPos();
            Dtos.Response.ResultMessage<Dtos.DetalleLiquidacionPos> respuesta_resultado = new Dtos.Response.ResultMessage<Dtos.DetalleLiquidacionPos>() { Content = dto, Successful = true, Message = "OK" };

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
        
        public async Task GeneraLiquidacionesByAllDeliveries(GeneraAllLiquidacionesEnviosRequest request)
        {
            var envios = await MyContext.DetalleLiquidacionPos
                .Where(x => x.Estado == EstadoItem.PendienteLiquidar &&
                    !x.Deleted &&
                   (request.ClienteId == null || x.ClienteId == request.ClienteId) &&
                   (request.FechaDesde == null || x.Fecha.Date >= request.FechaDesde.Value.Date) &&
                   (request.FechaHasta == null || x.Fecha.Date <= request.FechaHasta.Value.Date) &&
                   (string.IsNullOrEmpty(request.Search) || x.Etiqueta.Contains(request.Search, StringComparison.OrdinalIgnoreCase)) &&
                   (request.UncheckedIds == null || request.UncheckedIds.Length == 0 || !request.UncheckedIds.Contains(x.Id)))
                .ToListAsync();

            await GeneraLiquidaciones(envios, request.Descripcion);
        }

        private async Task GeneraLiquidaciones(List<DetalleLiquidacionPos> envios, string descripcion)
        {
            var detalleGroupedByClient = envios.GroupBy(x => x.ClienteId)
                .Select(x => new 
                { 
                    Id = x.Key,
                    ValorSinImpuestos = x.Sum(x => x.ValorSinImpuesto),
                    Detalle = x 
                });

            var conceptoGenerico = await MyContext.Conceptos.FirstOrDefaultAsync(x => x.IsGeneric);

            foreach (var detalleGroup in detalleGroupedByClient)
            {
                var cliente = await MyContext.Clientes
                    .Include(x => x.Conceptos)
                        .ThenInclude(x => x.Concepto)
                    .Include(MyContext.GetIncludePaths(typeof(Cliente)))
                    .SingleOrDefaultAsync(x => x.Id == detalleGroup.Id);

                var liquidacion = new Liquidacion();

                var conceptosPendientesDeAutorizacion = cliente?.Conceptos.Where(x => x.Estado == Enums.EstadoConceptoCliente.PendienteDeAutorizacion);

                if (cliente != null)
                {
                    liquidacion.IdCliente = cliente.Id;
                    liquidacion.Estado = Enums.EstadoLiquidacion.PendienteDeAutorizacion;
                    liquidacion.Descripcion = descripcion ?? "Generada x sistema desde Envios";
                    liquidacion.Enabled = true;
                    liquidacion.CreateDate = DateTime.Now;
                    liquidacion.CreatedBy = CurrentUser?.user_id == null ? "Admin" : CurrentUser.user_id.ToString();
                    liquidacion.Conceptos = conceptosPendientesDeAutorizacion
                        .Select(y => new ConceptoLiquidacion
                        {
                            ConceptoClienteId = y.Id,
                            ConceptoId = y.ConceptoId,
                            Monto = y.Monto,
                            Observacion = y.Observacion,
                            Enabled = true,
                            CreateDate = DateTime.Now,
                            Estado = EstadoConceptoCliente.Autorizado,
                            CreatedBy = CurrentUser?.user_id == null ? "Admin" : CurrentUser.user_id.ToString()
                        })
                        .ToList();

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

                    liquidacion.Saldo = detalleGroup.ValorSinImpuestos +
                        conceptosPendientesDeAutorizacion.Where(x => !x.Concepto.Descuento).Sum(x => x.Monto) -
                        conceptosPendientesDeAutorizacion.Where(x => x.Concepto.Descuento).Sum(x => x.Monto);

                    foreach (var concepto in conceptosPendientesDeAutorizacion)
                    {
                        concepto.Estado = Enums.EstadoConceptoCliente.Autorizado;
                    }
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

                    liquidacion.DetalleLiquidacionPos.Add(detalle);
                }

                await MyContext.Liquidaciones.AddAsync(liquidacion);
                await MyContext.SaveChangesAsync();
            }
        }

        public async Task GeneraLiquidacionesByDeliveries(GeneraLiquidacionEnviosRequest request)
        {
            var envios = await MyContext.DetalleLiquidacionPos
                .Where(x => request.ids_envios.Contains(x.Id) &&
                    x.Estado == EstadoItem.PendienteLiquidar)
                .ToListAsync();

            await GeneraLiquidaciones(envios, request.descripcion);
        }

    }
}