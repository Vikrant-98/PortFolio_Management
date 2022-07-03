using PortFolio_Management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortFolio_Management.Business
{
    public interface IMutualFundBusiness
    {
        Task<bool> AddMutualFunds(AddMutualFunds MutualFund);

        List<CustomerMutualFunds> GetAllCustomerMutualFunds(int CustomerId);

        Task<bool> AddCustomerMutualFunds(AddCustomerMutualFunds mutualFunds);

        List<Mutual> GetAllMutualFunds();
    }
}
