using E = Api.Core.Entities;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaIdentificationTypeService : IErpMilongaIdentificationTypeService
    {
        private readonly MyContext _dbContext;
        private readonly IErpMilongaService _erpMilongaService;

        public ErpMilongaIdentificationTypeService(MyContext dbContext, IErpMilongaService erpMilongaService)
        {
            _dbContext = dbContext;
            _erpMilongaService = erpMilongaService;
        }

        public async Task Sync()
        {
            var erpResponse = await _erpMilongaService.GetAllIdentificationTypeAsync();

            var tiposDeIdentificacion = await _dbContext.ErpMilongaIdentificationTypeDbSet
                       .AsNoTracking()
                       .ToListAsync();

            var items = erpResponse.Where(x => !tiposDeIdentificacion.Any(cp => cp.IdentificationTypeID == x.IdentificationTypeID)).ToList();

            foreach (var item in items)
            {
                _dbContext.Add(new E.ErpMilongaIdentificationType
                {
                    IdentificationTypeID = item.IdentificationTypeID,
                    Descripcion = item.IdentificationTypeName
                });
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
