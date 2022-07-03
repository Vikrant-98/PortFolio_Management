using Microsoft.EntityFrameworkCore;
using PortFolio_Management.Models;

namespace PortFolio_Management.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
            public Microsoft.EntityFrameworkCore.DbSet<Customer> Customer { get; set; }
            public Microsoft.EntityFrameworkCore.DbSet<Mutual> Mutual { get; set; }
            public Microsoft.EntityFrameworkCore.DbSet<CustomerMutualFunds> CustomerMutualFunds { get; set; }
            public Microsoft.EntityFrameworkCore.DbSet<CustomerStocks> CustomerStocks { get; set; }
            public Microsoft.EntityFrameworkCore.DbSet<Stocks> Stocks { get; set; }

        } 
}

