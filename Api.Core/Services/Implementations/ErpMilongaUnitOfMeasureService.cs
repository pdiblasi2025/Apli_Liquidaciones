using E = Api.Core.Entities;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaUnitOfMeasureService: IErpMilongaUnitOfMeasureService
    {
        private readonly MyContext _dbContext;
        private readonly IErpMilongaService _erpMilongaService;

        public ErpMilongaUnitOfMeasureService(MyContext dbContext, IErpMilongaService erpMilongaService)
        {
            _dbContext = dbContext;
            _erpMilongaService = erpMilongaService;
        }

        public async Task Sync()
        {
            var erpResponse = await _erpMilongaService.GetAllUnitOfMeasureAsync();

            var unidadesDeMedida = await _dbContext.ErpMilongaUnitOfMeasureDbSet
                       .AsNoTracking()
                       .ToListAsync();

            var items = erpResponse.Where(x => !unidadesDeMedida.Any(cp => cp.UnitOfMeasureID == x.UnitOfMeasureID)).ToList();

            foreach (var item in items)
            {
                _dbContext.Add(new E.ErpMilongaUnitOfMeasure
                {
                    UnitOfMeasureID = item.UnitOfMeasureID,
                    Descripcion = item.UnitOfMeasureName
                });
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
