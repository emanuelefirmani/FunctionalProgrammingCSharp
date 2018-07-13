using System;
using System.Collections.Generic;
using System.Linq;
using BodyMassIndexCalculator;
using FluentAssertions;
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
            var (valid, actual) = DecimalReader.Validate(input);
            valid.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(",5", 5)]
        [InlineData("text", 1.555)]
        public void should_notvalidate_nondecimals(string input, decimal expected)
        {
            var (valid, _) = DecimalReader.Validate(input);
            valid.Should().BeFalse();
        }

        [Fact]
        public void should_write_to_function()
        {
            _sut.Write("some text");

            _sentTexts.Single().Should().Be("some text");
            _sentTexts.Count.Should().Be(1);
        }

        [Fact]
        public void should_retrieve_from_function()
        {
            var actual = _sut.Retrieve();
            actual.Should().Be("42");
        }
    }
}