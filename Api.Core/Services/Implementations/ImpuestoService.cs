using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ImpuestoService : IImpuestoService
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;
        public ImpuestoService(MyContext context)
        {
            _context = context;
            _mapper = new Mapper(BootStrapper.MapperConfiguration);
        }

        public async Task<PagedListResponse<Impuesto>> GetAllAsync(FilterImpuesto filter)
        {
            var pageSize = filter.PageSize ?? 10;
            var currentPage = filter.CurrentPage ?? 1;

            try
            {
                var query = _context.Impuestos
                    .AsNoTracking()
                    .Where(x => string.IsNullOrEmpty(filter.MultiColumnSearchText) || x.Descripcion.Contains(filter.MultiColumnSearchText.TrimStart()));

                var impuestos = await query.Skip(pageSize * (currentPage - 1))
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedListResponse<Impuesto>
                {
                    Count = query.Count(),
                    Data = _mapper.Map<IList<Impuesto>>(impuestos)
                };
            }
            catch (Exception)
            {
                return new PagedListResponse<Impuesto>
                {
                    Count = 0,
                    Data = new List<Impuesto>()
                };
            }
        }
    }
}
