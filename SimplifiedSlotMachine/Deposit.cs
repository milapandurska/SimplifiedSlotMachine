namespace SimplifiedSlotMachine
{
    public class Deposit
    {
        private decimal _deposit;
        public decimal CurrentBalance
        {
            get { return _deposit; }
        }
        public Deposit(decimal initialBalance)
        {
            _deposit = initialBalance;
        }

        public void CalculateBalance(decimal stake, decimal coefficient)
        {
            _deposit = _deposit - stake + coefficient * stake;
        }
    }
}
