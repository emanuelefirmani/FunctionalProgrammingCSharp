using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace FunctorsMonads
{
    public static class ISetMapper
    {
        public static HashSet<int> Map(this HashSet<string> input, Func<string, int> f)
        {
            return new HashSet<int>();
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
    }
}