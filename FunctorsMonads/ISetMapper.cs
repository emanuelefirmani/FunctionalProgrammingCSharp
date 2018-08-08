using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace FunctorsMonads
{
    public static class ISetMapper
    {
        public static HashSet<int> Map(this ISet<string> input, Func<string, int> f)
        {
            return input.Select(f).ToHashSet();
        }
    }

    public class ISetMapperTest
    {
        [Fact]
        public void should_map_empty_set()
        {
            var sut = new HashSet<string>();

            Func<string, int> f = (string s) => s.Length;

            var actual = sut.Map(f);

            actual.Should().NotBeNull();
            actual.Count.Should().Be(0);
        }

        [Fact]
        public void should_map_hashset_with_one_element()
        {
            var sut = new HashSet<string>();
            sut.Add("test");

            int f(string s) => string.IsNullOrEmpty(s) ? 0 : s.Length;

            var actual = sut.Map(f);

            actual.Count.Should().Be(1);
            actual.Single().Should().Be(4);
        }

        [Fact]
        public void should_map_hashset_with_elements()
        {
            var sut = new HashSet<string>
            {
                null,
                "",
                "test",
                "test2",
                "test3"
            };

            int f(string s) => string.IsNullOrEmpty(s) ? 0 : s.Length;

            var actual = sut.Map(f);

            actual.Count.Should().Be(3);
            actual.Single(x => x == 0).Should().Be(0);
            actual.Single(x => x == 4).Should().Be(4);
            actual.Single(x => x == 5).Should().Be(5);
        }
    }
}