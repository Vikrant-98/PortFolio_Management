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
    public class PortFolioClient
    {
        private readonly HttpClient _httpClient;

        public PortFolioClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PortfolioDetails> GetAllPortFolioDetails(int CustomerId)
        {
            string URL = _httpClient.BaseAddress + "GetAllPortFolioDetails?CustomerId="+ CustomerId;
            PortfolioDetails _resp = new PortfolioDetails();

            try
            {
                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("GET"), URL);

                var response = await _httpClient.SendAsync(HttpRequest).ConfigureAwait(false);
                var ResponseRead = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if ((int)response.StatusCode == 200)
                {
                    _resp = JsonConvert.DeserializeObject<PortfolioDetails>(ResponseRead);
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
    }
}
