using Api.Core.Dtos.Filter.Oms;
using Api.Core.Dtos.Oms;
using Api.Core.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class OmsService : IOmsService
    {
        private readonly HttpClient _httpClient;

        private const string ClientEndpoint = "v1/external/client/";
        private const string ShippingEndpoint = "v1/external/shipping/";
        private const string OrderEndpoint = "v1/external/merchant_order/";

        public OmsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OmsClientDto>> GetAllClientsAsync()
        {
            var clients = new List<OmsClientDto>();
            var page = 1;
            bool keepRetrievingClients;

            do
            {
                var response = await _httpClient.GetFromJsonAsync<OmsPaginatedResponse<OmsClientDto>>(ClientEndpoint + $"?page={page}");
                clients.AddRange(response.results);
                page++;
                keepRetrievingClients = page <= response.pagination.pages;
            } while (keepRetrievingClients);

            return clients;
        }

        public async Task<List<OmsShippingDto>> GetAllShippingsAsync(OmsShippingRequestFilter requestFilter)
        {
            var shippings = new List<OmsShippingDto>();
            var page = 1;
            bool keepRetrievingShippings;

            do
            {
                var query = GetShippingQuery(requestFilter, page);
                var responseMessage = await _httpClient.GetAsync(QueryHelpers.AddQueryString(ShippingEndpoint, query));
                var content = await responseMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<OmsPaginatedResponse<OmsShippingDto>>(content);
                shippings.AddRange(response.results);
                page++;
                keepRetrievingShippings = page <= response.pagination.pages;
            } while (keepRetrievingShippings);

            return shippings;
        }

        private Dictionary<string, string> GetShippingQuery(OmsShippingRequestFilter requestFilter, int page)
        {
            var query = new Dictionary<string, string>();

            query.Add("page", page.ToString());

            if (!string.IsNullOrEmpty(requestFilter.Shipment))
                query.Add("filter[shipment]", requestFilter.Shipment);

            if (requestFilter.StateId.HasValue)
                query.Add("filter[state_id]", requestFilter.StateId.Value.ToString());

            if (requestFilter.ClientId.HasValue)
                query.Add("filter[client_id]", requestFilter.ClientId.Value.ToString());

            if (requestFilter.StateDateFrom.HasValue)
                query.Add("filter[state_from_date]", requestFilter.StateDateFrom.Value.ToString("dd/MM/yyyy"));

            if (requestFilter.StateDateTo.HasValue)
                query.Add("filter[state_to_date]", requestFilter.StateDateTo.Value.ToString("dd/MM/yyyy"));

            if (requestFilter.DeliveryMode.HasValue)
                query.Add("filter[delivery_mode]", requestFilter.DeliveryMode.Value.ToString());

            return query;
        }

        public async Task<List<OmsOrderDto>> GetAllOrdersAsync(OmsOrderRequestFilter requestFilter)
        {
            var orders = new List<OmsOrderDto>();
            var page = 1;
            bool keepRetrievingOrders;

            do
            {
                var query = GetOrderQuery(requestFilter, page);
                var responseMessage = await _httpClient.GetAsync(QueryHelpers.AddQueryString(OrderEndpoint, query));
                var content = await responseMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<OmsPaginatedResponse<OmsOrderDto>>(content);
                orders.AddRange(response.results);
                page++;
                keepRetrievingOrders = page <= response.pagination.pages;
            } while (keepRetrievingOrders);

            return orders;
        }

        private Dictionary<string, string> GetOrderQuery(OmsOrderRequestFilter requestFilter, int page)
        {
            var query = new Dictionary<string, string>();

            query.Add("page", page.ToString());

            if (requestFilter.StateId.HasValue)
                query.Add("filter[state_id]", requestFilter.StateId.Value.ToString());

            if (requestFilter.ClientId.HasValue)
                query.Add("filter[client_id]", requestFilter.ClientId.Value.ToString());

            if (requestFilter.DateFrom.HasValue)
                query.Add("filter[from_date]", requestFilter.DateFrom.Value.ToString("dd/MM/yyyy"));

            if (requestFilter.DateTo.HasValue)
                query.Add("filter[to_date]", requestFilter.DateTo.Value.ToString("dd/MM/yyyy"));

            if (requestFilter.DeliveryMode.HasValue)
                query.Add("filter[delivery_mode]", requestFilter.DeliveryMode.Value.ToString());

            return query;
        }
    }
}
