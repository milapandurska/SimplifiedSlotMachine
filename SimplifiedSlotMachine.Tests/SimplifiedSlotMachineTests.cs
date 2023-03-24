using SimplifiedSlotMachine;
namespace SimplifiedSlotMachine.Tests
{
    public class SimplifiedSlotMachineTests
    {
        private List<Symbol> symbols = new List<Symbol>
{
new Symbol(){ Key="A", Coefficient=0.4m, Probability=0.45 },
new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35},
new Symbol(){ Key="P", Coefficient=0.8m, Probability=0.15},
new Symbol(){ Key="*", Coefficient=0, Probability=0.05},
};
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GenerateArray_WithLength3_ReturnTrue()
        {
            int gameColumnLength = 3;
            int gameRowLength = 4;
            Game game = new Game(gameColumnLength, gameRowLength, symbols);
            var result = game.GenerateArray();

            Assert.That(gameColumnLength == result.Count());
        }

        [Test]
        public void GenerateArray_WithLength3_ReturnFalse()
        {
            int gameColumnLength = 3;
            int gameRowLength = 4;
            Game game = new Game(gameColumnLength, gameRowLength, symbols);
            var result = game.GenerateArray();

            Assert.That(4 != result.Count());
        }

        [Test]
        public void Deposit_CalculateBalance_ReduceCurrentBalanceWithStakeAmount_ReturnTrue()
        {
            decimal currentBalance = 50;
            decimal stake = 20;
            Deposit deposit = new Deposit(50);
            deposit.CalculateBalance(stake, 0);
            Assert.That(currentBalance - stake == deposit.CurrentBalance);
        }

        [Test]
        public void Deposit_CalculateBalance_CalculateWinWithTheSameSymbols_ReturnTrue()
        {
            Game game = new Game(3, 4, symbols);

            List<Symbol> symbolsInRow = new List<Symbol>()
            {
            new Symbol(){ Key="A", Coefficient=0.4m, Probability=0.45 },
            new Symbol(){ Key="A", Coefficient=0.4m, Probability=0.45 },
            new Symbol(){ Key="A", Coefficient=0.4m, Probability=0.45 },
            };

            var coefficient = game.GetCoefficientForRow(symbolsInRow);

            decimal currentBalance = 50;
            decimal stake = 20;
            Deposit deposit = new Deposit(currentBalance);

            deposit.CalculateBalance(stake, coefficient);

            Assert.That(currentBalance - stake + stake * coefficient == deposit.CurrentBalance);
        }

        [Test]
        public void Game_GetCoefficientForRow_WinWithWildcardAtFirstPosition()
        {
            Game game = new Game(3, 4, symbols);

            List<Symbol> symbolsInRow = new List<Symbol>()
            {
            new Symbol(){ Key="*", Coefficient=0, Probability=0.05},
            new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35},
            new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35 },
            };

            var coefficient = game.GetCoefficientForRow(symbolsInRow);

            Assert.That(1.2m == coefficient);
        }

        [Test]
        public void Game_GetCoefficientForRow_WinWithWildcardInTheMiddle()
        {
            Game game = new Game(3, 4, symbols);

            List<Symbol> symbolsInRow = new List<Symbol>()
            {
            new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35},
            new Symbol(){ Key="*", Coefficient=0, Probability=0.05},
            new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35 },
            };

            var coefficient = game.GetCoefficientForRow(symbolsInRow);

            Assert.That(1.2m == coefficient);
        }

        [Test]
        public void Game_GetCoefficientForRow_WinWithWildcardInLastPosition()
        {
            Game game = new Game(3, 4, symbols);

            List<Symbol> symbolsInRow = new List<Symbol>()
            {
            new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35},
            new Symbol(){ Key="B", Coefficient=0, Probability=0.05},
            new Symbol(){ Key="*", Coefficient=0.6m, Probability=0.35 },
            };

            var coefficient = game.GetCoefficientForRow(symbolsInRow);

            Assert.That(1.2m == coefficient);
        }

        [Test]
        public void Deposit_CalculateBalance_CalculateWinWithWildcard_ReturnTrue()
        {
            Game game = new Game(3, 4, symbols);

            List<Symbol> symbolsInRow = new List<Symbol>()
            {
            new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35},
            new Symbol(){ Key="*", Coefficient=0, Probability=0.05},
            new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35},
            };

            var coefficient = game.GetCoefficientForRow(symbolsInRow);

            decimal currentBalance = 50;
            decimal stake = 20;
            Deposit deposit = new Deposit(currentBalance);

            deposit.CalculateBalance(stake, coefficient);

            Assert.That(currentBalance - stake + stake * coefficient == deposit.CurrentBalance);
        }

        [Test]
        public void Deposit_CalculateBalance_CalculateLoss_ReturnTrue()
        {
            Game game = new Game(3, 4, symbols);

            List<Symbol> symbolsInRow = new List<Symbol>()
            {
            new Symbol(){ Key="B", Coefficient=0.6m, Probability=0.35},
            new Symbol(){ Key="*", Coefficient=0, Probability=0.05},
            new Symbol(){ Key="A", Coefficient=0.4m, Probability=0.45 },
            };

            var coefficient = game.GetCoefficientForRow(symbolsInRow);

            decimal currentBalance = 50;
            decimal stake = 20;
            Deposit deposit = new Deposit(currentBalance);

            deposit.CalculateBalance(stake, coefficient);

            Assert.That(currentBalance - stake + stake * coefficient == deposit.CurrentBalance);
        }
    }
}