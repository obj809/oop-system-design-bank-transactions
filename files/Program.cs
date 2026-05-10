namespace SkmProject
{
    public class Program
    {
        public static void Main()
        {
            
            Bank bank = new Bank();

            MenuOption userSelection;

            do
            {
                userSelection = ReadUserOption();
                switch (userSelection)
                {
                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;

                    case MenuOption.Transfer:
                        DoTransfer(bank, bank);
                        break;

                    case MenuOption.NewAccount:
                        DoNewAccount(bank);
                        break;

                    case MenuOption.PrintAccount:
                        DoPrint(bank);
                        break;

                    case MenuOption.PrintTransactions:
                        PrintTransactions(bank);
                        break;
                }
            } while (userSelection != MenuOption.Quit);
        }

        public static MenuOption ReadUserOption()
        {
            int option;
            Console.WriteLine("-----------------");
            Console.WriteLine("1 will withdraw, 2 will deposit, 3 will transfer, 4 will create new account, 5 will print account balance, 6 will print transaction history and 7 will quit");
            Console.WriteLine("-----------------");
            do
            {
                Console.WriteLine("Choose an option [1-7]:");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    option = -1;
                }
            }
            while (option < 1 || option > 7);
            return (MenuOption)(option - 1);
        }

        public enum MenuOption
        {
            Withdraw,
            Deposit,
            Transfer,
            NewAccount,
            PrintAccount,
            PrintTransactions,
            Quit
        }

        private static Account FindAccount(Bank fromBank)
        {
            Console.Write("Enter account name: ");
            string name = Console.ReadLine();
            Account result = fromBank.GetAccount(name);

            if (result == null)
            {
                Console.WriteLine($"No account found with name: {name}");
            }
            return result;
        }

        public static void DoDeposit(Bank toBank)
        {
            decimal input;
            Account toAccount = FindAccount(toBank);
            if (toAccount == null) return;

            Console.WriteLine("-----------------");
            Console.WriteLine("Deposit function activated");
            Console.WriteLine("Enter an amount to deposit:");
            Console.WriteLine("-----------------");
            try
            {
                input = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                input = -1;
            }
            DepositTransaction transaction = new DepositTransaction(toAccount, input);
            toBank.ExecuteTransaction(transaction);
            if (transaction.Success)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("DEPOSIT SUCCESSFUL");
                Console.WriteLine("-----------------");
                transaction.Print();
            }
            else
            {
                Console.WriteLine("Cannot deposit negative amount");
            }
        }

        public static void DoWithdraw(Bank fromBank)
        {
            decimal input;
            Account fromAccount = FindAccount(fromBank);
            if (fromAccount == null) return;
            Console.WriteLine("-----------------");
            Console.WriteLine("Withdraw function activated");
            Console.WriteLine("Enter an amount to withdraw:");
            Console.WriteLine("-----------------");
            try
            {
                input = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                input = -1;
            }
            WithdrawTransaction transaction = new WithdrawTransaction(fromAccount, input);
            fromBank.ExecuteTransaction(transaction);
            if (transaction.Success)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("WITHDRAWAL SUCCESSFUL");
                Console.WriteLine("Executing and printing");
                Console.WriteLine("-----------------");
                transaction.Print();
            }
            else
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("INSUFFICIENT BALANCE");
            }
        }

        public static void DoTransfer(Bank fromBank, Bank toBank)
        {
            decimal input;

            Account fromAccount = FindAccount(fromBank);
            if (fromAccount == null) return;

            Account toAccount = FindAccount(toBank);
            if (toAccount == null) return;

            Console.WriteLine("-----------------");
            Console.WriteLine("Transfer function activated");
            Console.WriteLine("Enter an amount to transfer:");
            Console.WriteLine("-----------------");
            try
            {
                input = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                input = -1;
            }
            TransferTransaction transaction = new TransferTransaction(fromAccount, toAccount, input);
            Console.WriteLine(input);
            Console.WriteLine("TRANSFER ACTIVATED");
            fromBank.ExecuteTransaction(transaction);
            transaction.Print();
        }

        public static void DoNewAccount(Bank bank)
        {
            string name;
            decimal balance;

            Console.WriteLine("-----------------");
            Console.WriteLine("Enter a name for the new account: ");
            name = Console.ReadLine();

            Console.WriteLine("Enter a balance for the new account: ");
            try
            {
                balance = Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid balance. Account not created.");
                return;
            }

            Account account = new Account(name, balance);
            Console.WriteLine("-----------------");
            Console.WriteLine("NEW ACCOUNT CREATED");
            bank.AddAccount(account);
            Console.WriteLine("ACCOUNT ADDED TO BANK");
        }

        public static void DoPrint(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account == null) return;

            Console.WriteLine("Balance printing function activated");
            Console.WriteLine("-----------------");
            account.Print();
            Console.WriteLine("-----------------");
        }

        public static void PrintTransactions(Bank bank)
        {
            Console.WriteLine("Transaction history printing function activated");
            Console.WriteLine("-----------------");
            bank.PrintTransactionHistory();
            Console.WriteLine("-----------------");
        }
    }
}