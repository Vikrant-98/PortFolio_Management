using CommonServices.ModelServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IMutualFundService
    {
        Task<bool> AddMutualFunds(Mutual Mutual);

        List<CustomerMutualFunds> GetAllCustomerMutualFunds(int CustomerId);

        Task<bool> AddCustomerMututlFunds(CustomerMutualFunds mutualFunds);

        List<Mutual> GetAllMutualFunds();
    }
}
