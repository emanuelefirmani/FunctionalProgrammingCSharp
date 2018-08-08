using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace FunctorsMonads
{
    public static class IDictionaryMapper
    {
        public static IDictionary<K, R> Map<K, T, R>(this IDictionary<K, T> input, Func<T, R> f)
        {
            return input.ToList().ToDictionary(x => x.Key, x => f(x.Value));
        }
    }

    public class IDictionaryMapperTest
    {
        [Fact]
        public void should_map_empty_dictionary()
        {
            var sut = new Dictionary<string, string>();

            int F(string s) => s.Length;

            var actual = sut.Map(F);

            actual.Should().NotBeNull();
            actual.Count.Should().Be(0);
        }

        [Fact]
        public void should_map_dictionary_with_one_element()
        {
            var sut = new Dictionary<string, string> {{"test", "test"}};
            int f(string s) => string.IsNullOrEmpty(s) ? 0 : s.Length;

            var actual = sut.Map(f);

            actual.Count.Should().Be(1);
            actual["test"].Should().Be(4);
        }

        [Fact]
        public void should_map_dictionary_with_elements()
        {
            var sut = new Dictionary<string, string> 
            {
                {"", ""},
                {"test", "test"},
                {"test2", "test3"},
                {"test3", "test3"},
            };

            int f(string s) => string.IsNullOrEmpty(s) ? 0 : s.Length;

            var actual = sut.Map(f);

            actual.Count.Should().Be(4);
            actual[""].Should().Be(0);
            actual["test"].Should().Be(4);
            actual["test2"].Should().Be(5);
            actual["test3"].Should().Be(5);
        }

        [Fact]
        public void should_map_dictionary_with_integers_to_strings()
        {
            var sut = new Dictionary<string, int> 
            {
                {"test", 0},
                {"test1", 1},
                {"test2", 2},
            };

            string F(int n) => n.ToString();

            var actual = sut.Map(F);

            actual.Count.Should().Be(3);
            actual["test"].Should().Be("0");
            actual["test1"].Should().Be("1");
            actual["test2"].Should().Be("2");
        }
    }
}