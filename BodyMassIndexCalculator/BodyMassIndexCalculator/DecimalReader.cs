using System;
using LaYumba.Functional;
using LaYumba.Functional.Option;

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

        public static Option<decimal> Validate(string input)
        {
            if (decimal.TryParse(input, out var value))
                return value;

            return new Option<decimal>();
        }

        public decimal Read(string message)
        {
            decimal value = 0;
            
            _writer(message);
            var readValue = Validate(_retriever());

            readValue.Match(
                None: () => value = Read("Provided value isn't a valid decimal"),
                Some: (d) =>
                {
                    _writer(null);
                    _writer(null);
                    return value = d;
                });

            return value;
        }
    }
}