using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Wallets.BusinessLayer.Users;
using Wallets.Services;
using WalletsWPF.Navigation;

namespace WalletsWPF.Authentication
{
    public class SignInViewModel : INotifyPropertyChanged, INavigatable<AuthNavigatableTypes>
    {
        private AuthenticationUser _authUser = new AuthenticationUser();
        private Action _gotoSignUp;
        private Action _gotoWallets;
        private bool _isEnabled = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public AuthNavigatableTypes Type
        {
            get
            {
                return AuthNavigatableTypes.SignIn;
            }
        }
        public string Login
        {
            get
            {
                return _authUser.Login;
            }
            set
            {
                _authUser.Login = value;
                OnPropertyChanged(nameof(Login));
                SignInCommand.RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get
            {
                return _authUser.Password;
            }
            set
            {
                _authUser.Password = value;
                OnPropertyChanged(nameof(Password));
                SignInCommand.RaiseCanExecuteChanged();
            }
        }
        public User CurrentUser { get; set; }


        public DelegateCommand SignInCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SignUpCommand { get; }
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public SignInViewModel(Action gotoSignUp, Action gotoWallets, User currentUser)
        {
            CurrentUser = currentUser;
            SignInCommand = new DelegateCommand(SignIn, IsSignInEnabled);
            CloseCommand = new DelegateCommand(() => Environment.Exit(0));
            _gotoSignUp = gotoSignUp;
            _gotoWallets = gotoWallets;
            SignUpCommand = new DelegateCommand(gotoSignUp);
        }

        private async void SignIn()
        {
            if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password))
                MessageBox.Show("Login or password is empty.");
            else
            {
                var authService = new AuthenticationService();
                User user = null;
                try
                {
                    IsEnabled = false;
                    user = await authService.Authenticate(_authUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In failed: {ex.Message}");
                    return;
                }
                finally
                {
                    IsEnabled = true;
                }
                MessageBox.Show("Sign In was successful");

                CopyUser(user);
                _gotoWallets.Invoke();
            }
        }

        private bool IsSignInEnabled()
        {
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearSensitiveData()
        {
            Password = "";
        }

        public Task UploadData()
        {
            return null;
        }

        private void CopyUser(User user)
        {
            CurrentUser.Name = user.Name;
            CurrentUser.Surname = user.Surname;
            CurrentUser.Login = user.Login;
            CurrentUser.Guid = user.Guid;
            CurrentUser.Email = user.Email;
        }
    }
}
