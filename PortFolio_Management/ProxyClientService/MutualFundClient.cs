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
    public class MutualFundClient
    {
        private readonly HttpClient _httpClient;

        public MutualFundClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Mutual>> GetAllMutualFunds()
        {
            string URL = _httpClient.BaseAddress + "GetAllMutualFunds";
            List<Mutual> _resp = new List<Mutual>();

            try
            {
                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("GET"), URL);

                var response = await _httpClient.SendAsync(HttpRequest).ConfigureAwait(false);
                var ResponseRead = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if ((int)response.StatusCode == 200)
                {
                    _resp = JsonConvert.DeserializeObject<List<Mutual>>(ResponseRead);
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

        public async Task<bool> AddMutualFunds(AddMutualFunds userMutualFunds)
        {
            string URL = _httpClient.BaseAddress + "AddMutualFunds";
            bool _resp = true;
            try
            {
                var requestContent = System.Text.Json.JsonSerializer.Serialize(userMutualFunds);
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

        public async Task<bool> AddCustomerMutualFunds(AddCustomerMutualFunds MutualFunds)
        {
            string URL = _httpClient.BaseAddress + "AddCustomerMutualFunds";
            bool _resp = true;

            try
            {
                var requestContent = System.Text.Json.JsonSerializer.Serialize(MutualFunds);
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

        public async Task<bool> RemoveCustomerMutualFunds(AddCustomerMutualFunds MutualFunds)
        {
            string URL = _httpClient.BaseAddress + "RemoveCustomerMutualFunds";
            bool _resp = true;

            try
            {
                var requestContent = System.Text.Json.JsonSerializer.Serialize(MutualFunds);
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
