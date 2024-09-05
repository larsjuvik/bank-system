using System.ComponentModel.DataAnnotations;

namespace BankSystem.WebApp.DTOs.DataAnnotations
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime dateOfBirth)
            {
                var age = DateTime.Today.Year - dateOfBirth.Year;

                // If not had birthday yet this year, subtract one year
                if (dateOfBirth > DateTime.Today.AddYears(-age))
                    age--;

                if (age < _minimumAge)
                    return false;

                return true;
            }

            return false;
        }
    }
}