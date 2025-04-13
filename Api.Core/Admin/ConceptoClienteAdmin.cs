using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Entities;
using Api.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Admin
{
    public class ConceptoClienteAdmin : BaseAdmin<int, Entities.ConceptoCliente, Dtos.ConceptoCliente, FilterConceptoCliente>
    {
        public override IQueryable GetQuery(FilterConceptoCliente filter)
        {
            var query = MyContext.ConceptosClientes
                .Include(MyContext.GetIncludePaths(typeof(Entities.ConceptoCliente)))
                .OrderByDescending(x => x.UpdateDate)
                .AsNoTracking();

            //query = query.Where(e => (string.IsNullOrEmpty(filter.MultiColumnSearchText) || (e.Cliente.RazonSocial.Contains(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase) ||
            //                                                                                (e.Cliente.Nombre + ' ' + e.Cliente.Apellido).Contains(filter.MultiColumnSearchText, StringComparison.OrdinalIgnoreCase) ||
            //                                                                                (e.Cliente.Apellido + ' ' + e.Cliente.Nombre).Contains(filter.MultiColumnSearchText, StringComparison.OrdinalIgnoreCase))) &&
            //                         (filter.FechaDesde == null || e.UpdateDate >= filter.FechaDesde.Value.Date) &&
            //                         (filter.FechaHasta == null || e.UpdateDate <= filter.FechaHasta.Value.Date));
            //PSD
            query = query.Where(e => (string.IsNullOrEmpty(filter.MultiColumnSearchText) ||
                                     e.Cliente.RazonSocial.Contains(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase) ||
                                     e.Cliente.Nombre.Contains(filter.MultiColumnSearchText, StringComparison.OrdinalIgnoreCase)) &&
                                     (filter.FechaDesde == null || e.UpdateDate >= filter.FechaDesde.Value.Date) &&
                                     (filter.FechaHasta == null || e.UpdateDate <= filter.FechaHasta.Value.Date));


            return query;          
        }

        public override void Delete(int id)
        {
            var conceptoCliente = MyContext.ConceptosClientes.FirstOrDefault(x => x.Id == id);

            if (conceptoCliente == null || conceptoCliente.Estado != EstadoConceptoCliente.PendienteDeAutorizacion)
            {
                var result = new Result
                {
                    HasErrors = true
                };

                result.Messages.Add($"No existe el concepto x cliente con Id {id} o no se encuentra en estado pendiente de autorizacion");

                throw new BadRequestException(result);
            }

            conceptoCliente.Estado = EstadoConceptoCliente.Anulado;
            conceptoCliente.UpdateDate = DateTime.Now;
            conceptoCliente.UpdatedBy = CurrentUser?.user_id;

            MyContext.SaveChanges();
        }

        public override ConceptoCliente ToEntity(Dtos.ConceptoCliente dto)
        {
            var entity = new Entities.ConceptoCliente();

            if (dto.Id.HasValue)
            {
                entity = MyContext.ConceptosClientes.Single(e => e.Id == dto.Id.Value);
            }

            if (entity.Monto < 0)
            {
                var result = new Result
                {
                    HasErrors = true
                };

                result.Messages.Add("El monto del concepto x cliente debe ser positivo");

                throw new BadRequestException(result);
            }

            entity.ConceptoId = dto.ConceptoId;
            entity.ClienteId = dto.ClienteId;
            entity.Monto = dto.Monto;
            entity.Observacion = dto.Observacion;
            entity.Estado = EstadoConceptoCliente.PendienteDeAutorizacion;


            return entity;
        }

        public override Dtos.Response.ResultMessage<Dtos.ConceptoCliente> Validate(Dtos.ConceptoCliente dto, int Type)
        {
            // Type: 1-Create, 2-Update, 3-Delete 
            return new Dtos.Response.ResultMessage<Dtos.ConceptoCliente>() { Content = dto, Successful = true, Message = "OK" };
        }
    }
}