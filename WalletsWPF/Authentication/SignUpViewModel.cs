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
    public class SignUpViewModel : INotifyPropertyChanged, INavigatable<AuthNavigatableTypes>
    {
        private RegistrationUser _regUser = new RegistrationUser();

        private Action _gotoSignIn;

        public event PropertyChangedEventHandler PropertyChanged;

        public AuthNavigatableTypes Type
        {
            get
            {
                return AuthNavigatableTypes.SignUp;
            }
        }


        public string FirstName
        {
            get
            {
                return _regUser.FirstName;
            }
            set
            {
                _regUser.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        public string LastName
        {
            get
            {
                return _regUser.LastName;
            }
            set
            {
                _regUser.LastName = value;
                OnPropertyChanged(nameof(LastName));
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get
            {
                return _regUser.Email;
            }
            set
            {
                _regUser.Email = value;
                OnPropertyChanged(nameof(Email));
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }
        public string Login
        {
            get
            {
                return _regUser.Login;
            }
            set
            {
                _regUser.Login = value;
                OnPropertyChanged(nameof(Login));
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get
            {
                return _regUser.Password;
            }
            set
            {
                _regUser.Password = value;
                OnPropertyChanged(nameof(Password));
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand SignUpCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand GotoSignInCommand { get; }

        public SignUpViewModel(Action gotoSignUp)
        {
            SignUpCommand = new DelegateCommand(SignUp, IsSignUpEnabled);
            CloseCommand = new DelegateCommand(() => Environment.Exit(0));
            _gotoSignIn = gotoSignUp;
            GotoSignInCommand = new DelegateCommand(gotoSignUp);
        }

        private async void SignUp()
        {
            var authService = new AuthenticationService();

            try
            {
                await authService.RegisterUser(_regUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign Up failed: {ex.Message}");
                return;
            }

            MessageBox.Show("User successfuly registered, please sigh in");
            _gotoSignIn.Invoke();
        }

        private bool IsSignUpEnabled()
        {
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password)
                && !String.IsNullOrWhiteSpace(LastName) && !String.IsNullOrWhiteSpace(FirstName)
                && !String.IsNullOrWhiteSpace(Email);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearSensitiveData()
        {
            _regUser = new RegistrationUser();
        }

        public Task UploadData()
        {
            return null;
        }
    }
}
