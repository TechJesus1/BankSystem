using System;

namespace BankingApp
{
    public abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        protected bool _executed;
        protected bool _reversed;
        protected DateTime _dateStamp;

        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        public bool Success => _success;
        public bool Executed => _executed;
        public bool Reversed => _reversed;
        public DateTime DateStamp => _dateStamp;

        public virtual void Print()
        {
            Console.WriteLine($"Transaction Amount: {_amount:C}");
            Console.WriteLine($"Transaction Status: {(_success ? "Successful" : "Failed")}");
            Console.WriteLine($"Transaction Date: {_dateStamp}");
        }

        public virtual void Execute()
        {
            _executed = true;
            _dateStamp = DateTime.Now;
        }

        public virtual void Rollback()
        {
            if (!_executed)
            {
                throw new InvalidOperationException("Transaction has not been executed.");
            }

            if (_reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }

            _reversed = true;
            _dateStamp = DateTime.Now;
        }
    }
}