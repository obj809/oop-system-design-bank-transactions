namespace SkmProject
{
    public class DepositTransaction : Transaction
    {
        private Account _account;
        private bool _success = false;

        public override bool Success
        {
            get
            { return _success; }
        }

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        public override void Execute()
        {
            base.Execute();
            _success = _account.Deposit(_amount);
        }

        public override void Rollback()
        {
            if (Reversed)
            {
                throw new Exception("Cannot rollback this transaction as it has already been reversed");
            }
            if (!Executed || !_success)
            {
                throw new Exception("Cannot rollback this transaction as it was not successfully executed");
            }

            if (_account.Withdraw(_amount))
            {
                _success = false;
                base.Rollback();
            }
            else
            {
                _success = true;
                throw new Exception("Rollback failed - account refused the reversing withdraw");
            }
        }

        public override void Print()
        {
            Console.WriteLine($"Deposit - Amount: {_amount}, Success: {_success}, Reversed: {Reversed} on {DateStamp}");
        }
    }
}