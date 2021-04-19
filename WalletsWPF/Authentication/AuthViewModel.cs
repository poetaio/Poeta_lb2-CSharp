using System;
using System.Threading.Tasks;
using Wallets.BusinessLayer.Users;
using WalletsWPF.Navigation;

namespace WalletsWPF.Authentication
{
    public class AuthViewModel : NavigationBase<AuthNavigatableTypes>, INavigatable<MainNavigatableTypes>
    {
        public User CurrentUser { get; set; }

        private Action _signInSuccess;

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Auth;
            }
        }

        public AuthViewModel(Action signInSuccess, User currentUser)
        {
            CurrentUser = currentUser;
            _signInSuccess = signInSuccess;
            Navigate(AuthNavigatableTypes.SignIn);
        }
        protected override INavigatable<AuthNavigatableTypes> CreateViewModel(AuthNavigatableTypes type)
        {
            switch (type)
            {
                case AuthNavigatableTypes.SignIn:
                    return new SignInViewModel(() => Navigate(AuthNavigatableTypes.SignUp), _signInSuccess, CurrentUser);
                case AuthNavigatableTypes.SignUp:
                    return new SignUpViewModel(() => Navigate(AuthNavigatableTypes.SignIn));
                default:
                    return new SignUpViewModel(() => Navigate(AuthNavigatableTypes.SignIn)); 
            }
        }

        public void ClearSensitiveData()
        {
            CurrentViewModel.ClearSensitiveData();
        }

        public Task UploadData()
        {
            return null;
        }
    }
}
