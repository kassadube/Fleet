using Fleet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Data
{
    public interface IAccountRepository
    {
        BaseResultInfo GetAccountDetails(int accountId);
    }
}
