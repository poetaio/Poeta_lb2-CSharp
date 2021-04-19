using System.Text.RegularExpressions;

namespace Wallets.Services
{
    public class ValidationService
    {
        public string InvalidEmailMessage = "Email must match a@b.cd";
        public Regex EmailPattern = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
        public string InvalidPasswordMessage = "Password must contain an Uppercease letter and a digit, and 5 to 20 signs";
        public Regex PasswordPattern = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{5,20}$");
        public string InvalidLoginMessage = "Login must contain at least 5 to 10 signs";
        public Regex LoginPattern = new Regex("^.{5,10}$");
        public string InvalidNameMessage = "Name must consist of 3-10 signs";
        public string InvalidFirstNameMessage = "First Name must consist of 3-10 signs";
        public string InvalidLastNameMessage = "Last Name must consist of 3-20 signs";
        public Regex NamePattern = new Regex("^.{3,20}$");
        public string InvalidCurrencyMessage = "Currency must Match \"USD\"";
        public Regex CurrencyPattern = new Regex("^[A-Z]{3}$");
    }
}
