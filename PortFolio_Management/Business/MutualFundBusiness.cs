using PortFolio_Management.Models;
using PortFolio_Management.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortFolio_Management.Business
{
    public class MutualFundBusiness : IMutualFundBusiness
    {
        private readonly IMutualFundService _mutualFundService;

        public MutualFundBusiness(IMutualFundService mutualFundService) 
        {
            _mutualFundService = mutualFundService;
        }

        public async Task<bool> AddMutualFunds(AddMutualFunds MutualFund)
        {
            var addMutuals = new Mutual()
            {
                MutualFundName = MutualFund.MutualFundName,
                MutualFundPrice = MutualFund.MutualFundPrice,
                ModifiedDate = System.DateTime.Now
            };
            return await _mutualFundService.AddMutualFunds(addMutuals).ConfigureAwait(false);
        }

        public List<Mutual> GetAllMutualFunds()
        {
            return _mutualFundService.GetAllMutualFunds();
        }

        public async Task<bool> AddCustomerMutualFunds(AddCustomerMutualFunds mutualFunds)
        {
            CustomerMutualFunds customerStocks = new CustomerMutualFunds()
            {
                CustomerId = mutualFunds.CustomerId,
                MutualFundId = mutualFunds.MutualFundId,
                MutualFundQuantity = mutualFunds.MutualFundQuantity,
                ModifiedDate = System.DateTime.Now
            };
            return await _mutualFundService.AddCustomerMututlFunds(customerStocks).ConfigureAwait(false);
        }

        public List<CustomerMutualFunds> GetAllCustomerMutualFunds(int CustomerId)
        {
            return _mutualFundService.GetAllCustomerMutualFunds(CustomerId);
        }
    }
}
