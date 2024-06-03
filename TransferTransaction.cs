using System;
using BankingApp;

namespace BankSystem
{
    public class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _deposit = new DepositTransaction(toAccount, amount);
            _withdraw = new WithdrawTransaction(fromAccount, amount);
        }

        public override void Print()
        {
            base.Print();
            if (_amount > 0 && _success)
                Console.WriteLine($"Transferred {_amount:C} from {_fromAccount.Name}'s account to {_toAccount.Name}'s account");
        }

        public override void Execute()
        {
            base.Execute();

            if (_amount > 0)
            {
                _withdraw.Execute();
                if (_withdraw.Success)
                {
                    _deposit.Execute();
                    if (_deposit.Success)
                    {
                        _success = true;
                    }
                }
            }
            else
            {
                _success = false;
            }
        }

        public override void Rollback()
        {
            if (_success)
            {
                _deposit.Rollback();
                _withdraw.Rollback();
            }
            else
            {
                throw new InvalidOperationException("Cannot reverse a failed transaction.");
            }

            base.Rollback();
        }
    }
}