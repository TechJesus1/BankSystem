using System;

namespace BankingApp
{
    public class Account
    {
        private decimal _balance;
        private string _name;

        public Account(string name, decimal balance)
        {
            _name = name;
            _balance = balance;
        }

        public bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                return true;
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
                return false;
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= _balance)
            {
                _balance -= amount;
                return true;
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount or insufficient balance.");
                return false;
            }
        }

        public void Print()
        {
            Console.WriteLine($"Account Holder: {_name}");
            Console.WriteLine($"Balance: {_balance:C}");
        }

        public string Name => _name;
    }
}
