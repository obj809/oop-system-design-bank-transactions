namespace SkmProject
{
    // Abstraction example (abstract class)
    public abstract class Transaction
    {
        // Inheritance example (protected field)
        protected decimal _amount;
        private bool _executed = false;
        private bool _reversed = false;
        private DateTime _dateStamp;

        public bool Executed
        {
            get { return _executed; }
        }

        public bool Reversed
        {
            get { return _reversed; }
        }

        public DateTime DateStamp
        {
            get { return _dateStamp; }
        }

        // Abstraction example (abstract readonly property)
        public abstract bool Success { get; }

        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        // Inheritance example (virtual method in parent)
        public virtual void Execute()
        {
            if (_executed == true)
            {
                throw new Exception("Already Executed!");
            }
            if (_reversed)
            {
                throw new Exception("Cannot re-execute a reversed transaction");
            }
            _executed = true;
            _dateStamp = DateTime.Now;
        }

        public virtual void Rollback()
        {
            if (_reversed == true)
            {
                throw new Exception("Rollback has already been performed!");
            }
            if (!_executed)
            {
                throw new Exception("This transaction has not been executed");
            }

            _reversed = true;
            _executed = false;
            _dateStamp = DateTime.Now;
        }

        // Abstraction example (abstract method - no implementation)
        public abstract void Print();
        
    }
}