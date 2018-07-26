using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LaYumba.Functional;
using Xunit;

namespace HonestFunctions
{
    public class LookupFunction
    {
        public static Option<int> Lookup(IEnumerable<int> list, Func<int, bool> predicate)
        {
            return list.First(predicate);
        }
    }

    public class LookupFunctionTest
    {
        [Fact]
        public void should_return_right_value_for_numbers()
        {
            bool IsOdd(int i) => i % 2 == 1;
            
            List<int> input;
            
            input = new List<int> {1};
            LookupFunction.Lookup(input, IsOdd).Equals(1).Should().BeTrue();

            input = new List<int> {0,2,1};
            LookupFunction.Lookup(input, IsOdd).Equals(1).Should().BeTrue();
        }       
    }
}