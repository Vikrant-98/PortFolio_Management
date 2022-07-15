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
    public class MutualFundClient
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public MutualFundClient(HttpClient httpClient, IUserService userService, IConfiguration config)
        {
            _httpClient = httpClient;
            _userService = userService;
            _config = config;
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
                var details = _userService.UserDetails(MutualFunds.CustomerId);
                var token = GenerateToken(details.Item1);
                var requestContent = System.Text.Json.JsonSerializer.Serialize(MutualFunds);
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

        public async Task<bool> RemoveCustomerMutualFunds(AddCustomerMutualFunds MutualFunds)
        {
            string URL = _httpClient.BaseAddress + "RemoveCustomerMutualFunds";
            bool _resp = true;

            try
            {
                var details = _userService.UserDetails(MutualFunds.CustomerId);
                var token = GenerateToken(details.Item1);
                var requestContent = System.Text.Json.JsonSerializer.Serialize(MutualFunds);
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
