using BodyMassIndexCalculator;
using FluentAssertions;
using Xunit;

namespace BodyMassIndexCalculatorTest
{
    public class CalculatorTest
    {
        [Xunit.Theory]
        [InlineData(1, 1, 1)]
        [InlineData(25, 5, 1)]
        [InlineData(100, 5, 4)]
        [InlineData(70, 1.7, 24.2)]
        public void should_calculate_bmi(int weight, decimal height, decimal expected)
        {
            var sut = new Calculator();
            var actual = Calculator.Calculate(weight, height);
            actual.Should().Be(expected);
        }
    }
}