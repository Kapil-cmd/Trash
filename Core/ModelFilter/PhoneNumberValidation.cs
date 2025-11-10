using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core
{
    public class PhoneNumberValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Phone number is required.");
            }

            string phoneNumber = value.ToString()!.Trim();


            string pattern = @"^(?:0|\+44)\d{9,10}$";
            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                return new ValidationResult("Invalid UK phone number.");
            }

            return ValidationResult.Success;
        }
    }
}
