using Business.Interface;
using CommonServices.ModelServices;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class StockBusiness : IStockBusiness
    {
        private readonly IStockServices _stockService;
        public StockBusiness(IStockServices stockService)
        {
            _stockService = stockService;
        }

        public async Task<bool> AddStocks(AddStocks stocks)
        {
            var addStocks = new Stocks()
            {
                StockName = stocks.StockName,
                StockPrice = stocks.StockPrice,
                ModifiedDate = System.DateTime.Now
            };
            return await _stockService.AddStocks(addStocks).ConfigureAwait(false);
        }

        public List<Stocks> GetAllStocks()
        {
            return _stockService.GetAllStocks();
        }

        public PortfolioDetails GetAllPortFolioDetails(int CustomerId)
        {
            return _stockService.GetAllPortFolioDetails(CustomerId);
        }

        public async Task<bool> AddCustomerStocks(AddCustomerStocks stocks)
        {
            CustomerStocks customerStocks = new CustomerStocks()
            {
                CustomerId = stocks.CustomerId,
                StocksId = stocks.StocksId,
                StocksQuantity = stocks.StocksQuantity,
                ModifiedDate = System.DateTime.Now
            };
            return await _stockService.AddCustomerStocks(customerStocks).ConfigureAwait(false);
        }

        public List<CustomerStocks> GetAllCustomerStocks(int CustomerId)
        {
            try
            {
                var CustomerDetails = _stockService.GetAllCustomerStocks(CustomerId);
                return CustomerDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
