using Data.Models;

namespace Data.Helpers;

public static class BankAccountTypeHelper
{
    public static string GetStringFromBankAccountType(BankAccountType? accountType)
    {
        if (accountType == null) return "Unknown";
        
        return accountType switch
        {
            BankAccountType.Checking => "Checking",
            BankAccountType.Savings => "Savings",
            BankAccountType.CertificateOfDeposit => "Certificate of Deposit",
            BankAccountType.Investing => "Investing",
            _ => throw new ArgumentOutOfRangeException("No such bank account type")
        };
    }

    public static List<string> GetAllBankAccountTypes()
    {
        List<string> types = [];
        foreach (var accountType in Enum.GetValues(typeof(BankAccountType)).Cast<BankAccountType>())
        {
            types.Add(GetStringFromBankAccountType(accountType));
        }

        return types;
    }
}