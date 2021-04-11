using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wallets.Models;
using Wallets.Services;

namespace WalletsWPF.Authentication
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private RegistrationUser _regUser = new RegistrationUser();

        private Action _gotoSignIn;

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void SignUp()
        {
            var authService = new AuthenticationService();

            try
            {
                authService.RegisterUser(_regUser);
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
            return !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password) && !String.IsNullOrWhiteSpace(LastName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
