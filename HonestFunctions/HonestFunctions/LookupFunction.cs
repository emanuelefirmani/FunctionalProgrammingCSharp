using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LaYumba.Functional;
using LaYumba.Functional.Option;
using Xunit;

namespace HonestFunctions
{
    public static class LookupFunction
    {
        public static Option<T> Lookup<T>(this IEnumerable<T> list, Func<T, bool> predicate) =>
            list.Any(predicate) ? list.First(predicate) : new Option<T>();
    }

    public class LookupFunctionTest
    {
        [Fact]
        public void should_return_right_value_for_numbers()
        {
            bool IsOdd(int i) => i % 2 == 1;
            
            List<int> input;
            
            input = new List<int> {1};
            input.Lookup(IsOdd).Equals(1).Should().BeTrue();

            input = new List<int> {0,2,1};
            input.Lookup(IsOdd).Equals(1).Should().BeTrue();

            input = new List<int> {0,2,3,1};
            input.Lookup(IsOdd).Equals(3).Should().BeTrue();
        }       

        [Fact]
        public void should_return_none_for_numbers()
        {
            bool IsOdd(int i) => i % 2 == 1;
            
            List<int> input;
            
            input = new List<int>();
            input.Lookup(IsOdd).Equals(new None()).Should().BeTrue();
            
            input = new List<int> {0};
            input.Lookup(IsOdd).Equals(new None()).Should().BeTrue();

            input = new List<int> {0,2,4};
            input.Lookup(IsOdd).Equals(new None()).Should().BeTrue();
        }       

        [Fact]
        public void should_return_right_value_for_strings()
        {
            bool IsCorrect(string s) => s == "ok";
            
            List<string> input;
            
            input = new List<string> {"ok"};
            input.Lookup(IsCorrect).Equals("ok").Should().BeTrue();

            input = new List<string> {"a", "b", "ok"};
            input.Lookup(IsCorrect).Equals("ok").Should().BeTrue();

            input = new List<string> {"A", "B", "ok", "C"};
            input.Lookup(IsCorrect).Equals("ok").Should().BeTrue();
        }       

        [Fact]
        public void should_return_none_for_strings()
        {
            bool IsCorrect(string s) => s == "ok";
            
            List<string> input;
            
            input = new List<string>();
            input.Lookup(IsCorrect).Equals(new None()).Should().BeTrue();

            input = new List<string> {"a"};
            input.Lookup(IsCorrect).Equals(new None()).Should().BeTrue();

            input = new List<string> {"A", "B", "C"};
            input.Lookup(IsCorrect).Equals(new None()).Should().BeTrue();
        }       
    }
}