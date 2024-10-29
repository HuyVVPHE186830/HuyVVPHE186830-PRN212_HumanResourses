using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;

namespace Repositories
{
    public interface IAccountRepository
    {
        void AddAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
        List<Account> GetAccounts();

        Account GetAccountByAccountId(int account);
    }
}
