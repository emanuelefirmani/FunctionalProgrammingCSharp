using System;
using FluentAssertions;
using Xunit;

namespace Compose
{
    public class Compose
    {
        public static Func<T, T> ComposeFunctions<T>(Func<T, T> f, Func<T, T> g) =>
            new Func<T, T>(x => f(g(x)));
    }

    public class ComposeText
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 4)]
        [InlineData(2, 9)]
        [InlineData(3, 16)]
        public void should_compose_int_functions(int value, int expected)
        {
            int sqr(int x) => x * x;
            int sum1(int x) => x + 1;

            var f = Compose.ComposeFunctions<int>(sqr, sum1);

            f(value).Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 5)]
        [InlineData(3, 10)]
        public void should_compose_int_functions2(int value, int expected)
        {
            int sqr(int x) => x * x;
            int sum1(int x) => x + 1;

            var f = Compose.ComposeFunctions<int>(sum1, sqr);
            
            f(value).Should().Be(expected);
        }

        [Theory]
        [InlineData("test", "testba")]
        [InlineData("123", "123ba")]
        public void should_compose_string_functions(string value, string expected)
        {
            int i = 0;
            
            string concata(string x) => x + "a";
            string concatb(string x) => x + "b";

            var f = Compose.ComposeFunctions<string>(concata, concatb);
            
            f(value).Should().Be(expected);
        }

        [Theory]
        [InlineData("test", "testab")]
        [InlineData("123", "123ab")]
        public void should_compose_string_functions2(string value, string expected)
        {
            int i = 0;
            
            string concata(string x) => x + "a";
            string concatb(string x) => x + "b";

            var f = Compose.ComposeFunctions<string>(concatb, concata);
            
            f(value).Should().Be(expected);
        }
    }
}