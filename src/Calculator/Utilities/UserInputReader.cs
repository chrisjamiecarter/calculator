// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Utilities.UserInputReader
// -------------------------------------------------------------------------------------------------
// Helper class to present a question and return a valid user response from the console.
// -------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace CalculatorProgram.Utilities;

internal class UserInputReader
{
    #region Methods: Internal Static

    internal static char GetChar(string message, string allowedCharsPattern)
    {
        string? input = "";
        char output;

        Console.Write(message);
        input = Console.ReadLine();

        // Note: this will validate the input.
        while (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, allowedCharsPattern))
        {
            Console.Write($"This is not valid input. {message}");
            input = Console.ReadLine();
        }

        // Converts input string to output char.
        output = input.First();

        return output;
    }

    internal static double GetDouble(string message)
    {
        string? input = "";
        double output = 0;
        
        Console.Write(message);
        input = Console.ReadLine();
        
        // Note: this will validate the input and assign valid values to the output variable.
        while (string.IsNullOrWhiteSpace(input) || !double.TryParse(input, out output))
        {
            Console.Write($"This is not valid input. {message}");
            input = Console.ReadLine();
        }
                
        return output;
    }

    internal static int GetInt(string message)
    {
        string? input = "";
        int output = 0;

        Console.Write(message);
        input = Console.ReadLine();

        // Note: this will validate the input and assign valid values to the output variable.
        while (string.IsNullOrEmpty(input) || !int.TryParse(input, out output))
        {
            Console.Write($"This is not valid input. {message}");
            input = Console.ReadLine();
        }

        return output;
    }

    internal static int GetInt(string message, int min, int max)
    {
        string? input = "";
        int output = 0;

        Console.Write(message);
        input = Console.ReadLine();

        // Note: this will validate the input and assign valid values to the output variable.
        while (string.IsNullOrEmpty(input) || !int.TryParse(input, out output) || int.Parse(input) < min || int.Parse(input) > max)
        {
            Console.Write($"This is not valid input. {message}");
            input = Console.ReadLine();
        }

        return output;
    }

    #endregion
}
