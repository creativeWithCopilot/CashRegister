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
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One(Category)-to-Many(Product) without navigation to dependents
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // One(SaleItem)-to-Many(Product) without navigation to dependents
            modelBuilder.Entity<SaleItem>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.PLUCode)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // One(Transaction)-to-Many(SaleItem) without navigation to Required
            modelBuilder.Entity<Transaction>()
                .HasMany(t => t.SaleItems)
                .WithOne(t => t.Transaction)
                .HasForeignKey(e => e.TransactionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}