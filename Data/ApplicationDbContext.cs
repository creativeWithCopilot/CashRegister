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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PLU>()
                .HasOne(p => p.Tax)
                .WithMany()
                .HasForeignKey(p => p.TaxId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete left Tax entries orphaned

            modelBuilder.Entity<PLU>()
                .HasOne(p => p.Department)
                .WithMany()
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete left Department entries orphaned

            modelBuilder.Entity<TransactionItem>()
                .HasOne(ti => ti.Transaction)
                .WithMany(t => t.TransactionItems)
                .HasForeignKey(ti => ti.TransactionId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete left Transaction entries orphaned

            modelBuilder.Entity<TransactionItem>()
                .HasOne(ti => ti.PLU)
                .WithMany()
                .HasForeignKey(ti => ti.PLUId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete left PLU entries orphaned

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Payment)
                .WithOne(p => p.Transaction)
                .HasForeignKey<Payment>(p => p.TransactionId);
        }

        // DbSet representing tables in the database
        public DbSet<Department> Departments { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<PLU> PLUs { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}