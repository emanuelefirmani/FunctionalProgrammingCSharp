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

            _writer(message);
            while (!isValid)
            {
                var input = _retriever();
                (isValid, value) = Validate(input);
                if(!isValid)
                {
                    _writer("Provided value isn't a valid decimal");
                }
            }

            _writer(null);
            _writer(null);
            return value;
        }
    }
}