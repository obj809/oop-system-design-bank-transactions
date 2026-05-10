namespace SkmProject
{
    public class Account
    {
        // Encapsulation example (private fields)
        private decimal _balance;
        private string _name;

        public Account(string name, decimal startingBalance)
        {
            _name = name;
            _balance = startingBalance;
        }

        // Encapsulation example (invariants - guard inside property)
        public bool Deposit(decimal amountToDeposit)
        {
            if (amountToDeposit > 0)
            {
                _balance += amountToDeposit;
                return true;
            }
            return false;
        }

        public bool Withdraw(decimal amountToWithdraw)
        {
            if (amountToWithdraw <= _balance && amountToWithdraw > 0)
            {
                _balance -= amountToWithdraw;
                return true;
            }
            return false;
        }
        // Encapsulation example (public getter)
        public string Name
        {
            get { return _name; }
        }
        
        public void Print()
        {
            Console.WriteLine(_name);
            Console.WriteLine(_balance);
        }
    }
}
