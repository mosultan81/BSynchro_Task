namespace Transactions_API.Repositories
{
    using System.Collections.Generic;
    using Transactions_API.Models;
    public class TransactionRepository
    {
        // generate list of transactions to be used as a static list to store new transactions in it

        public readonly List<Transaction> _transactions = new List<Transaction>();

        // here i is declared to set an increment id for each new Transaction created on runtime

        private int t = 1;

        // This is the function used to add new transaction for an existing Account of an existing Customer
        public async Task<Transaction> AddNewTransaction(int AccountId, float initialCredit)
        {
            Transaction transaction = new Transaction();
            if (AccountId > 0)
            {
                transaction.AccountId = AccountId;
                transaction.TransactioinId = t;
                transaction.InitialCreditValue = initialCredit;
                transaction.AddedDate = DateTime.Now;
                _transactions.Add(transaction);
                t++;
            }
            return transaction;
        }
    }
}
