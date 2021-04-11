using System;
using System.Windows;
using System.Windows.Controls;

namespace WalletsWPF.Authentication
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        private SignUpViewModel _viewModel;
        public SignUpView(Action gotoSignIn)
        {
            InitializeComponent();
            _viewModel = new SignUpViewModel(gotoSignIn);
            this.DataContext = _viewModel;
        }

        private void TbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = TbPassword.Password;
        }
    }

}
