using System;
using System.Collections.Generic;
using System.Configuration;
using FluentAssertions;
using LaYumba.Functional;
using Xunit;

namespace FunctorsMonads
{
    public static class WorkPermitExtensions
    {
        public static Option<WorkPermit> GetWorkPermit(this Dictionary<string, Employee> people, string employeeId)
        {
            return null;
        }
    }

    public class Employee
    {
        public string Id { get; set; }
        public Option<WorkPermit> WorkPermit { get; set; }
        public DateTime JoinedOn { get; set; }
        public Option<DateTime> LeftOn { get; set; }
    }

    public class WorkPermit
    {
        public string Number { get; set; }
        public DateTime Expiry { get; set; }
    }





    public class WorkPermitExtensionsTest
    {
        [Fact]
        public void should_return_none_when_empty()
        {
            var sut = new Dictionary<string, Employee>();
            var actual = sut.GetWorkPermit("id");
            actual.Should().Be(new Option<WorkPermit>());
        }

    }
}