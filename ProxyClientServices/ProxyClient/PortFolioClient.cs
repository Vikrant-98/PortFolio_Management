using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProxyClientServices.ProxyClient
{
    public class PortFolioClient
    {
        private readonly HttpClient _httpClient;

        public PortFolioClient(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<(string, bool)> GenerateTokan()
        {
            string token = string.Empty;
            string URL = "";

            try
            {

                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("GET"), URL);

                var response = await _httpClient.SendAsync(HttpRequest).ConfigureAwait(false);
                var ResponseRead = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if ((int)response.StatusCode == 200)
                {
                    var _resp = JsonConvert.DeserializeObject<TokanResponse>(ResponseRead);
                    token = _resp.access_token;

                }
                else
                {
                    _logger.LogWarning("Non Success from Get Token API ....");
                    return (token, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GenerateTokan ...." + ex.Message);
                return (token, false);
            }

            return (token, true);
        }
    }
}
