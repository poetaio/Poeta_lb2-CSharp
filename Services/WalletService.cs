using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets.BusinessLayer;
using Wallets.BusinessLayer.Users;
using Wallets.DataStorage;

namespace Wallets.Services
{
    public class WalletService : IUploadable
    {
        private FileDataStorage<Wallet> _walletsStorageService;
        private FileDataStorage<DBUser> _userStorageService;
        private FileDataStorage<Category> _categoriesStorageService;
        public User CurrentUser { get; set; }

        public WalletService(User currentUser)
        {
            CurrentUser = currentUser;
            _walletsStorageService = new FileDataStorage<Wallet>();
            _userStorageService = new FileDataStorage<DBUser>();
            _categoriesStorageService = new FileDataStorage<Category>();
        }

        public async Task DownloadData()
        {
            var dbUser = await _userStorageService.GetAsync(CurrentUser.Guid);

            CurrentUser.Wallets = await _walletsStorageService.GetAllFromGuids(dbUser.WalletsGuids);
            CurrentUser.SharedWallets = await _walletsStorageService.GetAllFromGuids(dbUser.SharedWalletsGuids);
            CurrentUser.Categories = await _categoriesStorageService.GetAllFromGuids(dbUser.CategoriesGuids);
        }

        public async Task UploadData()
        {
            await _walletsStorageService.AddOrUpdateAllAsync(CurrentUser.Wallets);
            await _walletsStorageService.AddOrUpdateAllAsync(CurrentUser.SharedWallets);
            await _categoriesStorageService.AddOrUpdateAllAsync(CurrentUser.Categories);
            
            var dbUser = await _userStorageService.GetAsync(CurrentUser.Guid);

            dbUser.WalletsGuids = CurrentUser.Wallets.Select(x => x.Guid).ToList();
            dbUser.SharedWalletsGuids = CurrentUser.SharedWallets.Select(x => x.Guid).ToList();
            dbUser.CategoriesGuids = CurrentUser.Categories.Select(x => x.Guid).ToList();

            await _userStorageService.AddOrUpdateAsync(dbUser);
        }
    }
}
