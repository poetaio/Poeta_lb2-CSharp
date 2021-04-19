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
        public SignUpView()
        {
            InitializeComponent();
        }

        private void TbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignUpViewModel)DataContext).Password = TbPassword.Password;
        }
    }

}
