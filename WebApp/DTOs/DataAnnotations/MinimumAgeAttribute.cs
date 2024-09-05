using System.ComponentModel.DataAnnotations;

namespace BankSystem.WebApp.DTOs.DataAnnotations
{
    public class MinimumAgeAttribute(int minimumAge) : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateOfBirth)
            {
                var age = DateTime.Today.Year - dateOfBirth.Year;

                // If not had birthday yet this year, subtract one year
                if (dateOfBirth > DateTime.Today.AddYears(-age))
                    age--;

                if (age < minimumAge)
                    return false;

                return true;
            }

            return false;
        }
    }
}