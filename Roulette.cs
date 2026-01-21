using System;

class RouletteGame
{
    static Random random = new Random();

    static void Main()
    {
        int balance = 1000;
        Console.WriteLine("Welcome to the Roulette Game");
        Console.WriteLine($"Your starting balance is: ${balance}");

        while (balance > 0)
        {
            Console.WriteLine("\nYou can bet on:");
            Console.WriteLine("1. Red");
            Console.WriteLine("2. Black");
            Console.WriteLine("3. Odd");
            Console.WriteLine("4. Even");
            Console.WriteLine("5. Specific Number (0-36)");

            Console.Write("Choose your bet type (1-5): ");
            string betType = Console.ReadLine();

            string betChoice = "";
            int betNumber = -1;

            if (betType == "5")
            {
                Console.Write("Enter the number you want to bet on (0–36): ");
                betChoice = Console.ReadLine();
                int.TryParse(betChoice, out betNumber);
            }

            Console.Write($"Your current balance: ${balance}. How much do you want to bet? ");
        int wager = int.Parse(Console.ReadLine());

            if (wager > balance || wager <= 0)
            {
                Console.WriteLine("Invalid wager amount.");
                continue;
            }

            int result = SpinWheel();
            string color = GetColor(result);

            Console.WriteLine($"\nThe ball landed on: {result} ({color.ToUpper()})");

            bool win = CheckWin(betType, betNumber, result, color);

            if (win)
            {
                int winnings = (betType == "5") ? wager * 35 : wager * 2;
                balance += winnings;
                Console.WriteLine($"You win! You won ${winnings}.");
            }
            else
            {
                balance -= wager;
                Console.WriteLine("You lose!");
            }

            Console.WriteLine($"Your current balance is: ${balance}");
            if (balance <= 0)
            {
                Console.WriteLine("You are out of money. Game over!");
                break;
            }

            Console.Write("\nDo you want to play again? (y/n): ");
            string again = Console.ReadLine();
            if (again.ToLower() != "y")
            {
                Console.WriteLine("Thanks for playing!");
                break;
            }
        }
    }

    static int SpinWheel()
    {
        return random.Next(0, 37); // 0 to 36
    }

    static string GetColor(int number)
    {
        int[] redNumbers = {
            1, 3, 5, 7, 9, 12, 14, 16, 18, 19,
            21, 23, 25, 27, 30, 32, 34, 36
        };
        if (number == 0) return "green";
        return Array.Exists(redNumbers, n => n == number) ? "red" : "black";
    }

    static bool CheckWin(string betType, int betNumber, int result, string color)
    {
        switch (betType)
        {
            case "1": return color == "red";
            case "2": return color == "black";
            case "3": return result != 0 && result % 2 != 0;
            case "4": return result != 0 && result % 2 == 0;
            case "5": return result == betNumber;
            default: return false;
        }
    }
}
