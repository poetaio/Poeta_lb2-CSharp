using Wallets.BusinessLayer.Users;

namespace WalletsWPF.Wallets
{
    public class UserInfoViewModel
    {
        private User _user;

        public string FirstName { get => $"First name: {_user.Name}";  }
        public string LastName { get => $"Last name: {_user.Surname}"; }
        public string Login { get => $"Login: {_user.Login}"; }
        public string Email { get => $"Email: {_user.Email}"; }

        public UserInfoViewModel(User user)
        {
            _user = user;
        }
    }
}
