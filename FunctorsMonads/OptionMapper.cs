using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using LaYumba.Functional;

namespace FunctorsMonads
{
    public static class OptionMapper
    {
        public static Option<R> Map<T, R>(this Option<T> input, Func<T, R> f) =>
            input.Bind((v) => (Option<R>)f(v));
    }

    public class OptionMapperTest
    {
        [Fact]
        public void should_map_empty_option()
        {
            var sut = new Option<string>();

            int F(string s) => s.Length;

            var actual = sut.Map(F);

            actual.Should().Be(new Option<int>());
        }


        [Fact]
        public void should_map_Option_with_integer_to_string()
        {
            var sut = (Option<int>)5;

            string F(int n) => n.ToString();

            var actual = sut.Map(F);

            actual.Should().Be((Option<string>)"5");
        }
    }
}