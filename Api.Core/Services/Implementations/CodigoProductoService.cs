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
    public class CodigoProductoService : ICodigoProductoService
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;
        public CodigoProductoService(MyContext context)
        {
            _context = context;
            _mapper = new Mapper(BootStrapper.MapperConfiguration);
        }
        public async Task<PagedListResponse<CodigoProducto>> GetAllAsync(FilterCodigoProducto filter)
        {
            var pageSize = filter.PageSize ?? 10;
            var currentPage = filter.CurrentPage ?? 1;

            try
            {
                var query = _context.ErpMilongaProductCodes
                    .AsNoTracking()
                    .Where(x => string.IsNullOrEmpty(filter.MultiColumnSearchText) || x.ProductName.Contains(filter.MultiColumnSearchText.TrimStart()));

                var codigoProductos = await query.Skip(pageSize * (currentPage - 1))
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedListResponse<CodigoProducto>
                {
                    Count = query.Count(),
                    Data = _mapper.Map<IList<CodigoProducto>>(codigoProductos)
                };
            }
            catch (Exception ex)
            {
                return new PagedListResponse<CodigoProducto>
                {
                    Count = 0,
                    Data = new List<CodigoProducto>()
                };
            }
        }
    }
}
