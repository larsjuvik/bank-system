using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data
{
    public class BankContext : DbContext
    {
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("BankSystemDemo");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.FromTransactions)
                .WithOne(t => t.From)
                .HasForeignKey(t => t.FromId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.ToTransactions)
                .WithOne(t => t.To)
                .HasForeignKey(t => t.ToId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeding initial data
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount
                {
                    Id = 1,
                    AccountNumber = "1234567890",
                    AccountHolderName = "John Doe",
                    Balance = 1000m,
                    CreatedDate = DateTime.Now
                },
                new BankAccount
                {
                    Id = 2,
                    AccountNumber = "0987654321",
                    AccountHolderName = "Jane Doe",
                    Balance = 2000m,
                    CreatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    FromId = 1,
                    ToId = 2,
                    Amount = 500m,
                    TransactionDate = DateTime.Now
                },
                new Transaction
                {
                    Id = 2,
                    FromId = 2,
                    ToId = 1,
                    Amount = 750m,
                    TransactionDate = DateTime.Now
                }
            );
        }
    }
}