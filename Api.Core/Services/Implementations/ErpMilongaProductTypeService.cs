using Api.Core.Entities;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaProductTypeService : IErpMilongaProductTypeService
    {
        private readonly MyContext _dbContext;
        private readonly IErpMilongaService _erpMilongaService;

        public ErpMilongaProductTypeService(MyContext dbContext, IErpMilongaService erpMilongaService)
        {
            _dbContext = dbContext;
            _erpMilongaService = erpMilongaService;
        }

        public async Task Sync()
        {
            var erpResponse = await _erpMilongaService.GetAllProductTypeAsync();

            var tiposProducto = await _dbContext.ErpMilongaProductTypeDbSet
                       .AsNoTracking()
                       .ToListAsync();

            var items = erpResponse.Where(x => !tiposProducto.Any(cp => cp.ProductTypeID == x.ProductTypeID)).ToList();

            foreach (var item in items)
            {
                _dbContext.Add(new ErpMilongaProductType
                {
                    ProductTypeID = item.ProductTypeID,
                    Descripcion = item.ProductTypeName
                });
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
