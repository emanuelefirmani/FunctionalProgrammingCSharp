using System;

namespace BodyMassIndexCalculator
{
    public static class Program
    {
        public static void Main()
        {
            var reader = new DecimalReader(Console.WriteLine);
            
            var weight = reader.Read("Please, write your weight (kg):");
            var height = reader.Read("Please, write your height (m):");
            
            Console.WriteLine($"You are {BMIRenderer.Render(Calculator.Calculate(weight, height))}");
        }
    }
}