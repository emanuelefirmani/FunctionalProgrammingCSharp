using System;

namespace BodyMassIndexCalculator
{
    public class DecimalReader
    {
        private readonly Action<string> _writer;
        private readonly Func<string> _retriever;

        public DecimalReader(Action<string> writer, Func<string> retriever)
        {
            _writer = writer;
            _retriever = retriever;
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
            
            Write(message);
            while (!isValid)
            {
                var input = _retriever();
                (isValid, value) = Validate(input);
                if(!isValid) Write("Provided value isn't a valid decimal");
            }

            Write(null);
            Write(null);
            return value;
        }

        public void Write(string text)
        {
            _writer(text);
        }

        public string Retrieve()
        {
            return _retriever();
        }
    }
}