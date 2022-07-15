using Business.Interface;
using CommonServices.ModelServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Controllers
{
    public class MutualFundController : Controller
    {
        private readonly IMutualFundBusiness _mutualFundBusiness;

        public MutualFundController(IMutualFundBusiness mutualFundBusiness) 
        {
            _mutualFundBusiness = mutualFundBusiness;
        }

        // GET: MutualFundController
        [HttpGet("GetAllMutualFunds")]
        public List<Mutual> GetAllMutualFunds()
        {
            var Result = _mutualFundBusiness.GetAllMutualFunds();
            return Result;
        }

        [HttpPost("AddMutualFunds")]
        public async Task<bool> AddMutualFunds([FromBody] AddMutualFunds MutualFund)
        {
            var Result = await _mutualFundBusiness.AddMutualFunds(MutualFund).ConfigureAwait(false);
            return Result;
        }

        [HttpPost("AddCustomerMutualFunds")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> AddCustomerMutualFunds([FromBody] AddCustomerMutualFunds mutualFunds)
        {
            var Result = await _mutualFundBusiness.AddCustomerMutualFunds(mutualFunds).ConfigureAwait(false);
            return Result;
        }

        [HttpPost("RemoveCustomerMutualFunds")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> RemoveCustomerMutualFunds([FromBody] AddCustomerMutualFunds mutualFunds)
        {
            mutualFunds.MutualFundQuantity = 0 - mutualFunds.MutualFundQuantity;
            var Result = await _mutualFundBusiness.AddCustomerMutualFunds(mutualFunds).ConfigureAwait(false);
            return Result;
        }
    }
}
