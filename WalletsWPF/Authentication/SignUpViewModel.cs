using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Wallets.BusinessLayer.Users;
using Wallets.Services;
using WalletsWPF.Navigation;
using System.Runtime.CompilerServices;

namespace WalletsWPF.Authentication
{
    public class SignUpViewModel : INotifyPropertyChanged, INavigatable<AuthNavigatableTypes>
    {
        private RegistrationUser _regUser = new RegistrationUser();
        private Action _gotoSignIn;
        private ValidationService _validationService = new ValidationService();
        private string _validationMessage = "";

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
                ShowValidationMessage();
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
                ShowValidationMessage();
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
                ShowValidationMessage();
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
                ShowValidationMessage();
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
                ShowValidationMessage();
                OnPropertyChanged(nameof(Password));
                SignUpCommand.RaiseCanExecuteChanged();
            }
        }
        public string ValidationMessage 
        {
            get => _validationMessage;
            set
            {
                _validationMessage = value;
                OnPropertyChanged(nameof(ValidationMessage));
            }
        }
        public DelegateCommand SignUpCommand { get; }
        public DelegateCommand GotoSignInCommand { get; }

        public SignUpViewModel(Action gotoSignUp)
        {
            SignUpCommand = new DelegateCommand(SignUp, IsSignUpEnabled);
            Email = "em@sc.ss";
            FirstName = "ssc";
            LastName = "sss";
            Login = "scssae";
            Password = "";
            _gotoSignIn = gotoSignUp;
            GotoSignInCommand = new DelegateCommand(gotoSignUp);
            ValidationMessage = "";
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
            return _validationService.LoginPattern.IsMatch(Login) && _validationService.PasswordPattern.IsMatch(Password)
                && _validationService.NamePattern.IsMatch(LastName) && _validationService.NamePattern.IsMatch(FirstName)
                && _validationService.EmailPattern.IsMatch(Email);
        }

        public void ClearSensitiveData()
        {
            _regUser = new RegistrationUser();
            ValidationMessage = "";
        }

        public Task UploadData()
        {
            return null;
        }
        private void ShowValidationMessage()
        {

            if (!_validationService.NamePattern.IsMatch(FirstName))
            {
                ValidationMessage = _validationService.InvalidFirstNameMessage;
            }
            else if (!_validationService.NamePattern.IsMatch(LastName))
            {
                ValidationMessage = _validationService.InvalidLastNameMessage;
            }
            else if (!_validationService.EmailPattern.IsMatch(Email))
            {
                ValidationMessage = _validationService.InvalidEmailMessage;
            }
            else if (!_validationService.LoginPattern.IsMatch(Login))
            {
                ValidationMessage = _validationService.InvalidLoginMessage;
            }
            else if (!_validationService.PasswordPattern.IsMatch(Password))
            {
                ValidationMessage = _validationService.InvalidPasswordMessage;
            }
            else
            {
                ValidationMessage = "";
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
