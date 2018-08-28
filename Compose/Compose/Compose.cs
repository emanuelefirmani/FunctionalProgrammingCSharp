using System;
using FluentAssertions;
using Xunit;

namespace Compose
{
    public class Compose
    {
        public static Func<T, T> ComposeFunctions<T>(Func<T, T> f, Func<T, T> g) =>
            x => f(g(x));
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
            int Sqr(int x) => x * x;
            int Sum1(int x) => x + 1;

            var f = Compose.ComposeFunctions<int>(Sqr, Sum1);

            f(value).Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 5)]
        [InlineData(3, 10)]
        public void should_compose_int_functions2(int value, int expected)
        {
            int Sqr(int x) => x * x;
            int Sum1(int x) => x + 1;

            var f = Compose.ComposeFunctions<int>(Sum1, Sqr);
            
            f(value).Should().Be(expected);
        }

        [Theory]
        [InlineData("test", "testba")]
        [InlineData("123", "123ba")]
        public void should_compose_string_functions(string value, string expected)
        {
            string Concata(string x) => x + "a";
            string Concatb(string x) => x + "b";

            var f = Compose.ComposeFunctions<string>(Concata, Concatb);
            
            f(value).Should().Be(expected);
        }

        [Theory]
        [InlineData("test", "testab")]
        [InlineData("123", "123ab")]
        public void should_compose_string_functions2(string value, string expected)
        {
            string Concata(string x) => x + "a";
            string Concatb(string x) => x + "b";

            var f = Compose.ComposeFunctions<string>(Concatb, Concata);
            
            f(value).Should().Be(expected);
        }
    }
}