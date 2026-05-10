namespace SkmProject
{
    public class WithdrawTransaction : Transaction
    {
        private Account _account;
        private bool _success = false;

        public override bool Success
        {
            get
            { return _success; }
        }

        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        public override void Execute()
        {
            // Inheritance example (base.Method() call)
            base.Execute();
            _success = _account.Withdraw(_amount);
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

            if (_account.Deposit(_amount))
            {
                _success = false;
                base.Rollback();
            }
            else
            {
                _success = true;
                throw new Exception("Rollback failed - account refused the reversing deposit");
            }
        }

        public override void Print()
        {
            Console.WriteLine($"Withdrawal - Amount: {_amount}, Success: {_success}, Reversed: {Reversed} on {DateStamp}");
        }
    }
}
