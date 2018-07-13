namespace BodyMassIndexCalculator
{
    public class DecimalReader
    {
        public static (bool IsValid, decimal Value) Validate(string input)
        {
            if (decimal.TryParse(input, out var value))
                return (true, value);

            return (false, 0);
        }
    }
}