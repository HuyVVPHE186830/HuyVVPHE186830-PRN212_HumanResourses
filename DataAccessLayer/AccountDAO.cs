using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class AccountDAO
    {
        private static List<Account> listAccounts;
        public static List<Account> GetAccounts()
        {
            listAccounts = new List<Account>();
            try
            {
                using var db = new FuhrmContext();
                listAccounts = db.Accounts.ToList();
            }
            catch (Exception e) { }
            return listAccounts;
        }
        public static void AddAccount(Account account)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Accounts.Add(account);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateAccount(Account account)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Entry<Account>(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeteleAccount(Account account)
        {
            try
            {
                using var db = new FuhrmContext();
                var b1 = db.Accounts.SingleOrDefault(b => b.AccountId == account.AccountId);
                db.Accounts.Remove(b1);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Account GetAccountByBookingId(int accountId)
        {
            using var db = new FuhrmContext();
            return db.Accounts.FirstOrDefault(b => b.AccountId.Equals(accountId));
        }

    }
}
