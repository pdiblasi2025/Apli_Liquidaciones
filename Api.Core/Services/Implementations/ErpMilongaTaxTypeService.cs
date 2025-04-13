using E = Api.Core.Entities;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaTaxTypeService : IErpMilongaTaxTypeService
    {
        private readonly MyContext _dbContext;
        private readonly IErpMilongaService _erpMilongaService;

        public ErpMilongaTaxTypeService(MyContext dbContext, IErpMilongaService erpMilongaService)
        {
            _dbContext = dbContext;
            _erpMilongaService = erpMilongaService;
        }

        public async Task Sync()
        {
            var erpResponse = await _erpMilongaService.GetAllTaxTypeAsync();

            var tipoImpuesto = await _dbContext.ErpMilongaTaxTypeDbSet
                       .AsNoTracking()
                       .ToListAsync();

            var items = erpResponse.Where(x => !tipoImpuesto.Any(cp => cp.TaxTypeID == x.TaxTypeID)).ToList();

            foreach (var item in items)
            {
                _dbContext.Add(new E.ErpMilongaTaxType
                {
                    TaxTypeID = item.TaxTypeID,
                    Descripcion = item.TaxTypeDescription
                });
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
