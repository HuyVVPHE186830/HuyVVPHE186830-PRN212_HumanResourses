using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public void AddAccount(Account account) => AccountDAO.AddAccount(account);
        public void UpdateAccount(Account account) => AccountDAO.UpdateAccount(account);
        public void DeleteAccount(Account account) => AccountDAO.DeleteAccount(account); 
        public List<Account> GetAccounts() => AccountDAO.GetAccounts();
        public Account GetAccountByAccountId(int accountId) => AccountDAO.GetAccountByAccountId(accountId);
    }
}
