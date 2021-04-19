using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wallets.Services
{
    public class ValidationService
    {
        public string InvalidEmailMessage = "Email must match a@b.cd";
        public Regex EmailPattern = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
        public string InvalidPasswordMessage = "Password must contain Uppercease letter and digit, minimum 5 signs";
        public Regex PasswordPattern = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{5,}$", RegexOptions.Compiled);
        public string InvalidLoginMessage = "Login must contain at least 5 signs";
        public Regex LoginPattern = new Regex("^[.]{5,}", RegexOptions.Compiled);
        public string InvalidNameMessage = "Name must consist of 3-10 signs";
        public Regex NamePattern = new Regex(".{3,10}", RegexOptions.Compiled);
        public string InvalidCurrencyMessage = "Currency must Match \"USD\"";
        public Regex CurrencyPattern = new Regex("[A-Z]{3}", RegexOptions.Compiled);
    }
}
