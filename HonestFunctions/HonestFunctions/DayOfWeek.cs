using System;
using FluentAssertions;
using LaYumba.Functional;
using LaYumba.Functional.Option;
using Xunit;
using Enum = LaYumba.Functional.Enum;

namespace HonestFunctions
{
    public class DayOfWeekHelper
    {
        public static Option<DayOfWeek> GetDay(string input)
        {
            return Enum.Parse<DayOfWeek>(input);
        }
    }
    
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