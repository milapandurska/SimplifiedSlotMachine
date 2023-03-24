using Microsoft.Extensions.Configuration;
using SimplifiedSlotMachine;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

var symbols = config.GetSection("Symbols").Get<List<Symbol>>();
int gameColumnLength=0;
int gameRowLength=0;
if (!int.TryParse(config["GameColumnLength"], out gameColumnLength)
    || !int.TryParse(config["GameRowLength"], out gameRowLength)
    || symbols == null
    || symbols.Count() == 0)
{
    Console.WriteLine("Configuration error!");
    Environment.Exit(0);
}

decimal inputDeposit = InputHelper.AddAmount("Please deposit money you would like to play with:");
Deposit deposit=new Deposit(inputDeposit);
Game game = new(gameColumnLength, gameRowLength, symbols);

do
{
    Console.WriteLine();
    decimal stake = InputHelper.AddAmount("Enter stake amount:", deposit.CurrentBalance);
    Console.WriteLine();
   
    decimal coefficient = 0;
    for (int i = 0; i < gameRowLength; i++)
    {
        var row = game.GenerateArray();
        Console.WriteLine(string.Join("", row.Select(a => a.Key)));
        coefficient += game.GetCoefficientForRow(row); 
    }
    deposit.CalculateBalance(stake, coefficient);

    Console.WriteLine();
    Console.WriteLine("You have won: " + coefficient * stake);
    Console.WriteLine("Current balance is: " + deposit.CurrentBalance); 
}
while (deposit.CurrentBalance > 0);

Console.WriteLine("Game Over!");
Console.ReadLine();

