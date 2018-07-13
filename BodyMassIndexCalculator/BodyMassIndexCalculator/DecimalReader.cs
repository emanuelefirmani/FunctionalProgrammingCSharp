using System;

namespace BodyMassIndexCalculator
{
    public class DecimalReader
    {
        private readonly Action<string> _writer;
        public DecimalReader(){}
        
        public DecimalReader(Action<string> writer)
        {
            _writer = writer;
        }

        public static (bool IsValid, decimal Value) Validate(string input)
        {
            if (decimal.TryParse(input, out var value))
                return (true, value);

            return (false, 0);
        }

        public decimal Read(string message)
        {
            var isValid = false;
            decimal value = 0;
            
            Console.WriteLine(message);
            while (!isValid)
            {
                var input = Console.ReadLine();
                (isValid, value) = Validate(input);
                if(!isValid)Console.WriteLine("Provided value isn't a valid decimal");
            }

            Console.WriteLine();
            Console.WriteLine();
            return value;
        }

        public void Write(string text)
        {
            _writer(text);
        }
    }
}