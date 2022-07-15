using Business.Interface;
using CommonServices.ModelServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StocksAPI.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockBusiness _stockBusiness;

        public StockController(IStockBusiness stockBusiness) 
        {
            _stockBusiness = stockBusiness;
        }

        [HttpPost("AddStocks")]
        public async Task<bool> AddStocks([FromBody] AddStocks userStocks)
        {
            var Result = await _stockBusiness.AddStocks(userStocks).ConfigureAwait(false);
            return Result;
        }

        [HttpGet("GetAllStocks")]
        public List<Stocks> GetAllStocks()
        {
            var Result = _stockBusiness.GetAllStocks();
            return Result;
        }

        [HttpGet("GetAllCustomerStocks")]

        public List<CustomerStocks> GetAllCustomerStocks(int CustomerId)
        {
            var Result = _stockBusiness.GetAllCustomerStocks(CustomerId);
            return Result;
        }

        [HttpPost("AddCustomerStocks")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> AddCustomerStocks([FromBody] AddCustomerStocks stocks)
        {
            var Result = await _stockBusiness.AddCustomerStocks(stocks).ConfigureAwait(false);
           return Result;
        }

        [HttpPost("RemoveCustomerStocks")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> RemoveCustomerStocks([FromBody] AddCustomerStocks stocks)
        {
            stocks.StocksQuantity = 0 - stocks.StocksQuantity;
            var Result = await _stockBusiness.AddCustomerStocks(stocks).ConfigureAwait(false);
            return Result;
        }
    }
}
