using System;
using BankingApp;

public class WithdrawTransaction : Transaction
{
    private Account _account;

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Account Holder: {_account.Name}");
        if (_amount > 0 && _success)
            Console.WriteLine($"Amount Withdrawn: {_amount:C}");
    }

    public override void Execute()
    {
        base.Execute();

        if (_amount > 0 && _account.Withdraw(_amount))
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
            _account.Deposit(_amount);
        }
        else
        {
            throw new InvalidOperationException("Cannot reverse a failed transaction.");
        }

        base.Rollback();
    }
}