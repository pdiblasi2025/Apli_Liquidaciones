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
    public class LocalidadService : ILocalidadService
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;
        public LocalidadService(MyContext context)
        {
            _context = context;
            _mapper = new Mapper(BootStrapper.MapperConfiguration);
        }

        public async Task<PagedListResponse<Localidad>> GetAllAsync(FilterLocalidad filter)
        {
            var pageSize = filter.PageSize ?? 10;
            var currentPage = filter.CurrentPage ?? 1;

            try
            {
                var query = _context.Localidades
                    .AsNoTracking()
                    .Where(x => (!filter.ProvinciaId.HasValue || x.ProvinciaId == filter.ProvinciaId.Value) &&
                            (string.IsNullOrEmpty(filter.MultiColumnSearchText) || x.Nombre.Contains(filter.MultiColumnSearchText.TrimStart())));

                var localidades = await query.Skip(pageSize * (currentPage - 1))
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedListResponse<Localidad>
                {
                    Count = query.Count(),
                    Data = _mapper.Map<IList<Localidad>>(localidades)
                };
            }
            catch (Exception)
            {
                return new PagedListResponse<Localidad>
                {
                    Count = 0,
                    Data = new List<Localidad>()
                };
            }
        }
    }
}
