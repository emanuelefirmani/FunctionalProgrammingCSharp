using System;
using FluentAssertions;
using Xunit;

namespace HonestFunctions
{
    public class DayOfWeekTest
    {
        [Xunit.Theory]
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
    }
}