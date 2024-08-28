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

            // Configuring the relationship between BankAccount and Transaction
            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.Transactions)
                .WithOne(t => t.From)
                .HasForeignKey(t => t.FromId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankAccount>()
                .HasMany(b => b.Transactions)
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
        }
    }
}