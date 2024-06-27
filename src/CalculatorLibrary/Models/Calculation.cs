using CalculatorLibrary.Constants;

namespace CalculatorLibrary.Models;

public class Calculation
{
    public double FirstNumber { get; init; }
    
    public double SecondNumber { get; init; }

    public char Option { get; init; }

    public string Symbol
    {
        get => Option switch
        {
            'a' => OperationSymbol.Addition,
            's' => OperationSymbol.Subtraction,
            'm' => OperationSymbol.Multiplication,
            'd' => OperationSymbol.Division,
            _ => throw new ArgumentOutOfRangeException(nameof(Option))
        };
    }
    
    public double Result { get; set; }

    public override string ToString()
    {
        return $"{FirstNumber} {Symbol} {SecondNumber} = {Result}";
    }
}
