using WalletsWPF.Authentication;
using WalletsWPF.Wallets;
using System.Windows.Controls;

namespace WalletsWPF
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            Content = new SignInView(GotoSignUp, GotoWallets);
        }

        public void GotoSignUp()
        {
            Content = new SignUpView(GotoSignIn);
        }

        public void GotoSignIn()
        {
            Content = new SignInView(GotoSignUp, GotoWallets);
        }
        public void GotoWallets()
        {
            Content = new WalletsView();
        }
    }
}
