using System.Runtime.CompilerServices;

namespace BodyMassIndexCalculator
{
    public class BMIRenderer
    {
        public static string Render(decimal bmi)
        {
            switch (bmi)
            {
                case decimal a when bmi < (decimal) 18.5: return $"underweight: {bmi}";
                case decimal a when bmi >= (decimal) 25: return $"overweight: {bmi}";
                default: return $"healthy: {bmi}";
            }
        }
    }
}