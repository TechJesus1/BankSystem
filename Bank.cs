using System;
using System.Collections.Generic;

namespace BankingApp
{
    public class Bank
    {
        private List<Account> _accounts;
        private List<Transaction> _transactions;

        public Bank()
        {
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public Account GetAccount(string name)
        {
            foreach (var account in _accounts)
            {
                if (account.Name == name)
                {
                    return account;
                }
            }
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        public void RollbackTransaction(Transaction transaction)
        {
            if (transaction.Executed && !transaction.Reversed)
            {
                transaction.Rollback();
            }
            else
            {
                throw new InvalidOperationException("Cannot rollback this transaction.");
            }
        }

        public void PrintTransactionHistory()
        {
            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ");
                _transactions[i].Print();
                Console.WriteLine();
            }
        }

        public List<Transaction> Transactions => _transactions;
    }
}