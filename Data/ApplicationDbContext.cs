using Microsoft.EntityFrameworkCore;
using CashRegister.Models;

namespace CashRegister.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor accepting DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}