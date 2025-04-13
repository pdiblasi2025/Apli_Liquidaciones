using Api.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaMasterService : IErpMilongaMasterService
    {
        private readonly IErpMilongaPaymentMethodService _erpMilongaPaymentMethodService;
        private readonly IErpMilongaTaxTypeService _erpMilongaTaxTypeService;
        private readonly IErpMilongaIdentificationTypeService _erpMilongaIdentificationTypeService;
        private readonly IErpMilongaProductTypeService _erpMilongaProductTypeService;
        private readonly IErpMilongaTaxCodeService _erpMilongaTaxCodeService;
        private readonly IErpMilongaUnitOfMeasureService _erpMilongaUnitOfMeasureService;
        private readonly IErpMilongaProductCodeService _erpMilongaProductCodeService;

        public ErpMilongaMasterService(
            IErpMilongaPaymentMethodService erpMilongaPaymentMethodService,
            IErpMilongaTaxTypeService erpMilongaTaxTypeService,
            IErpMilongaIdentificationTypeService erpMilongaIdentificationTypeService,
            IErpMilongaProductTypeService erpMilongaProductTypeService,
            IErpMilongaTaxCodeService erpMilongaTaxCodeService,
            IErpMilongaUnitOfMeasureService erpMilongaUnitOfMeasureService,
            IErpMilongaProductCodeService erpMilongaProductCodeService)
        {
            _erpMilongaPaymentMethodService = erpMilongaPaymentMethodService;
            _erpMilongaTaxTypeService = erpMilongaTaxTypeService;
            _erpMilongaIdentificationTypeService = erpMilongaIdentificationTypeService;
            _erpMilongaProductTypeService = erpMilongaProductTypeService;
            _erpMilongaTaxCodeService = erpMilongaTaxCodeService;
            _erpMilongaUnitOfMeasureService = erpMilongaUnitOfMeasureService;
            _erpMilongaProductCodeService = erpMilongaProductCodeService;
        }

        public async Task SyncMasters()
        {
            await _erpMilongaPaymentMethodService.Sync();
            await _erpMilongaTaxTypeService.Sync();
            await _erpMilongaIdentificationTypeService.Sync();
            await _erpMilongaProductTypeService.Sync();
            await _erpMilongaTaxCodeService.Sync();
            await _erpMilongaUnitOfMeasureService.Sync();
            await _erpMilongaProductCodeService.Sync();
        }
    }
}
