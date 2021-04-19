using System.Threading.Tasks;

namespace Wallets.DataStorage
{
    public interface IUploadable
    {
        public Task UploadData();
    }
}
