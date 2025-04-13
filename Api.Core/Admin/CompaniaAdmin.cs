using Api.Core.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Api.Core.Dtos.Filter;

namespace Api.Core.Admin
{
    public class CompaniaAdmin : BaseAdmin<int, Entities.Compania, Dtos.Compania, FilterCompania>
    {
        public override IQueryable GetQuery(FilterCompania filter)
        {
            var query = MyContext.Companias.Include(MyContext.GetIncludePaths(typeof(Entities.Compania))).AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                query = query.Where(e => e.Nombre.StartsWith(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();
            }

            return query;
        }

        public override Entities.Compania ToEntity(Dtos.Compania dto)
        {
            var entity = new Entities.Compania();

            if (base.HasValue(dto.Id))
            {
                entity = MyContext.Companias.Single(e => e.Id == dto.Id);

                foreach (PropertyInfo propEntity in entity.GetType().GetProperties())
                {
                    if (dto.GetType().GetProperties().Where(x => base.IsSimple(x.PropertyType) && x.Name.Equals(propEntity.Name)).FirstOrDefault() is PropertyInfo entDto && entDto != null)
                    {
                        propEntity.SetValue(entity, entDto.GetValue(dto));
                    }
                }
            }

            entity.Nombre = dto.Nombre;
            entity.Descripcion = dto.Descripcion;

            if (dto.LicenciaActiva != null)
            {
                var licenciaActiva = MyContext.Licencias.Where(e => e.Codigo == dto.LicenciaActiva.Codigo).FirstOrDefault();

                if (licenciaActiva != null)
                {
                    entity.LicenciaActiva = licenciaActiva;
                    entity.LicenciaActiva.Expiracion = dto.LicenciaActiva.Expiracion;
                }
            }

            return entity;
        }

        public override Dtos.Response.ResultMessage<Dtos.Compania> Validate(Dtos.Compania dto, int Type)
        {
            // Type: 1-Create, 2-Update, 3-Delete 
            return new Dtos.Response.ResultMessage<Dtos.Compania>() { Content = dto, Successful = true, Message = "OK" };
        }

        public PagedListResponse<Dtos.Compania> GetByFilterBasic(FilterCompania filter)
        {
            IQueryable<Entities.Compania> query;

            query = MyContext.Companias.Include(MyContext.GetIncludePaths(typeof(Entities.Compania)))
                .Where(e => !e.Deleted)
                .OrderByDescending(e => e.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                query = query.Where(e =>
                        e.Nombre.StartsWith(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase))
                    .AsQueryable();
            }

            var pageSize = filter.PageSize ?? 10;
            var currentPage = filter.CurrentPage ?? 1;

            var data = query.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();

            foreach (var c in data)
            {
                var licencia = c.Licencias.OrderByDescending(x => x.CreateDate).FirstOrDefault(x => !x.Deleted && x.Estado == 1);
                var licenciaId = licencia == null ? 0 : licencia.Id;
                c.LicenciaActiva = MyContext.Licencias.FirstOrDefault(x => x.Id == licenciaId);
            }

            return new PagedListResponse<Dtos.Compania>
            {
                Count = query.Count(),
                Data = Mapper.Map<IList<Entities.Compania>, IList<Dtos.Compania>>(data)
            };
        }

        public virtual Dtos.Compania RegisterCompany(Dtos.Compania dto)
        {
            Validate(dto,1);
            var entity = ToEntity(dto);

            entity.Enabled = false;
            entity.CreateDate = DateTime.Now;
            entity.CreatedBy = CurrentUser.user_id;

            entity.Licencias = new List<Entities.Licencia>() { new Entities.Licencia() { Codigo = "TRIAL", CreateDate = DateTime.Now, Enabled = false, CreatedBy = CurrentUser.user_id, Estado =  0} };

            MyContext.Set<Entities.Compania>().Add(entity);
            MyContext.SaveChanges();

            return Mapper.Map<Entities.Compania, Dtos.Compania>(entity);
        }
    }
}
