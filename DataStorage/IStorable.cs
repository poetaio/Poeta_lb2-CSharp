using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallets.DataStorage
{
    public interface IStorable
    {
        public Guid Guid { get; }
    }
}
