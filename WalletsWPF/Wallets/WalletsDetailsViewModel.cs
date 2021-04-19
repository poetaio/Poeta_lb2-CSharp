using Prism.Commands;
using Prism.Mvvm;
using System;
using Wallets.BusinessLayer;
using Wallets.Services;


namespace WalletsWPF.Wallets
{
    public class WalletsDetailsViewModel : BindableBase
    {
        private Wallet _wallet;
        private ValidationService _validationService;
        private bool _removeButtonEnabled;
        private bool _nameEnabled;
        private bool _descriptionEnabled;
        private bool _currencyEnabled;
        private string _validationMessage;
        public string ValidationMessage 
        { 
            get => _validationMessage;
            set
            {
                _validationMessage = value;
                RaisePropertyChanged(nameof(ValidationMessage));
            }
        }
        public DelegateCommand RemoveWalletCommand { get; set; }
        public Action<bool> EnableInterface { get; }

        public bool NameEnabled
        {
            get => _nameEnabled;
            set
            {
                _nameEnabled = value;
                RaisePropertyChanged(nameof(NameEnabled));
            }
        }
        public bool CurrencyEnabled
        {
            get => _currencyEnabled;
            set
            {
                _currencyEnabled = value;
                RaisePropertyChanged(nameof(CurrencyEnabled));
            }
        }
        public bool DescriptionEnabled
        {
            get => _descriptionEnabled;
            set
            {
                _descriptionEnabled = value;
                RaisePropertyChanged(nameof(DescriptionEnabled));
            }
        }
        public Guid Guid
        {
            get
            {
                return _wallet.Guid;
            }
        }
        public string Name 
        { 
            get
            {
                return _wallet.Name;
            }
            set
            {
                if (_validationService.NamePattern.IsMatch(value))
                {
                    ValidationMessage = "";
                    EnableInterface.Invoke(true);
                    DescriptionEnabled = true;
                    CurrencyEnabled = true;
                    RemoveButtonEnabled = true;
                }
                else
                {
                    ValidationMessage = _validationService.InvalidNameMessage;
                    EnableInterface.Invoke(false);
                    DescriptionEnabled = false;
                    CurrencyEnabled = false;
                    RemoveButtonEnabled = false;
                }
                _wallet.Name = value;
                RaisePropertyChanged(nameof(FullName));
            }
        }
        public string Description
        {
            get
            {
                return _wallet.Description;
            }
            set
            {
                _wallet.Description = value;
                RaisePropertyChanged(nameof(FullName));
            }
        }
        public string Currency
        {
            get
            {
                return _wallet.Currency;
            }
            set
            {
                if (_validationService.CurrencyPattern.IsMatch(value))
                {
                    ValidationMessage = "";
                    DescriptionEnabled = true;
                    NameEnabled = true;
                    RemoveButtonEnabled = true;
                    EnableInterface.Invoke(true);
                }
                else
                {
                    ValidationMessage = _validationService.InvalidCurrencyMessage;
                    EnableInterface.Invoke(false);
                    DescriptionEnabled = false;
                    NameEnabled = false;
                    RemoveButtonEnabled = false;
                }
                _wallet.Currency = value;
                RaisePropertyChanged(nameof(FullName));
            }
        }
        public string Balance
        {
            get
            {
                return _wallet.Balance.ToString();
            }
        }
        public string Income
        {
            get
            {
                return _wallet.GetLastMonthIncome().ToString();
            }
        }
        public string Expanses
        {
            get
            {
                return _wallet.GetLastMonthExpenses().ToString();
            }
        }
        public string FullName
        {
            get
            {
                return $"{Name} ${Balance}";
            }
        }

        public bool RemoveButtonEnabled 
        { 
            get => _removeButtonEnabled; 
            set 
            { 
                _removeButtonEnabled = value; 
                RaisePropertyChanged(nameof(RemoveButtonEnabled)); 
            } 
        }

        public WalletsDetailsViewModel(Wallet wallet, Action<Guid> removeWalletAction, Action<bool> enableInterface)
        {
            _wallet = wallet;
            RemoveWalletCommand = new DelegateCommand(() => removeWalletAction(Guid));
            EnableInterface = enableInterface;
            _removeButtonEnabled = true;
            _validationService = new ValidationService();
            NameEnabled = true;
            DescriptionEnabled = true;
            CurrencyEnabled = true;
        }
    }
}
