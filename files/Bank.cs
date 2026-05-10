namespace SkmProject
{
    public class Bank
    {
        // Polymorphism example - holding subclass instances
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
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (_accounts[i].Name == name)
                {
                    Console.WriteLine("MATCHING ACCOUNT NAME FOUND");
                    return _accounts[i];
                }
            }
            Console.WriteLine("ACCOUNT NOT FOUND");
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        public void PrintTransactionHistory()
        {
            // Polymorphism example - iterating over subclass instances
            foreach (Transaction transaction in _transactions)
            {
                transaction.Print();
            }
        }
    }
}
