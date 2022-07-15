using CommonServices.ModelServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IMutualFundBusiness
    {
        Task<bool> AddMutualFunds(AddMutualFunds MutualFund);

        List<CustomerMutualFunds> GetAllCustomerMutualFunds(int CustomerId);

        Task<bool> AddCustomerMutualFunds(AddCustomerMutualFunds mutualFunds);

        List<Mutual> GetAllMutualFunds();
    }
}
