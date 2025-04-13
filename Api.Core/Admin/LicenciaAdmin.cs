using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Core.Admin
{
    public class LicenciaAdmin : BaseAdmin<int, Entities.Licencia, Dtos.Licencia, FilterLicencia>
    {
        public override IQueryable GetQuery(FilterLicencia filter)
        {
            var query = MyContext.Licencias.AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                query = query.Where(e => e.Codigo.StartsWith(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();
            }

            return query;
        }

        public override Entities.Licencia ToEntity(Dtos.Licencia dto)
        {
            var entity = new Entities.Licencia();            

            if (base.HasValue(dto.Id))
            {
                entity = MyContext.Licencias.Single(e => e.Id == dto.Id);
            }

            entity.Descripcion = dto.Descripcion;
            entity.Codigo = dto.Codigo;
            entity.CompaniaId = dto.CompaniaId;
            entity.Estado = 1;
            entity.Expiracion = DateTime.Now.AddMonths(dto.Plan.MesesValidez);

            //foreach (PropertyInfo propEntity in entity.GetType().GetProperties())
            //{
            //    if (dto.GetType().GetProperties().Where(x => base.IsSimple(x.PropertyType) && x.Name.Equals(propEntity.Name)).FirstOrDefault() is PropertyInfo entDto && entDto != null)
            //    {
            //        propEntity.SetValue(entity, entDto.GetValue(dto));
            //    }
            //}

            return entity;
        }

        public override Dtos.Response.ResultMessage<Dtos.Licencia> Validate(Dtos.Licencia dto, int Type)
        {
            // Type: 1-Create, 2-Update, 3-Delete 
            return new Dtos.Response.ResultMessage<Dtos.Licencia>() { Content = dto, Successful = true, Message = "OK" };
        }

        public PagedListResponse<Dtos.Licencia> GetByFilterBasic(FilterBase filter)
        {
            IQueryable<Entities.Licencia> query;

            query = MyContext.Licencias.Include(new List<string>() { "Plan" })
                .Where(e => !e.Deleted)
                .OrderByDescending(e => e.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                query = query.Where(e =>
                        e.Codigo.StartsWith(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase))
                    .AsQueryable();
            }

            var pageSize = filter.PageSize ?? 10;
            var currentPage = filter.CurrentPage ?? 1;

            var data = query.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();

            foreach (var c in data)
            {
                c.CompaniaNombre = MyContext.Companias.FirstOrDefault(x => x.Id == c.CompaniaId).Nombre;
            }

            return new PagedListResponse<Dtos.Licencia>
            {
                Count = query.Count(),
                Data = Mapper.Map<IList<Entities.Licencia>, IList<Dtos.Licencia>>(data)
            };
        }

        public void UpdateExpirationDate(DateTime expirationDate, int id)
        {
            var entity = new Entities.Licencia();

            if (base.HasValue(id))
            {
                entity = MyContext.Licencias.Single(e => e.Id == id);
                entity.Expiracion = expirationDate;
                MyContext.SaveChanges();
            }
        }

        public Dtos.Licencia GetByCompany(string idAuth)
        {
            IQueryable<Entities.Licencia> query;

            query = MyContext.Licencias.Include(new List<string>() { "Plan" })
                .Where(e => !e.Deleted && e.Compania.AuthId == idAuth)
                .AsQueryable();

            var licencia = query.Single();

            return Mapper.Map<Entities.Licencia, Dtos.Licencia>(licencia);
        }
    }
}
