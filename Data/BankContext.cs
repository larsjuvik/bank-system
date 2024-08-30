using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data
{
    public class BankContext : DbContext
    {
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

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

            // User to bank account
            modelBuilder.Entity<User>()
                .HasMany(u => u.BankAccounts)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

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
            modelBuilder.Entity<User>().HasData(
                CreateDummyUser(1, "JaneDoe", "password123", "Jane Doe"),
                CreateDummyUser(2, "JohnDoe", "password123", "John Doe"),
                CreateDummyUser(3, "TestUser", "password123", "Test User")
            );
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount
                {
                    Id = 1,
                    UserId = 1,
                    AccountNumber = "1234567890",
                    Balance = 10000,
                    CreatedDate = DateTime.Now
                },
                new BankAccount
                {
                    Id = 2,
                    UserId = 2,
                    AccountNumber = "0987654321",
                    Balance = 2000,
                    CreatedDate = DateTime.Now
                },
                new BankAccount
                {
                    Id = 3,
                    UserId = 3,
                    AccountNumber = "1357924680",
                    Balance = 3000,
                    CreatedDate = DateTime.Now
                }
            );
        }

        private static User CreateDummyUser(int id, string username, string password, string name)
        {
            User.CreateSaltAndHash(password, out var salt, out var passwordHash);
            var user = new User
            {
                Id = id,
                Username = username,
                Name = name,
                Salt = salt,
                PasswordHash = passwordHash
            };
            return user;
        }
    }
}