using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        #region Variables

        private readonly JsonWriter _writer;

        #endregion
        #region Constructors

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            
            logFile.AutoFlush = true;
            
            _writer = new JsonTextWriter(logFile)
            {
                Formatting = Formatting.Indented
            };
            
            // Create the root object, and start the operations array.
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();

            // Initialise the History list.
            History = [];
        }

        #endregion
        #region Properties

        public int UsageCount { get; private set; }

        public List<string> History { get; }

        #endregion
        #region Methods: Public

        public void ClearHistory()
        {
            History.Clear();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            // Default value is "not-a-number" if an operation, such as division, could result in an error.
            double result = double.NaN;

            // Write operation data to JSON log file.
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(num2);
            _writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    _writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    _writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    _writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    _writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }

            // Write result data to JSON log file.
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();

            // Add to History.
            History.Add($"{num1} {op} {num2} = {result}");

            // Increment the usage counter that records how many operations have been performed.
            UsageCount++;

            return result;
        }

        public void Finish()
        {
            // End the operations array.
            _writer.WriteEndArray();
            
            // Write the usage count to the JSON log file.
            _writer.WritePropertyName("UsageCount");
            _writer.WriteValue(UsageCount);
            
            // End the root object and close the lock on the json file.
            _writer.WriteEndObject();
            _writer.Close();
        }

        #endregion
    }
}
