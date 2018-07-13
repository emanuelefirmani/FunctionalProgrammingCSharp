using System;

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

        public decimal Read()
        {
            var isValid = false;
            decimal value = 0;
            
            while (!isValid)
            {
                var input = Console.ReadLine();
                (isValid, value) = Validate(input);
            }

            return value;
        }
    }
}