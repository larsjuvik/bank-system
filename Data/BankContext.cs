using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
{
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("BankSystemDemo");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        DefineEntityRelations(modelBuilder);
        SeedDatabaseWithTestData(modelBuilder);
    }

    private static void DefineEntityRelations(ModelBuilder modelBuilder)
    {
        // User to bank account
        modelBuilder.Entity<User>()
            .HasMany(u => u.BankAccounts)
            .WithOne(b => b.Owner)
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
    }

    private static void SeedDatabaseWithTestData(ModelBuilder modelBuilder)
    {
        (int userIdIndex, int bankAccountIndex, int transactionIndex) = CreateKnownUsers(modelBuilder);
        CreateTestUsers(modelBuilder, GetRandomNames(100), userIdIndex+1, bankAccountIndex+1, transactionIndex+1);
    }

    private static List<string> GetRandomNames(int count)
    {
        List<string> firstNames = new List<string>
        {
            "James", "Mary", "John", "Patricia", "Robert", "Jennifer", "Michael", "Linda", "William", "Elizabeth",
            "David", "Barbara", "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah", "Charles", "Karen",
            "Christopher", "Nancy", "Daniel", "Lisa", "Matthew", "Betty", "Anthony", "Margaret", "Mark", "Sandra",
            "Donald", "Ashley", "Steven", "Kimberly", "Paul", "Emily", "Andrew", "Donna", "Joshua", "Michelle",
            "Kenneth", "Carol", "Kevin", "Amanda", "Brian", "Dorothy", "George", "Melissa", "Edward", "Deborah"
        };
        List<string> lastNames = new List<string>
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
            "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
            "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts"
        };

        var names = new List<string>();
        var random = new Random();
        for (int i = 0; i < count; i++)
        {
            var firstname = firstNames[random.Next(0, firstNames.Count)];
            var lastName = lastNames[random.Next(0, lastNames.Count)];
            names.Add(firstname + " " + lastName);
        }
        
        return names;
    }

    private static void CreateTestUsers(ModelBuilder modelBuilder, List<string> names, int userIndexStart, int bankAccountIndexStart, int transactionIndexStart)
    {
        var random = new Random();
        var userIndex = userIndexStart;
        var users = new List<User>();
        foreach (var name in names)
        {
            var username = name.Replace(" ", string.Empty) + random.Next(1000, 9999);
            var password = GetRandomAccountNumber(34) + GetRandomBalanceAmount();
            users.Add(GetDummyUser(userIndex, username, password, name, GetRandomBoolean()));
            userIndex++;
        }
        modelBuilder.Entity<User>().HasData(users);
        
        var bankAccountIndex = bankAccountIndexStart;
        var bankAccounts = new List<BankAccount>();
        foreach (var user in users)
        {
            var bankAccountsToCreate = random.Next(0, 4);
            for (var i = 0; i < bankAccountsToCreate; i++)
            {
                bankAccounts.Add(GetRandomBankAccount(bankAccountIndex, user.Id));
                bankAccountIndex++;
            }
        }
        modelBuilder.Entity<BankAccount>().HasData(bankAccounts);

        // Creating transactions
        AddRandomTransactions(modelBuilder, bankAccounts, transactionIndexStart);
    }

    /// <summary>
    /// Creates known users, and returns the max indices used
    /// </summary>
    /// <param name="modelBuilder">The modelbuilder to add models to</param>
    /// <returns>A tuple of max used ids. First represents user id, second bank account id and last is transaction id</returns>
    private static (int, int, int) CreateKnownUsers(ModelBuilder modelBuilder)
    {
        var users = new List<User>
        {
            GetDummyUser(1, "JaneDoe", "password123", "Jane Doe", true),
            GetDummyUser(2, "JohnDoe", "password123", "John Doe")
        };
        modelBuilder.Entity<User>().HasData(users);

        var bankAccounts = new List<BankAccount>
        {
            GetRandomBankAccount(1, 1),
            GetRandomBankAccount(2, 1),
            GetRandomBankAccount(3, 1),
            GetRandomBankAccount(4, 2),
            GetRandomBankAccount(5, 2)
        };
        modelBuilder.Entity<BankAccount>().HasData(bankAccounts);

        // Creating transactions
        var maxTransactionIndexUsed = AddRandomTransactions(modelBuilder, bankAccounts);

        return (2, 5, maxTransactionIndexUsed);
    }

    /// <summary>
    /// Generate random transactions between the accounts given as parameter.
    /// </summary>
    /// <param name="modelBuilder">The modelbuilder to add data to</param>
    /// <param name="accountsPool">The pool of users to send transactions between</param>
    /// <returns>The max id used in creating transaction ids</returns>
    private static int AddRandomTransactions(ModelBuilder modelBuilder, List<BankAccount> accountsPool, int startIndex = 1)
    {
        var random = new Random();
        var transactions = new List<Transaction>();
        var index = startIndex-1;
        foreach (var bankAccount in accountsPool)
        {
            index++;
            var otherBankAccount = accountsPool[random.Next(0, accountsPool.Count)];
            transactions.Add(GetRandomTransaction(index, bankAccount.Id, otherBankAccount.Id));
        }
        
        modelBuilder.Entity<Transaction>().HasData(transactions);
        return index;
    }

    private static Transaction GetRandomTransaction(int id, int bankAccountIdFrom, int bankAccountIdTo)
    {
        return new Transaction
        {
            Id = id,
            FromId = bankAccountIdFrom,
            ToId = bankAccountIdTo,
            Amount = GetRandomTransactionAmount(),
            TransactionDate = GetRandomDateTime()
        };
    }

    private static BankAccount GetRandomBankAccount(int id, int userId)
    {
        return new BankAccount
        {
            Id = id,
            UserId = userId,
            AccountNumber = GetRandomAccountNumber(10),
            Balance = GetRandomBalanceAmount(),
            CreatedDate = GetRandomDateTime(),
            HasDebitCard = GetRandomBoolean(),
            Type = GetRandomBankAccountType()
        };
    }

    private static User GetDummyUser(int id, string username, string password, string name, bool isAdmin = false)
    {
        User.CreateSaltAndHash(password, out var salt, out var passwordHash);
        var user = new User
        {
            Id = id,
            Username = username,
            Name = name,
            Salt = salt,
            PasswordHash = passwordHash,
            IsAdmin = isAdmin,
            BirthDate = DateTime.Now.AddYears(-(new Random().Next(20, 80))),
        };
        return user;
    }

    private static DateTime GetRandomDateTime()
    {
        var random = new Random();
        return DateTime.Now
            .AddDays(random.Next(0, 31))
            .AddMonths(random.Next(0, 12))
            .AddYears(random.Next(-40, -5));
    }

    private static decimal GetRandomTransactionAmount()
    {
        var random = new Random();
        return random.Next(-8000, 8000);
    }
    
    private static decimal GetRandomBalanceAmount()
    {
        var random = new Random();
        return random.Next(-50000, 1000000);
    }

    private static bool GetRandomBoolean()
    {
        var random = new Random();
        return random.Next(0, 2) == 1;
    }

    private static BankAccountType GetRandomBankAccountType()
    {
        var random = new Random();
        return (BankAccountType)random.Next(0, 3);
    }

    private static string GetRandomAccountNumber(int length)
    {
        var random = new Random();
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}