
using Api.Core.Dtos.ErpMilonga;
using Api.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Api.Core.Services.Implementations
{
    public class ErpMilongaService: IErpMilongaService
    {
        private readonly HttpClient _httpClient;
        private const string AUTHORIZATION = "authorization";
        private const string BEARER = "Bearer ";
        private const string LoginEndpoint = "Api/Login";
        private const string PaymentMethodEndpoint = "Api/Values/ECL/PaymentMethodID";
        private const string TaxTypeEndpoint = "Api/Values/ECL/TaxTypeID";
        private const string IdentificationTypeEndpoint = "Api/Values/ECL/IdentificationTypeID";
        private const string TaxCodeEndpoint = "Api/Values/ECL/TaxCode";
        private const string ProductTypeIDEndpoint = "Api/Values/ECL/ProductTypeID";
        private const string UnitOfMeasureEndpoint = "Api/Values/ECL/UnitOfMeasureID";
        private const string InvoiceEndpoint = "Api/Invoice";
        private const string ProductCodeEndpoint = "Api/Values/ECL/ProductCode";
        private readonly IConfiguration _configuration;

        public ErpMilongaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public void AddAuthorization(string token)
        {
            _httpClient.DefaultRequestHeaders.Add(AUTHORIZATION, $"{BEARER}{token}");
        }

        public virtual async Task Login()
        {
            var login = new ErpMilongaUserLoginDto
            {
                UserName = _configuration["Erp:UserName"],
                Password = _configuration["Erp:Password"]
            };

            var response = await _httpClient.PostAsJsonAsync(LoginEndpoint, login);

            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsAsync<ErpMilongaLoginResponseDto>().Result;
                if (!_httpClient.DefaultRequestHeaders.Contains(AUTHORIZATION))
                    AddAuthorization(result.Token);
            }
        }

        public async Task<List<ErpMasterPaymentMethodDto>> GetAllPaymentMethodAsync()
        {
            await Login();
            var response = await _httpClient.GetFromJsonAsync<List<ErpMasterPaymentMethodDto>>(PaymentMethodEndpoint);

                return response;           
        }

        public async Task<List<ErpMasterTaxTypeDto>> GetAllTaxTypeAsync()
        {
            
            await Login();
            var response = await _httpClient.GetFromJsonAsync<List<ErpMasterTaxTypeDto>>(TaxTypeEndpoint);

            return response;
        }

        public async Task<List<ErpMasterIdentificationTypeDto>> GetAllIdentificationTypeAsync()
        {

            await Login();
            var response = await _httpClient.GetFromJsonAsync<List<ErpMasterIdentificationTypeDto>>(IdentificationTypeEndpoint);

            return response;
        }

        public async Task<List<ErpMasterTaxCodeDto>> GetAllTaxCodeAsync()
        {

            await Login();
            var response = await _httpClient.GetFromJsonAsync<List<ErpMasterTaxCodeDto>>(TaxCodeEndpoint);

            return response;
        }

        public async Task<List<ErpMasterProductTypeDto>> GetAllProductTypeAsync()
        {

            await Login();
            var response = await _httpClient.GetFromJsonAsync<List<ErpMasterProductTypeDto>>(ProductTypeIDEndpoint);

            return response;
        }

        public async Task<List<ErpMasterUnitOfMeasureDto>> GetAllUnitOfMeasureAsync()
        {

            await Login();
            var response = await _httpClient.GetFromJsonAsync<List<ErpMasterUnitOfMeasureDto>>(UnitOfMeasureEndpoint);

            return response;
        }

        public async Task<List<ErpMasterProductCodeDto>> GetAllProductCodeAsync()
        {
            await Login();

            var response = await _httpClient.GetFromJsonAsync<List<ErpMasterProductCodeDto>>(ProductCodeEndpoint);

            return response;
        }

        public async Task<ErpInvoiceResponseDto> PostOrderAsync(InvoiceOrderDto order)
        {
            await Login();

            var response = await _httpClient.PostAsJsonAsync(InvoiceEndpoint, order);

            var res = await response.Content.ReadAsStringAsync();


            ErpInvoiceResponseDto erpInvoiceResponse =
            JsonConvert.DeserializeObject<ErpInvoiceResponseDto>(res);
            erpInvoiceResponse.LiquidacionId = order.OrderCode;
            return erpInvoiceResponse;
        }
    }
}
