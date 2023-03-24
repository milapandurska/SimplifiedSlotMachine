namespace SimplifiedSlotMachine
{
    public static class InputHelper
    {
        public static decimal AddAmount(string message, decimal maxValue = 0)
        {
            decimal amount;
            do
            {
                Console.WriteLine(message);
                decimal.TryParse(Console.ReadLine(), out amount);
                if (maxValue > 0 && amount > maxValue)
                {
                    Console.WriteLine("Your stake cannot be greater than your balance!");
                }
            }
            while (amount <= 0 || (maxValue > 0 && amount > maxValue));
            return amount;
        }
    }
}
