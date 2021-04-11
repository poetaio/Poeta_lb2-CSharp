using System;
using System.Windows;
using System.Windows.Controls;

namespace WalletsWPF.Authentication
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        private SignInViewModel _viewModel;
        public SignInView(Action gotoSignUp, Action gotoWallets)
        {
            InitializeComponent();
            _viewModel = new SignInViewModel(gotoSignUp, gotoWallets);
            this.DataContext = _viewModel;
        }

        private void TbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = TbPassword.Password;
        }
    }

}
