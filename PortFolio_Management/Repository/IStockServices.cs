using PortFolio_Management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortFolio_Management.Repository
{
    public interface IStockServices
    {
        Task<bool> AddStocks(Stocks stocks);
        List<Stocks> GetAllStocks();
        Task<bool> AddCustomerStocks(CustomerStocks stocks);
        List<CustomerStocks> GetAllCustomerStocks(int CustomerId);
        PortfolioDetails GetAllPortFolioDetails(int CustomerId);
    }
}
