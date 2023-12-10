namespace Accounts_API.Repositories
{
    using System.Collections.Generic;
    using Accounts_API.Models;
    public class AccountRepository
    {
        // Declare new static list of accounts to store created accounts in it

        public readonly List<Account> _accounts = new List<Account>();

        // here i is declared to set an increment id for each new account created on runtime
        private int i = 1;

        // This is the function used to open new account for an existing Customer
        public async Task<Account> OpenNewAccount(int customerId)
        {
            Account account = new Account();
            if (customerId > 0) 
            {
                account.AccountId = i;
                account.CustomerId = customerId;
                account.Balance = 0;
                account.AddedDate = DateTime.Now;
                _accounts.Add(account);
                i++;
               
            }
              return account;
        }
    }
}
