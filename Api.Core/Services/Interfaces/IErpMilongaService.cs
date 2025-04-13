using Api.Core.Dtos.ErpMilonga;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Services.Interfaces
{
    public interface IErpMilongaService
    {
        void AddAuthorization(string token);

        Task Login();
        Task<List<ErpMasterPaymentMethodDto>> GetAllPaymentMethodAsync();
        Task<List<ErpMasterTaxTypeDto>> GetAllTaxTypeAsync();
        Task<List<ErpMasterIdentificationTypeDto>> GetAllIdentificationTypeAsync();
        Task<List<ErpMasterTaxCodeDto>> GetAllTaxCodeAsync();
        Task<List<ErpMasterProductTypeDto>> GetAllProductTypeAsync();
        Task<List<ErpMasterUnitOfMeasureDto>> GetAllUnitOfMeasureAsync();
        Task<List<ErpMasterProductCodeDto>> GetAllProductCodeAsync();
        Task<ErpInvoiceResponseDto> PostOrderAsync(InvoiceOrderDto order);
    }

}
