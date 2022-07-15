using CommonServices.ModelServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IStockBusiness
    {
        Task<bool> AddStocks(AddStocks stocks);

        List<Stocks> GetAllStocks();

        PortfolioDetails GetAllPortFolioDetails(int CustomerId);

        List<CustomerStocks> GetAllCustomerStocks(int CustomerId);

        Task<bool> AddCustomerStocks(AddCustomerStocks stocks);
    }
}
