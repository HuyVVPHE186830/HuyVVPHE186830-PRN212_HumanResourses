using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Objects;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;
        public AccountService()
        {
            iAccountRepository = new AccountRepository();
        }
        public void AddAccount(Account account)
        {
            iAccountRepository.AddAccount(account);
        }
        public void UpdateAccount(Account account)
        {
            iAccountRepository.UpdateAccount(account);
        }
        public void DeleteAccount(Account account)
        {
            iAccountRepository.DeleteAccount(account);
        }
        public List<Account> GetAccounts()
        {
            return iAccountRepository.GetAccounts();
        }
        public Account GetAccountByAccountId(int accountId)
        {
            return iAccountRepository.GetAccountByAccountId(accountId);
        }
    }
}
