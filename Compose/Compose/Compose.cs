using System;
using FluentAssertions;
using Xunit;

namespace Compose
{
    public class Compose
    {
        public static Func<int, int> ComposeFunctions(Func<int, int> f, Func<int, int> g) =>
            new Func<int, int>(x => f(g(x)));
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

            var f = Compose.ComposeFunctions(sqr, sum1);

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

            var f = Compose.ComposeFunctions(sum1, sqr);

            f(value).Should().Be(expected);
        }
    }
}