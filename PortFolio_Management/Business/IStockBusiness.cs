using PortFolio_Management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortFolio_Management.Business
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
