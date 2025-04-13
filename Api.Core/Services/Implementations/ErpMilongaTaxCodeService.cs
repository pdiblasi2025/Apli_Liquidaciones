using E = Api.Core.Entities;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaTaxCodeService: IErpMilongaTaxCodeService
    {
        private readonly MyContext _dbContext;
        private readonly IErpMilongaService _erpMilongaService;

        public ErpMilongaTaxCodeService(MyContext dbContext, IErpMilongaService erpMilongaService)
        {
            _dbContext = dbContext;
            _erpMilongaService = erpMilongaService;
        }

        public async Task Sync()
        {
            var erpResponse = await _erpMilongaService.GetAllTaxCodeAsync();

            var codigosImpuesto = await _dbContext.Impuestos
                       .AsNoTracking()
                       .ToListAsync();

            var items = erpResponse.Where(x => !codigosImpuesto.Any(cp => cp.Codigo != x.TaxCode)).ToList();

            foreach (var item in items)
            {
                _dbContext.Add(new E.Impuesto
                {
                    Codigo = item.TaxCode,
                    Descripcion = item.TaxDescription
                });
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
