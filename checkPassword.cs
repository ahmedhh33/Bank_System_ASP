using System.Text.RegularExpressions;

namespace BankSystem_API
{
    public class checkPassword
    {
        public static bool IsStrongPassword(string password)
        {
            // Define regular expressions for each criterion
            Regex minLengthRegex = new Regex(@".{8,}");
            Regex uppercaseRegex = new Regex(@"[A-Z]");
            Regex lowercaseRegex = new Regex(@"[a-z]");
            Regex digitRegex = new Regex(@"\d");
            Regex symbolRegex = new Regex(@"[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]");

            // Check each criterion
            bool hasMinLength = minLengthRegex.IsMatch(password);
            bool hasUppercase = uppercaseRegex.IsMatch(password);
            bool hasLowercase = lowercaseRegex.IsMatch(password);
            bool hasDigit = digitRegex.IsMatch(password);
            bool hasSymbol = symbolRegex.IsMatch(password);

            // Check if all criteria are met
            return hasMinLength && hasUppercase && hasLowercase && hasDigit && hasSymbol;
        }
    }
}
