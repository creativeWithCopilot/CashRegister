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
            // One(Tax)-to-Many(Category) without navigation to dependents
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Tax)
                .WithMany()
                .HasForeignKey(c => c.TaxId)
                .IsRequired();

            // One(Category)-to-Many(Product) without navigation to dependents
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            // One(SaleItem)-to-Many(Product) without navigation to dependents
            modelBuilder.Entity<SaleItem>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.Product)
                .IsRequired();

            // One(Transaction)-to-Many(SaleItem) without navigation to Required
            modelBuilder.Entity<Transaction>()
                .HasMany(t => t.SaleItems)
                .WithOne(t => t.Transaction)
                .HasForeignKey(e => e.TransactionId)
                .IsRequired();
        }
    }
}