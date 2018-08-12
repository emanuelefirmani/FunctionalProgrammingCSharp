using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using LaYumba.Functional;

namespace FunctorsMonads
{
    public static class IEnumerableMapper
    {
        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> input, Func<T, R> f)
            => input.Bind(v => (Option<R>) f(v));
    }

    public class IEnumerableMapperTest
    {
        [Fact]
        public void should_map_empty_list()
        {
            var sut = new List<string>();

            int F(string s) => s.Length;

            var actual = sut.Map(F).ToList();

            actual.Should().NotBeNull();
            actual.Count.Should().Be(0);
        }

        [Fact]
        public void should_map_list_with_one_element()
        {
            var sut = new List<string> {"test"};
            int f(string s) => string.IsNullOrEmpty(s) ? 0 : s.Length;

            var actual = sut.Map(f);

            actual.Single().Should().Be(4);
        }

        [Fact]
        public void should_map_list_with_elements()
        {
            var sut = new List<string> 
            {
                "","test", "test2", "test3"
            };

            int F(string s) => string.IsNullOrEmpty(s) ? 0 : s.Length;

            var actual = sut.Map(F);

            actual.Should().BeEquivalentTo(new List<int> { 0, 4, 5, 5});
        }

        [Fact]
        public void should_map_dictionary_with_integers_to_strings()
        {
            var sut = new List<int> {0, 1, 2}; 

            string F(int n) => n.ToString();

            var actual = sut.Map(F);

            actual.Should().BeEquivalentTo(new List<string> { "0", "1", "2"});
        }
    }
}