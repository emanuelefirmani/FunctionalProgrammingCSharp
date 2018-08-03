using System;
using System.Collections.Generic;
using System.Linq;
using BodyMassIndexCalculator;
using FluentAssertions;
using LaYumba.Functional;
using LaYumba.Functional.Option;
using Xunit;

namespace BodyMassIndexCalculatorTest
{
    public class DecimalReaderTest : IDisposable
    {
        private List<string> _sentTexts;
        private DecimalReader _sut;

        public DecimalReaderTest()
        {
            _sentTexts = new List<string>();
            _sut = new DecimalReader((text) => { _sentTexts.Add(text); }, () => "42");
        }

        public void Dispose()
        {
            _sentTexts = null;
            _sut = null;
        }
        
        [Theory]
        [InlineData(".5", .5)]
        [InlineData("5", 5)]
        [InlineData("1.5", 1.5)]
        [InlineData("1,5", 15)]
        [InlineData("1.555", 1.555)]
        [InlineData("1,55.5", 155.5)]
        public void should_validate_decimal(string input, decimal expected)
        {
            var actual = DecimalReader.Validate(input);
            actual.Equals(expected).Should().BeTrue();
        }

        [Theory]
        [InlineData(",5")]
        [InlineData("text")]
        public void should_not_validate_non_decimals(string input)
        {
            var actual = DecimalReader.Validate(input);
            actual.Equals(new Option<decimal>()).Should().BeTrue();
        }

        [Fact]
        public void read_should_use_function()
        {
            var actual = _sut.Read("some text");
            
            actual.Should().Be(42);
            _sentTexts.Count.Should().Be(3);
            _sentTexts[0].Should().Be("some text");
            _sentTexts[1].Should().BeNull();
            _sentTexts[2].Should().BeNull();
        }
    }
}