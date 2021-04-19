namespace Wallets.BusinessLayer.Users
{
    public class RegistrationUser
    {
        public RegistrationUser(string firstName = "", string lastName = "", string email = "", string login = "", string password = "")
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Password = password;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
