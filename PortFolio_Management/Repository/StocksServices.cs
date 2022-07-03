using PortFolio_Management.Data;
using PortFolio_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortFolio_Management.Repository
{
    public class StocksServices : IStockServices
    {
        private readonly ApplicationDbContext _dbContext;
        public StocksServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddStocks(Stocks stocks)
        {
            try
            {
                var Result = await _dbContext.Stocks.AddAsync(stocks).ConfigureAwait(false);
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

        public async Task<bool> AddCustomerStocks(CustomerStocks stocks)
        {
            try
            {
                var stockDetails = _dbContext.CustomerStocks.ToList();
                var flag = stockDetails.Any(x=>x.StocksId == stocks.StocksId && x.CustomerId == stocks.CustomerId);

                if (flag)
                {
                    var Entries = (from x in _dbContext.CustomerStocks
                                   where x.StocksId == stocks.StocksId && x.CustomerId == stocks.CustomerId
                                   select x).First();
                    if (Entries != null)
                    {
                        Entries.StocksId = stocks.StocksId;
                        Entries.StocksQuantity = Entries.StocksQuantity + stocks.StocksQuantity < 0? Entries.StocksQuantity : Entries.StocksQuantity + stocks.StocksQuantity;
                        Entries.CustomerId = stocks.CustomerId;
                        Entries.ModifiedDate = DateTime.Now;
                        _dbContext.CustomerStocks.Update(Entries);
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
                    if (stocks.StocksQuantity <= 0)
                    {
                        return false;
                    }
                    var Result = await _dbContext.CustomerStocks.AddAsync(stocks).ConfigureAwait(false);
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

        public List<CustomerStocks> GetAllCustomerStocks(int CustomerId)
        {
            try
            {
                var CustomerDetails = _dbContext.CustomerStocks.Where(x=>x.CustomerId == CustomerId).ToList();
                return CustomerDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PortfolioDetails GetAllPortFolioDetails(int CustomerId)
        {
            try
            {
                List<StocksResp> ListStockDetails = new List<StocksResp>();
                List<MutualResp> ListMutualFundsDetails = new List<MutualResp>();
                var CustomerStockDetails = _dbContext.CustomerStocks.Where(x => x.CustomerId == CustomerId).ToList();
                var CustomerMutualDetails = _dbContext.CustomerMutualFunds.Where(x => x.CustomerId == CustomerId).ToList();
                var stocksDetails = _dbContext.Stocks.ToList();
                var mutualFundDetails = _dbContext.Mutual.ToList();
                foreach (var item in CustomerStockDetails)
                {
                    var result = stocksDetails.Where(x => x.ID == item.StocksId).FirstOrDefault();
                    ListStockDetails.Add(new StocksResp() 
                    {
                        StockName = result.StockName,
                        StockPrice = result.StockPrice,
                        StockQuantity = item.StocksQuantity,
                        TotalStockPrice = result.StockPrice * item.StocksQuantity
                    });
                }

                foreach (var item in CustomerMutualDetails)
                {
                    var result = mutualFundDetails.Where(x => x.ID == item.MutualFundId).FirstOrDefault();
                    ListMutualFundsDetails.Add(new MutualResp() 
                    {
                        MutualFundName = result.MutualFundName,
                        MutualFundPrice = result.MutualFundPrice,
                        Quantity = item.MutualFundQuantity,
                        TotalMutualFundPrice = result.MutualFundPrice * item.MutualFundQuantity
                    });
                }

                PortfolioDetails portfolioDetails = new PortfolioDetails()
                {
                    StockDetails = ListStockDetails,
                    MutualFundsDetails = ListMutualFundsDetails,
                    PortfolioId = CustomerId,
                };
                
                return portfolioDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Stocks> GetAllStocks()
        {
            try
            {
                return _dbContext.Stocks.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

