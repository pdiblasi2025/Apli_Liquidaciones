using Api.Core.Entities;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaProductCodeService : IErpMilongaProductCodeService
    {
        private readonly MyContext _dbContext;
        private readonly IErpMilongaService _erpMilongaService;

        public ErpMilongaProductCodeService(MyContext dbContext, IErpMilongaService erpMilongaService)
        {
            _dbContext = dbContext;
            _erpMilongaService = erpMilongaService;
        }

        public async Task Sync()
        {
            var erpResponse = await _erpMilongaService.GetAllProductCodeAsync();

            var codigosProducto = await _dbContext.ErpMilongaProductCodes
                       .AsNoTracking()
                       .ToListAsync();

            var items = erpResponse.Where(x => !codigosProducto
                    .Any(cp => string.Compare(cp.ProductCode, x.ProductCode, StringComparison.OrdinalIgnoreCase) == 0)).ToList();

            foreach (var item in items)
            {
                _dbContext.Add(new ErpMilongaProductCode
                {
                    ProductTypeCode = item.ProductTypeCode,
                    ProductCode = item.ProductCode,
                    ProductName = item.ProductName
                });
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
