using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortFolio_Management.Models
{
    public class PortfolioDetails
    {
        public int PortfolioId { get; set; }
        public List<StocksResp> StockDetails { get; set; }
        public List<MutualResp> MutualFundsDetails { get; set; }
    }
}
