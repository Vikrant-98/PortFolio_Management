using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortFolio_Management.Models
{
    public class Mutual
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string MutualFundName { get; set; }
        public double MutualFundPrice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } 
    }

    public class MutualResp
    {
        public string MutualFundName { get; set; }
        public double MutualFundPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalMutualFundPrice { get; set; }
    }
}
