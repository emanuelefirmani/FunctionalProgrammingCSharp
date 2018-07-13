using System;

namespace BodyMassIndexCalculator
{
    public class Calculator
    {
        public static decimal Calculate(decimal weight, decimal height)
        {
            if (height == 0)
                return 0;

            var pWeight = (double) weight;
            var pHeight = (double) height;

            return Math.Round((decimal) (pWeight / pHeight / pHeight), 1);
        }
    }
}