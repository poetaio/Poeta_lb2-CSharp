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
    public class SignInViewModel : INotifyPropertyChanged
    {
        private AuthenticationUser _authUser = new AuthenticationUser();
        private Action _gotoSignUp;
        private Action _gotoWallets;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public DelegateCommand SignInCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SignUpCommand { get; }

        public SignInViewModel(Action gotoSignUp, Action gotoWallets)
        {
            SignInCommand = new DelegateCommand(SignIn, IsSignInEnabled);
            CloseCommand = new DelegateCommand(() => Environment.Exit(0));
            _gotoSignUp = gotoSignUp;
            _gotoWallets = gotoWallets;
            SignUpCommand = new DelegateCommand(gotoSignUp);
        }

        private void SignIn()
        {
            if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password))
                MessageBox.Show("Login or password is empty.");
            else
            {
                var authService = new AuthenticationService();
                User user = null;
                try
                {
                    user = authService.Authenticate(_authUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In failed: {ex.Message}");
                    return;
                }
                MessageBox.Show("Sign In was successful");
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

    }
}
