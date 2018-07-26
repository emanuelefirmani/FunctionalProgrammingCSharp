using System;
using FluentAssertions;
using LaYumba.Functional.Option;
using Xunit;

namespace HonestFunctions
{
    public class DayOfWeekTest
    {
        [Theory]
        [InlineData("Monday", DayOfWeek.Monday)]
        [InlineData("Tuesday", DayOfWeek.Tuesday)]
        [InlineData("Wednesday", DayOfWeek.Wednesday)]
        [InlineData("Thursday", DayOfWeek.Thursday)]
        [InlineData("Friday", DayOfWeek.Friday)]
        [InlineData("Saturday", DayOfWeek.Saturday)]
        [InlineData("Sunday", DayOfWeek.Sunday)]
        public void should_resolve(string input, DayOfWeek expected)
        {
            DayOfWeekHelper.GetDay(input).Equals(expected).Should().BeTrue();
        }

        [Theory]
        [InlineData("text")]
        [InlineData("Montag")]
        [InlineData("Freeday")]
        public void should_not_resolve(string input)
        {
            DayOfWeekHelper.GetDay(input).Equals(new None()).Should().BeTrue();
        }
    }
}