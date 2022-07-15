using CommonServices.ModelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
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
