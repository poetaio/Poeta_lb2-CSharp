using WalletsWPF.Authentication;
using WalletsWPF.Wallets;
using WalletsWPF.Navigation;
using Wallets.DataStorage;
using System.Threading.Tasks;
using Wallets.BusinessLayer.Users;
using System;

namespace WalletsWPF
{
    class MainViewModel : NavigationBase<MainNavigatableTypes>, IUploadable
    {
        public User CurrentUser { get; set; }
        public MainViewModel()
        {
            CurrentUser = new User(Guid.Empty);
            Navigate(MainNavigatableTypes.Auth);
        }

        protected override INavigatable<MainNavigatableTypes> CreateViewModel(MainNavigatableTypes type)
        {
            switch (type)
            {
                case MainNavigatableTypes.Auth:
                    return new AuthViewModel(() => Navigate(MainNavigatableTypes.Wallets), CurrentUser);
                case MainNavigatableTypes.Wallets:
                    return new WalletsViewModel(CurrentUser);
                default:
                    return new WalletsViewModel(CurrentUser);
            }
        }

        public async Task UploadData()
        {
            await CurrentViewModel.UploadData();
        }
    }
}
