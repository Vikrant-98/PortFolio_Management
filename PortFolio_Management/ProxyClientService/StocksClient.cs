using CommonServices.ModelServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PortFolio_Management.ProxyClientService
{
    public class StocksClient
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public StocksClient(HttpClient httpClient, IConfiguration config, IUserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
            _config = config;
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
                var details = _userService.UserDetails(stocks.CustomerId);
                var token = GenerateToken(details.Item1);
                var requestContent = System.Text.Json.JsonSerializer.Serialize(stocks);
                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("POST"), URL);
                HttpRequest.Content = new StringContent(requestContent);
                HttpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                HttpRequest.Headers.Add("Authorization", "Bearer " + token);
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
                var details = _userService.UserDetails(stocks.CustomerId);
                var token = GenerateToken(details.Item1);
                var requestContent = System.Text.Json.JsonSerializer.Serialize(stocks);
                HttpRequestMessage HttpRequest = new HttpRequestMessage(new HttpMethod("POST"), URL);
                HttpRequest.Content = new StringContent(requestContent);
                HttpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                HttpRequest.Headers.Add("Authorization", "Bearer " + token);
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

        private string GenerateToken(Customer Info)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, Info.Role),
                    new Claim("EmailID", Info.MailID.ToString()),
                };
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
