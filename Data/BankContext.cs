using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
{
    public DbSet<BankAccount> BankAccounts { get; init; }
    public DbSet<Transaction> Transactions { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<UserLogin> UserLogins { get; init; }

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
        
        // User to user logins
        modelBuilder.Entity<User>()
            .HasMany(u => u.UserLogins)
            .WithOne(ul => ul.User)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Bank account
        modelBuilder.Entity<BankAccount>()
            .Property(b=>b.Type)
            .HasConversion<string>();
        
        modelBuilder.Entity<BankAccount>()
            .HasMany(b => b.FromTransactions)
            .WithOne(t => t.From)
            .HasForeignKey(t => t.FromId)
            .OnDelete(DeleteBehavior.Restrict);

        // Bank account
        modelBuilder.Entity<BankAccount>()
            .HasMany(b => b.ToTransactions)
            .WithOne(t => t.To)
            .HasForeignKey(t => t.ToId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    /// <summary>
    /// Creates a set of known users, and then a lot of test users.
    /// </summary>
    private static void SeedDatabaseWithTestData(ModelBuilder modelBuilder)
    {
        var (userIdIndex, bankAccountIndex, transactionIndex) = CreateKnownUsers(modelBuilder);
        CreateTestData(modelBuilder, GetRandomNames(100), userIdIndex+1, bankAccountIndex+1, transactionIndex+1);
    }

    private static List<string> GetRandomNames(int count)
    {
        var maleFirstNames = new List<string>
        {
            "James", "John", "Robert", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles",
            "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua",
            "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan",
            "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Scott", "Frank", "Brandon",
            "Benjamin", "Gregory", "Samuel", "Patrick", "Alexander", "Jack", "Dennis", "Jerry", "Tyler", "Aaron"
        };

        var maleMiddleNames = new List<string>
        {
            "James", "Michael", "John", "David", "William", "Joseph", "Thomas", "Charles", "Edward", "Alexander",
            "Scott", "Paul", "Christopher", "Daniel", "Anthony", "Ray", "Patrick", "Lee", "Andrew", "Benjamin",
            "Richard", "Brian", "Allen", "George", "Robert", "Ryan", "Stephen", "Matthew", "Timothy", "Alexander",
            "Samuel", "Mark", "Joseph", "Henry", "Jacob", "Frank", "Peter", "Nicholas", "Gregory", "Lawrence",
            "Phillip", "Martin", "Nathan", "Jameson", "Elliot", "Aaron", "Francis", "Adam", "Raymond", "Louis"
        };
        
        var femaleFirstNames = new List<string>
        {
            "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen",
            "Nancy", "Margaret", "Sandra", "Ashley", "Kimberly", "Emily", "Donna", "Michelle", "Dorothy", "Carol",
            "Amanda", "Melissa", "Deborah", "Stephanie", "Rebecca", "Sharon", "Laura", "Cynthia", "Kathleen", "Amy",
            "Angela", "Shirley", "Brenda", "Pamela", "Emma", "Nicole", "Helen", "Samantha", "Katherine", "Christine",
            "Debra", "Rachel", "Carolyn", "Janet", "Catherine", "Maria", "Heather", "Diane", "Virginia", "Julie"
        };

        var femaleMiddleNames = new List<string>
        {
            "Marie", "Ann", "Elizabeth", "Grace", "Renee", "Lynn", "Jean", "Rose", "Elaine", "Nicole",
            "Michelle", "Marie", "Jane", "Louise", "Faith", "Claire", "Hope", "Joy", "Diana", "Marie",
            "Evelyn", "Christine", "Leigh", "Marie", "Mae", "June", "Lee", "Elaine", "Pearl", "Sue",
            "Marie", "Victoria", "Faye", "Irene", "Marie", "Elaine", "Claire", "Marie", "Michelle", "Isabelle",
            "Ann", "Marie", "Christine", "Joyce", "Sophia", "Marie", "Marie", "Taylor", "Jean", "Marie"
        };

        var lastNames = new List<string>
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
            "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores",
            "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts"
        };

        var names = new List<string>();
        var random = new Random();
        var femaleNamesCount = count / 2;
        var maleNamesCount = count / 2;
        
        // If the number is odd, randomly assign the extra name to male or female
        if (count % 2 != 0)
        {
            if (random.Next(2) == 0) // 0 or 1
            {
                femaleNamesCount++;
            }
            else
            {
                maleNamesCount++;
            }
        }
        
        for (var i = 0; i < femaleNamesCount; i++)
        {
            var firstname = femaleFirstNames[random.Next(0, femaleFirstNames.Count)];
            var middleName = string.Empty;
            if (GetRandomBoolean())
            {
                middleName = femaleMiddleNames[random.Next(0, femaleMiddleNames.Count)];
            }

            var lastName = lastNames[random.Next(0, lastNames.Count)];
            if (string.IsNullOrEmpty(middleName))
            {
                names.Add(firstname + " " + lastName);
            }
            else
            {
                names.Add(firstname + " " + middleName + " " + lastName);
            }
        }
        
        for (var i = 0; i < maleNamesCount; i++)
        {
            var firstname = maleFirstNames[random.Next(0, maleFirstNames.Count)];
            var middleName = string.Empty;
            if (GetRandomBoolean())
            {
                middleName = maleMiddleNames[random.Next(0, maleMiddleNames.Count)];
            }

            var lastName = lastNames[random.Next(0, lastNames.Count)];
            if (string.IsNullOrEmpty(middleName))
            {
                names.Add(firstname + " " + lastName);
            }
            else
            {
                names.Add(firstname + " " + middleName + " " + lastName);
            }
        }
        
        return names;
    }

    /// <summary>
    /// Creates randomized test users, bank accounts and transactions.
    /// </summary>
    /// <param name="modelBuilder">The model builder to add models to</param>
    /// <param name="names">The names of the users - the length decides number of users added</param>
    /// <param name="userIndexStart">The start index of user id</param>
    /// <param name="bankAccountIndexStart">The start index of bank account id</param>
    /// <param name="transactionIndexStart">The start index of transaction id</param>
    private static void CreateTestData(ModelBuilder modelBuilder, List<string> names, int userIndexStart, int bankAccountIndexStart, int transactionIndexStart)
    {
        var random = new Random();
        
        var userIndex = userIndexStart;
        var users = new List<User>();
        foreach (var name in names)
        {
            var username = name.Replace(" ", string.Empty) + random.Next(1000, 9999);
            var password = GetRandomAccountNumber() + GetRandomBalanceAmount();
            users.Add(GetDummyUser(userIndex, username, password, name, GetRandomBoolean()));
            userIndex++;
        }
        modelBuilder.Entity<User>().HasData(users);
        
        // Add random login datetimes
        var userLoginIndex = 1;
        var logins = new List<UserLogin>();
        foreach (var user in users)
        {
            var amountOfLogins = random.Next(0, 20);
            for (var i = 0; i < amountOfLogins; i++)
            {
                logins.Add(new UserLogin
                {
                    Id = userLoginIndex,
                    UserId = user.Id,
                    LoginDateTime = GetRandomLoginTime()
                });
                userLoginIndex++;
            }
        }
        modelBuilder.Entity<UserLogin>().HasData(logins);

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
    /// <param name="startIndex">The start index for first transaction entity</param>
    /// <returns>The max id used in creating transaction ids</returns>
    private static int AddRandomTransactions(ModelBuilder modelBuilder, List<BankAccount> accountsPool, int startIndex = 1)
    {
        var random = new Random();
        var transactions = new List<Transaction>();
        var index = startIndex-1;
        foreach (var bankAccount in accountsPool)
        {
            const int transactionsPerBankAccount = 10;
            for (int i = 0; i < transactionsPerBankAccount; i++)
            {
                index++;
                var otherBankAccount = accountsPool[random.Next(0, accountsPool.Count)];
                transactions.Add(GetRandomTransaction(index, bankAccount.Id, otherBankAccount.Id));
            }
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
            AccountNumber = GetRandomAccountNumber(),
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
    
    private static DateTime GetRandomLoginTime()
    {
        var random = new Random();
        return DateTime.Now
            .AddDays(random.Next(-31, 0))
            .AddMonths(random.Next(-12, 0))
            .AddYears(random.Next(-3, 0));
    }

    private static decimal GetRandomTransactionAmount()
    {
        var random = new Random();
        return random.Next(1, 20000);
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

    public static string GetRandomAccountNumber()
    {
        var random = new Random();
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, 16)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}