using System;
using System.Collections.Specialized;
using System.Configuration;
using FluentAssertions;
using LaYumba.Functional;
using LaYumba.Functional.Option;
using Xunit;

namespace HonestFunctions
{
    public class AppConfig
    {
        private readonly NameValueCollection _source;

        public AppConfig() : this(ConfigurationManager.AppSettings)
        {
        }

        public AppConfig(NameValueCollection source) =>
            _source = source;

        public Option<T> Get<T>(string name) => _source[name] != null ? (T)(Convert.ChangeType(_source[name], typeof(T))) : new Option<T>();

        public T Get<T>(string name, T defaultValue) => _source[name] != null ? (T)(Convert.ChangeType(_source[name], typeof(T))) : defaultValue;
    }


    public class AppConfigTest
    {
        [Fact]
        public void should_return_some()
        {
            var source = new NameValueCollection();
            source.Add("k1", "2000-01-01");
            source.Add("k2", "42");
            source.Add("k3", "test");
            
            var sut = new AppConfig(source);

            sut.Get<DateTime>("k1").Equals(new DateTime(2000, 1, 1)).Should().BeTrue();
            sut.Get<int>("k2").Equals(42).Should().BeTrue();
            sut.Get<string>("k3").Equals("test").Should().BeTrue();
        }

        [Fact]
        public void should_return_none()
        {
            var source = new NameValueCollection();
            source.Add("k1", "2000-01-01");
            source.Add("k2", "42");
            source.Add("k3", "test");
            
            var sut = new AppConfig(source);

            sut.Get<DateTime>("nk1").Should().Be(new Option<DateTime>());
            sut.Get<int>("nk2").Should().Be(new Option<int>());
            sut.Get<string>("nk3").Should().Be(new Option<string>());
        }

        [Fact]
        public void should_return_value()
        {
            var source = new NameValueCollection();
            source.Add("k1", "2000-01-01");
            source.Add("k2", "42");
            source.Add("k3", "test");
            
            var sut = new AppConfig(source);

            sut.Get("k1", DateTime.Now).Should().Be(new DateTime(2000, 1, 1));
            sut.Get("k2", 0).Should().Be(42);
            sut.Get("k3", "a").Should().Be("test");
        }

        [Fact]
        public void should_return_default()
        {
            var source = new NameValueCollection();
            source.Add("k1", "2000-01-01");
            source.Add("k2", "42");
            source.Add("k3", "test");
            
            var sut = new AppConfig(source);

            sut.Get("nk1", new DateTime(3000,12,31)).Should().Be(new DateTime(3000,12,31));
            sut.Get("nk2", 42).Should().Be(42);
            sut.Get("nk3", "a").Should().Be("a");
        }
    }
}