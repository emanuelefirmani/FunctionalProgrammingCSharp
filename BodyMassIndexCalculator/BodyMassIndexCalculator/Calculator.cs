using System;

namespace BodyMassIndexCalculator
{
    public class Calculator
    {
        public static decimal Calculate(int weight, decimal height)
        {
            var pWeight = (double) weight;
            var pHeight = (double) height;

            return Math.Round((decimal) (pWeight / pHeight / pHeight), 1);
        }
    }
}