using E = Api.Core.Entities;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaPaymentMethodService : IErpMilongaPaymentMethodService
    {
        private readonly MyContext _dbContext;
        private readonly IErpMilongaService _erpMilongaService;

        public ErpMilongaPaymentMethodService(MyContext dbContext, IErpMilongaService erpMilongaService)
        {
            _dbContext = dbContext;
            _erpMilongaService = erpMilongaService;
        }

        public async Task Sync()
        {
            var erpResponse = await _erpMilongaService.GetAllPaymentMethodAsync();

            var condicionesPago = await _dbContext.CondicionesPago
                       .AsNoTracking()
                       .ToListAsync();

            var items = erpResponse.Where(x => !condicionesPago.Any(cp => cp.ErpId == x.PaymentMethodID)).ToList();

            foreach (var item in items)
            {
                _dbContext.Add(new E.CondicionPago
                {
                    ErpId = item.PaymentMethodID,
                    Nombre = item.PaymentMethodDescription,
                    TerminoPago = item.PaymentTerm
                });
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
