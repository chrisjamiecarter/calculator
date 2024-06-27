// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Views.MainMenu
// -------------------------------------------------------------------------------------------------
// The main menu console view of the application.
// -------------------------------------------------------------------------------------------------
using System.Text;
using CalculatorLibrary;
using CalculatorLibrary.Constants;
using CalculatorProgram.Enums;
using CalculatorProgram.Utilities;

namespace CalculatorProgram.Views;

internal class MainMenu
{
    #region Constants

    private const string PageTitle = "Main Menu";

    #endregion
    #region Variables

    private readonly Calculator _calculator;

    #endregion
    #region Constructors

    public MainMenu()
    {
        _calculator = new Calculator();
    }

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
            sb.AppendLine("N - New calculation");
            sb.AppendLine("R - Recall a result and perform a new calculation");
            sb.AppendLine("V - View history");
            sb.AppendLine("C - Clear history");
            sb.AppendLine("Q - Quit the application");
            sb.AppendLine("");
            
            return sb.ToString();
        }
    }

    #endregion
    #region Methods: Internal Static

    internal ProgramStatus Show()
    {
        Console.Clear();
        Console.Write(MenuText);
        
        var option = UserInputReader.GetChar("Enter your selection: ", "[n|N|r|R|v|V|c|C|q|Q]");

        return PerformOption(option);
    }

    internal ProgramStatus PerformOption(char option)
    {
        // Retain Started unless explicitly change (i.e. quit chosen).
        var output = ProgramStatus.Started;

        // Normalise input option.
        option = char.ToLower(option);

        switch (option)
        {
            case 'n':
                // New calculation.
                var newCalculation = CalculationPage.Show();
                newCalculation.Result = _calculator.DoOperation(newCalculation.FirstNumber, newCalculation.SecondNumber, newCalculation.Option);
                Console.WriteLine(newCalculation.ToString());
                Console.WriteLine("Press any key to continue...");
                Console.Read();
                break;
            case 'r':
                // Recall calculation.
                if (_calculator.HasHistoryItems)
                {
                    var firstNumber = RecallPage.Show(_calculator.History);
                    var recallCalculation = CalculationPage.Show(firstNumber);
                    recallCalculation.Result = _calculator.DoOperation(recallCalculation.FirstNumber, recallCalculation.SecondNumber, recallCalculation.Option);
                    Console.WriteLine(recallCalculation.ToString());
                    Console.WriteLine("Press any key to continue...");
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("No history items to recall. Press any key to continue...");
                    Console.Read();
                }
                break;
            case 'v':
                // View calculation history.
                if (_calculator.HasHistoryItems)
                {
                    HistoryPage.Show(_calculator.History);
                }
                else
                {
                    Console.WriteLine("No history items to view. Press any key to continue...");
                    Console.Read();
                }
                break;
            case 'c':
                // Clear calculation history.
                _calculator.ClearHistory();
                Console.WriteLine("History cleared. Press any key to continue...");
                Console.Read();

                break;
            case 'q':
                // Quit.
                output = ProgramStatus.Stopped;
                break;
            default:
                Console.WriteLine("Invalid option selected. Press any key to continue...");
                Console.Read();
                break;
        }

        return output;
    }

    internal void Close()
    {
        _calculator.Finish();
    }

    #endregion
}
