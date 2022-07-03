using PortFolio_Management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortFolio_Management.Repository
{
    public interface IMutualFundService
    {
        Task<bool> AddMutualFunds(Mutual Mutual);

        List<CustomerMutualFunds> GetAllCustomerMutualFunds(int CustomerId);

        Task<bool> AddCustomerMututlFunds(CustomerMutualFunds mutualFunds);

        List<Mutual> GetAllMutualFunds();
    }
}
