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
        public void should_calculate_bmi(decimal weight, decimal height, decimal expected)
        {
            var actual = Calculator.Calculate(weight, height);
            actual.Should().Be(expected);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(100)]
        [InlineData(70)]
        public void should_return_0_for_invalid_height(decimal weight)
        {
            var actual = Calculator.Calculate(weight, 0);
            actual.Should().Be(0);
        }
    }
}