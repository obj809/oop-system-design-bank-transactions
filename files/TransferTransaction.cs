namespace SkmProject
{
    // Inheritance example (Base class after class name)
    public class TransferTransaction : Transaction
    {
        private Account _toAccount;
        private Account _fromAccount;
        private DepositTransaction _theDeposit;
        private WithdrawTransaction _theWithdraw;

        public override bool Success
        {
            get
            {
                return _theDeposit.Success && _theWithdraw.Success;
            }
        }

        // Inheritance example (base(arg) after constructor)
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _theDeposit = new DepositTransaction(_toAccount, _amount);
            _theWithdraw = new WithdrawTransaction(_fromAccount, _amount);
        }

        // Polymorphism example (override in child class)
        public override void Execute()
        {
            base.Execute();
            _theWithdraw.Execute();
            if (_theWithdraw.Success)
            {
                _theDeposit.Execute();
                if (!_theDeposit.Success)
                {
                    _theWithdraw.Rollback();
                }
            }
        }
        
        public override void Rollback()
        {
            if (_theWithdraw.Success && !_theWithdraw.Reversed)
            {
                _theWithdraw.Rollback();
            }

            if (_theDeposit.Success && !_theDeposit.Reversed)
            {
                _theDeposit.Rollback();
            }
            base.Rollback();
        }

        public override void Print()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("THE TRANSFER");
            Console.WriteLine($"Transferred ${_amount} from {_fromAccount.Name} Account to {_toAccount.Name} Account on {DateStamp}");
            Console.WriteLine("    ");
        }
    }
}
