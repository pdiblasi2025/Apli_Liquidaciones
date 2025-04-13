using Api.Core.Dtos.Filter;
using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Admin
{
    public class ConceptoAdmin : BaseAdmin<int, Entities.Concepto, Dtos.Concepto, FilterConcepto>
    {
        public override IQueryable GetQuery(FilterConcepto filter)
        {           
            var query = MyContext.Conceptos
                .Include(MyContext.GetIncludePaths(typeof(Entities.Concepto)))
                .Where(x => !x.IsGeneric)
                .OrderByDescending(x => x.UpdateDate ?? x.CreateDate).ThenBy(x => x.Nombre)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                query = query.Where(e => e.Nombre.StartsWith(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase));
            }

            return query;
        }

        public override Concepto ToEntity(Dtos.Concepto dto)
        {
            var entity = new Entities.Concepto();

            if (dto.Id.HasValue)
            {
                entity = MyContext.Conceptos.Single(e => e.Id == dto.Id.Value);
            }

            entity.Codigo = dto.Codigo;
            entity.Nombre = dto.Nombre;
            entity.Descuento = dto.Descuento;
            entity.CodigoProductoId = dto.CodigoProducto.Id;

            return entity;
        }

        public async Task<IEnumerable<Dtos.Concepto>> GetConceptosSinAsociarAsync()
        {
            var conceptosSinAsociar = await MyContext.Conceptos
                .Where(x => x.Clientes.Count == 0 && x.Liquidaciones.Count == 0)
                .ToListAsync();

            return Mapper.Map<IEnumerable<Dtos.Concepto>>(conceptosSinAsociar);
        }

        public override Dtos.Response.ResultMessage<Dtos.Concepto> Validate(Dtos.Concepto dto, int Type)
        {
            // Type: 1-Create, 2-Update, 3-Delete 
            var entity = new Entities.Concepto();
            Dtos.Response.ResultMessage<Dtos.Concepto> respuesta_resultado = new Dtos.Response.ResultMessage<Dtos.Concepto>() { Content = dto, Successful = true, Message = "OK" };
            switch (Type)   
            {
                case 1:
                    if (dto.Codigo != null)
                    {
                        entity = MyContext.Conceptos.FirstOrDefault(e => !e.Deleted && e.Codigo == dto.Codigo);

                        if (entity != null)
                        {
                            respuesta_resultado.Successful = false;
                            respuesta_resultado.Message = "El código ingresado ya existe para otro concepto";
                        }
                    }
                    break;
                case 2:
                    entity = MyContext.Conceptos.FirstOrDefault(x => x.Id == dto.Id);

                    if (string.Compare(dto.Codigo, entity.Codigo, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        respuesta_resultado.Successful = false;
                        respuesta_resultado.Message = "No se puede modificar el código del concepto existente";
                    }
                    break;
                case 3:
                    entity = MyContext.Conceptos.Include(x => x.Liquidaciones).FirstOrDefault(x => x.Id == dto.Id);

                    if(entity.Liquidaciones.Any())
                    {
                        respuesta_resultado.Successful = false;
                        respuesta_resultado.Message = "No se puede eliminar el concepto ya que está asociado a una o más liquidaciones";
                    }
                    break;

                default:
                    break;
            }


            return respuesta_resultado;
        }

    }
}