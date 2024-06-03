using System;
using BankSystem;

namespace BankingApp
{
    public class BankSystem
    {
        public enum MenuOption
        {
            Withdraw = 1,
            Deposit,
            Print,
            Transfer,
            AddAccount,
            PrintTransactionHistory,
            RollbackTransaction,
            Quit
        }

        public static MenuOption ReadUserOption()
        {
            int choice;
            do
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Withdraw");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Print");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Add new account");
                Console.WriteLine("6. Print transaction history");
                Console.WriteLine("7. Rollback transaction");
                Console.WriteLine("8. Quit");

                if (int.TryParse(Console.ReadLine(), out choice) &&
                    Enum.IsDefined(typeof(MenuOption), choice))
                {
                    return (MenuOption)choice;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            } while (true);
        }

        public static void DoDeposit(Bank bank)
        {
            var account = FindAccount(bank);
            if (account != null)
            {
                Console.Write("Enter deposit amount: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                {
                    var depositTransaction = new DepositTransaction(account, depositAmount);
                    bank.ExecuteTransaction(depositTransaction);
                    depositTransaction.Print();
                }
                else
                {
                    Console.WriteLine("Invalid amount entered.");
                }
            }
        }

        public static void DoWithdraw(Bank bank)
        {
            var account = FindAccount(bank);
            if (account != null)
            {
                Console.Write("Enter withdrawal amount: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                {
                    var withdrawTransaction = new WithdrawTransaction(account, withdrawAmount);
                    bank.ExecuteTransaction(withdrawTransaction);
                    withdrawTransaction.Print();
                }
                else
                {
                    Console.WriteLine("Invalid amount entered.");
                }
            }
        }

        public static void DoPrint(Bank bank)
        {
            var account = FindAccount(bank);
            if (account != null)
            {
                account.Print();
            }
        }

        public static void DoTransfer(Bank bank)
        {
            Console.Write("Enter the name of the account to transfer from: ");
            var fromAccount = FindAccount(bank);
            if (fromAccount != null)
            {
                Console.Write("Enter the name of the account to transfer to: ");
                var toAccount = FindAccount(bank);
                if (toAccount != null)
                {
                    Console.Write("Enter transfer amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal transferAmount))
                    {
                        var transferTransaction = new TransferTransaction(fromAccount, toAccount, transferAmount);
                        bank.ExecuteTransaction(transferTransaction);
                        transferTransaction.Print();
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount entered.");
                    }
                }
            }
        }

        public static void AddNewAccount(Bank bank)
        {
            Console.Write("Enter account holder's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter starting balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal balance))
            {
                if (balance >= 0)
                {
                    var account = new Account(name, balance);
                    bank.AddAccount(account);
                    Console.WriteLine("Account created successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid balance entered. Balance cannot be negative.");
                }
            }
            else
            {
                Console.WriteLine("Invalid balance entered.");
            }
        }

        public static void PrintTransactionHistory(Bank bank)
        {
            bank.PrintTransactionHistory();

            Console.Write("Enter the index of the transaction to rollback (or 0 to go back): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= bank.Transactions.Count)
            {
                DoRollback(bank, bank.Transactions[index - 1]);
            }
        }

        public static void DoRollback(Bank bank, Transaction transaction)
        {
            try
            {
                bank.RollbackTransaction(transaction);
                Console.WriteLine("Transaction rolled back successfully.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static Account FindAccount(Bank bank)
        {
            Console.Write("Enter account name: ");
            string name = Console.ReadLine();
            var account = bank.GetAccount(name);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
            }
            return account;
        }

        public static void Main(string[] args)
        {
            Bank bank = new Bank();

            while (true)
            {
                MenuOption option = ReadUserOption();

                switch (option)
                {
                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;

                    case MenuOption.Print:
                        DoPrint(bank);
                        break;

                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;

                    case MenuOption.AddAccount:
                        AddNewAccount(bank);
                        break;

                    case MenuOption.PrintTransactionHistory:
                        PrintTransactionHistory(bank);
                        break;

                    case MenuOption.RollbackTransaction:
                        PrintTransactionHistory(bank);
                        break;

                    case MenuOption.Quit:
                        Console.WriteLine("Exiting...");
                        return;
                }
            }
        }
    }
}