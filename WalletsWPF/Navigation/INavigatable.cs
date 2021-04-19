using System;
using Wallets.DataStorage;

namespace WalletsWPF.Navigation
{
    public interface INavigatable<TObject> : IUploadable where TObject : Enum
    {
        public TObject Type { get; }
        public void ClearSensitiveData();
    }
}