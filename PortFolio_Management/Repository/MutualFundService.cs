using PortFolio_Management.Data;
using PortFolio_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortFolio_Management.Repository
{
    public class MutualFundService : IMutualFundService
    {
        private readonly ApplicationDbContext _dbContext;
        public MutualFundService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddMutualFunds(Mutual stocks)
        {
            try
            {
                var Result = await _dbContext.Mutual.AddAsync(stocks).ConfigureAwait(false);
                _dbContext.SaveChanges();
                if (Result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<bool> AddCustomerMututlFunds(CustomerMutualFunds stocks)
        {
            try
            {
                var mutualDetails = _dbContext.CustomerMutualFunds.ToList();
                var flag = mutualDetails.Any(x => x.MutualFundId == stocks.MutualFundId && x.CustomerId == stocks.CustomerId);

                if (flag)
                {
                    var Entries = (from x in _dbContext.CustomerMutualFunds
                                   where x.MutualFundId == stocks.MutualFundId && x.CustomerId == stocks.CustomerId
                                   select x).First();
                    if (Entries != null)
                    {
                        Entries.CustomerId = stocks.CustomerId;
                        Entries.MutualFundQuantity = Entries.MutualFundQuantity + stocks.MutualFundQuantity < 0 ? Entries.MutualFundQuantity : Entries.MutualFundQuantity + stocks.MutualFundQuantity;
                        Entries.MutualFundId = stocks.MutualFundId;
                        Entries.ModifiedDate = DateTime.Now;
                        _dbContext.CustomerMutualFunds.Update(Entries);
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (stocks.MutualFundQuantity <= 0)
                    {
                        return false;
                    }
                    var Result = await _dbContext.CustomerMutualFunds.AddAsync(stocks).ConfigureAwait(false);
                    _dbContext.SaveChanges();
                    if (Result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<CustomerMutualFunds> GetAllCustomerMutualFunds(int CustomerId)
        {
            try
            {
                var CustomerDetails = _dbContext.CustomerMutualFunds.Where(x => x.CustomerId == CustomerId).ToList();
                return CustomerDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Mutual> GetAllMutualFunds()
        {
            try
            {
                return _dbContext.Mutual.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
