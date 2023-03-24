namespace SimplifiedSlotMachine
{
    public class Game
    {
        private readonly int _gameColumnLength;
        private readonly int _gameRowLength;
        private readonly List<Symbol> _symbols;
        public Game(int gameColumnLength, int gameRowLength, List<Symbol> symbols)
        {
            _gameColumnLength = gameColumnLength;
            _gameRowLength = gameRowLength;
            _symbols = symbols;
        }

        public List<Symbol> GenerateArray()
        {
            var random = new Random();
            List<Symbol> symbolsInRow = new List<Symbol>();

            for (int i = 0; i < _gameColumnLength; i++)
            {
                var temp = random.NextDouble();
                double sum = 0;
                foreach (var item in _symbols)
                {
                    sum += item.Probability;
                    if (sum > temp)
                    {
                        symbolsInRow.Add(item);
                        break;
                    }
                }
            }

            return symbolsInRow;
        }

        public decimal GetCoefficientForRow(List<Symbol> symbolsInRow)
        {
            decimal currentCoef = 0;
            var allSymbols = symbolsInRow.Select(s => s.Key).ToList();
            var symbolsWithoutWildcard = symbolsInRow.Where(s => s.Key != "*").Select(s => s.Key).ToList();

            if (allSymbols.TrueForAll(s => s.Equals(allSymbols.First()))
                || allSymbols.Any(s => s == "*")
                    && symbolsWithoutWildcard.TrueForAll(s => s.Equals(symbolsWithoutWildcard.First()))
                )
            {
                currentCoef += symbolsInRow.Sum(s => s.Coefficient);
            }
            return currentCoef;
        }
    }
}
