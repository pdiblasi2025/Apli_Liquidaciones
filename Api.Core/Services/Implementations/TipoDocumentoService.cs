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
    public class TipoDocumentoService: ITipoDocumentoService
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;
        public TipoDocumentoService(MyContext context)
        {
            _context = context;
            _mapper = new Mapper(BootStrapper.MapperConfiguration);
        }
        public async Task<PagedListResponse<TipoDocumento>> GetAllAsync(FilterTipoDocumento filter)
        {
            var pageSize = filter.PageSize ?? 10;
            var currentPage = filter.CurrentPage ?? 1;

            try
            {
                var query = _context.ErpMilongaIdentificationTypeDbSet
                    .AsNoTracking()
                    .Where(x => string.IsNullOrEmpty(filter.MultiColumnSearchText) || x.Descripcion.Contains(filter.MultiColumnSearchText.TrimStart()));

                var tipoDocs = await query.Skip(pageSize * (currentPage - 1))
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedListResponse<TipoDocumento>
                {
                    Count = query.Count(),
                    Data = _mapper.Map<IList<TipoDocumento>>(tipoDocs)
                };
            }
            catch (Exception ex)
            {
                return new PagedListResponse<TipoDocumento>
                {
                    Count = 0,
                    Data = new List<TipoDocumento>()
                };
            }
        }
    }
}
