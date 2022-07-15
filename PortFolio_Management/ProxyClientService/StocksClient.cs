using CommonServices.ModelServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PortFolio_Management.ProxyClientService
{
    public class StocksClient
    {
        private readonly HttpClient _httpClient;

        public StocksClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Stocks>> GetAllStocks()
        {
            string URL = _httpClient.BaseAddress + "GetAllStocks";
            List<Stocks> _resp = new List<Stocks>();

            try
            {
                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("GET"), URL);

                var response = await _httpClient.SendAsync(HttpRequest).ConfigureAwait(false);
                var ResponseRead = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if ((int)response.StatusCode == 200)
                {
                    _resp = JsonConvert.DeserializeObject<List<Stocks>>(ResponseRead);
                }
                else
                {
                    return _resp;
                }

            }
            catch (Exception ex)
            {
                return _resp;
            }

            return _resp;
        }

        public async Task<bool> AddStocks(AddStocks userStocks)
        {
            string URL = _httpClient.BaseAddress + "AddStocks";
            bool _resp = true;
            try
            {
                var requestContent = System.Text.Json.JsonSerializer.Serialize(userStocks);
                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("POST"), URL);
                HttpRequest.Content = new StringContent(requestContent);
                HttpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await _httpClient.SendAsync(HttpRequest).ConfigureAwait(false);

                var ResponseRead = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if ((int)response.StatusCode == 200)
                {
                    return _resp;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return _resp;
            }
        }

        public async Task<bool> AddCustomerStocks(AddCustomerStocks stocks)
        {
            string URL = _httpClient.BaseAddress + "AddCustomerStocks";
            bool _resp = true;

            try
            {
                var requestContent = System.Text.Json.JsonSerializer.Serialize(stocks);
                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("POST"), URL);
                HttpRequest.Content = new StringContent(requestContent);
                HttpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await _httpClient.SendAsync(HttpRequest).ConfigureAwait(false);

                var ResponseRead = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if ((int)response.StatusCode == 200)
                {
                    return _resp;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return _resp;
            }
        }

        public async Task<bool> RemoveCustomerStocks(AddCustomerStocks stocks)
        {
            string URL = _httpClient.BaseAddress + "RemoveCustomerStocks";
            bool _resp = true;

            try
            {
                var requestContent = System.Text.Json.JsonSerializer.Serialize(stocks);
                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("POST"), URL);
                HttpRequest.Content = new StringContent(requestContent);
                HttpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await _httpClient.SendAsync(HttpRequest).ConfigureAwait(false);

                var ResponseRead = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if ((int)response.StatusCode == 200)
                {
                    return _resp;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return _resp;
            }
        }

    }
}
