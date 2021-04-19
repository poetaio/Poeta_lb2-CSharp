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
        private ObservableCollection<WalletsDetailsViewModel> _wallets;
        private bool _emptyMessageVisible;

        public UserInfoViewModel UserInfo { get; set; }
        public DelegateCommand AddWalletCommand { get; set; }
        public MainNavigatableTypes Type => MainNavigatableTypes.Wallets;
        public bool EmptyMessageVisible 
        {
            get => _emptyMessageVisible; 
            set
            {
                _emptyMessageVisible = value;
                RaisePropertyChanged(nameof(EmptyMessageVisible));
            }
        }
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
            get => _wallets;
            set
            {
                _wallets = value;
                RaisePropertyChanged(nameof(Wallets));
            }
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
            EmptyMessageVisible = _wallets.Count == 0;
        }
        public void AddWallet()
        {
            CurrentUser.Wallets.Add(new Wallet());
            EmptyMessageVisible = false;
            Wallets.Add(new WalletsDetailsViewModel(CurrentUser.Wallets.Last(), RemoveWallet, InterfaceEnable));
            RaisePropertyChanged(nameof(Wallets));
        }

        public void RemoveWallet(Guid walletGuid)
        {
            CurrentUser.Wallets.RemoveAll(x => x.Guid == walletGuid);
            for (int i = 0; i < Wallets.Count; i++)
            {
                if (Wallets[i].Guid == walletGuid)
                {
                    Wallets.RemoveAt(i);
                    if (i > 0)
                        CurrentWallet = Wallets[i - 1];
                    else if (Wallets.Count() > 0)
                        CurrentWallet = Wallets[0];
                    break;
                }
            }
            EmptyMessageVisible = _wallets.Count == 0;
            RaisePropertyChanged(nameof(Wallets));
        }

        public void InterfaceEnable(bool enabled)
        {
            InterfaceEnabled = enabled;
            RaisePropertyChanged(nameof(InterfaceEnabled));
        }

        public void ClearSensitiveData()
        {
            
        }

        public async Task UploadData()
        {
            await _service.UploadData();
        }
    }
}
