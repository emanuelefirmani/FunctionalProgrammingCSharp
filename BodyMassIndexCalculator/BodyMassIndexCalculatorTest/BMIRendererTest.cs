using BodyMassIndexCalculator;
using FluentAssertions;
using Xunit;

namespace BodyMassIndexCalculatorTest
{
    public class BMIRendererTest
    {
        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(18.499999)]
        [InlineData(-7)]
        [InlineData(0)]
        public void should_return_underweight(decimal bmi)
        {
            var actual = BMIRenderer.Render(bmi);
            actual.Should().StartWith("underweight: ");
        }

        [Xunit.Theory]
        [InlineData(25)]
        [InlineData(30)]
        [InlineData(1000)]
        public void should_return_overweight(decimal bmi)
        {
            var actual = BMIRenderer.Render(bmi);
            actual.Should().StartWith("overweight: ");
        }

        [Xunit.Theory]
        [InlineData(18.5)]
        [InlineData(20)]
        [InlineData(24.9999)]
        public void should_return_healthy(decimal bmi)
        {
            var actual = BMIRenderer.Render(bmi);
            actual.Should().StartWith("healthy: ");
        }
    }
}