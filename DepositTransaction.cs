using System;
using BankingApp;

public class DepositTransaction : Transaction
{
    private Account _account;

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Account Holder: {_account.Name}");
        if (_amount > 0)
            Console.WriteLine($"Amount Deposited: {_amount:C}");
    }

    public override void Execute()
    {
        base.Execute();

        if (_amount > 0 && _account.Deposit(_amount))
        {
            _success = true;
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
            _account.Withdraw(_amount);
        }
        else
        {
            throw new InvalidOperationException("Cannot reverse a failed transaction.");
        }

        base.Rollback();
    }
}