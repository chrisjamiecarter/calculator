// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.CalculationPage
// -------------------------------------------------------------------------------------------------
// The calculation menu console view of the application.
// -------------------------------------------------------------------------------------------------
using System.Text;
using CalculatorLibrary.Constants;
using CalculatorLibrary.Models;
using CalculatorProgram.Utilities;

namespace CalculatorProgram.Views;

internal class CalculationPage
{
    #region Constants

    private const string PageTitle = "Calculation";

    #endregion
    #region Properties

    internal static string MenuText
    {
        get
        {
            var sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine($"{Application.Title}: {PageTitle}");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("");
             
            return sb.ToString();
        }
    }

    #endregion
    #region Methods: Internal Static

    internal static Calculation Show(double? firstNumber = null)
    {
        Console.Clear();
        Console.Write(MenuText);

        return GetCalculation(firstNumber);
    }

    internal static Calculation GetCalculation(double? firstNumber = null)
    {
        // Declare variables and set to empty.
        double numInput1 = 0;
        double numInput2 = 0;
        double result = 0;

        if (firstNumber == null)
        {
            // Ask the user to type the first number.
            numInput1 = UserInputReader.GetDouble("Type a number, and then press Enter: ");
        }
        else
        {
            // Use the passed in first number.
            numInput1 = firstNumber!.Value;
            Console.WriteLine($"First number: {numInput1}");
        }

        // Ask the user to choose an operator.
        var operatorQuestion = new StringBuilder();
        operatorQuestion.AppendLine("Choose an operator from the following list:");
        operatorQuestion.AppendLine("\ta - Add");
        operatorQuestion.AppendLine("\ts - Subtract");
        operatorQuestion.AppendLine("\tm - Multiply");
        operatorQuestion.AppendLine("\td - Divide");
        operatorQuestion.Append("Your option? ");
        char option = UserInputReader.GetChar(operatorQuestion.ToString(), "[a|s|m|d]");

        // Ask the user to type the second number.
        numInput2 = UserInputReader.GetDouble("Type another number, and then press Enter: ");

        return new Calculation()
        {
            FirstNumber = numInput1,
            SecondNumber = numInput2,
            Option = option
        };
    }

    #endregion
}
