using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Wallets.Services;
using WalletsWPF.Navigation;
using Wallets.BusinessLayer.Users;
using WalletsWPF.Wallets;
using Nito.AsyncEx;
using Prism.Commands;
using Wallets.BusinessLayer;
using System;

namespace WalletsWPF.Wallets
{
    public class WalletsViewModel : BindableBase, INavigatable<MainNavigatableTypes>
    {
        public User CurrentUser { get; private set; }
        private bool _interfaceEnabled;
        public bool InterfaceEnabled
        {
            get
            {
                return _interfaceEnabled;
            }
            set
            {
                _interfaceEnabled = value;
                RaisePropertyChanged(nameof(InterfaceEnabled));
            }
        }
        private WalletService _service;
        private WalletsDetailsViewModel _currentWallet;
        public UserInfoViewModel UserInfo { get; set; }
        public DelegateCommand AddWalletCommand { get; set; }
        public WalletsDetailsViewModel CurrentWallet
        {
            get
            {
                return _currentWallet;
            }
            set
            {
                _currentWallet = value;
                RaisePropertyChanged(nameof(CurrentWallet));
            }
        }
        public ObservableCollection<WalletsDetailsViewModel> Wallets 
        {
            get;
            set; 
        }
        public WalletsViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            UserInfo = new UserInfoViewModel(CurrentUser);
            _service = new WalletService(CurrentUser);
            AddWalletCommand = new DelegateCommand(AddWallet);
            _interfaceEnabled = true;

            AsyncContext.Run(_service.DownloadData);
            Wallets = new ObservableCollection<WalletsDetailsViewModel>(
            CurrentUser.Wallets
                .Select(x => new WalletsDetailsViewModel(x, RemoveWallet, InterfaceEnable))
                .ToList()
            );
        }
        public void AddWallet()
        {
            CurrentUser.Wallets.Add(new Wallet());
            Wallets.Add(new WalletsDetailsViewModel(CurrentUser.Wallets.Last(), RemoveWallet, InterfaceEnable));
            RaisePropertyChanged(nameof(Wallets));
        }

        public void RemoveWallet(Guid walletGuid)
        {
            CurrentUser.Wallets.RemoveAll(x => x.Guid == walletGuid);
            foreach (WalletsDetailsViewModel walletDetails in Wallets)
            {
                if (walletDetails.Guid == walletGuid)
                {
                    Wallets.Remove(walletDetails);
                    break;
                }
            }
            RaisePropertyChanged(nameof(Wallets));
        }

        public void InterfaceEnable(bool enabled)
        {
            InterfaceEnabled = enabled;
            RaisePropertyChanged(nameof(InterfaceEnabled));
        }

        public MainNavigatableTypes Type => MainNavigatableTypes.Wallets;

        public void ClearSensitiveData()
        {
            
        }

        public async Task UploadData()
        {
            await _service.UploadData();
        }
    }
}
