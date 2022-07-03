using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortFolio_Management.Models
{
    public class Stocks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }        
        public string StockName { get; set; }
        public double StockPrice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; }
    }
    
    public class StocksResp
    {
        public string StockName { get; set; }
        public double StockPrice { get; set; }
        public int StockQuantity { get; set; }
        public double TotalStockPrice { get; set; }

    }
}
