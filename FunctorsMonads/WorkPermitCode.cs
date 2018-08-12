using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LaYumba.Functional;
using Xunit;

namespace FunctorsMonads
{
    public static class WorkPermitExtensions
    {
        private static Option<Employee> GetEmployee(this IReadOnlyDictionary<string, Employee> people, string employeeId)
            => people.ContainsKey(employeeId) ? people[employeeId] : null;

        public static Option<WorkPermit> GetWorkPermit(this Dictionary<string, Employee> people, string employeeId)
            => people.GetEmployee(employeeId).Bind((v) => v.WorkPermit).Match(
                () => new Option<WorkPermit>(),
                (v) => v.Expiry > DateTime.Now ? v : new Option<WorkPermit>()
            );

        public static double AverageYearsWorkedAtTheCompany(this List<Employee> employees)
        {
            return employees.Any() ? employees.First().GetYearsWorked() : 0;
        }

        public static double GetYearsWorked(this Employee employee) =>
            employee.LeftOn.Match(
                () => (DateTime.Now - employee.JoinedOn).Days / 365,
                (v) => (v - employee.JoinedOn).Days / 365
            );
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

        [Fact]
        public void should_return_none_when_not_present()
        {
            var sut = new Dictionary<string, Employee> {{"id", new Employee { WorkPermit = new WorkPermit{ Number = "42"}}}};
            var actual = sut.GetWorkPermit("wrong_key");
            actual.Should().Be(new Option<WorkPermit>());
        }

        [Fact]
        public void should_return_none_when_expired()
        {
            var sut = new Dictionary<string, Employee>
                {{"id", new Employee {WorkPermit = new WorkPermit {Number = "42", Expiry = new DateTime(2000, 1, 1)}}}};
            var actual = sut.GetWorkPermit("id");
            actual.Should().Be(new Option<WorkPermit>());
        }

        [Fact]
        public void should_return_workpermit()
        {
            var sut = new Dictionary<string, Employee>
            {
                {"id1", new Employee { WorkPermit = new WorkPermit{ Number = "1", Expiry = new DateTime(3000, 1, 1)}}},
                {"id2", new Employee { WorkPermit = new WorkPermit{ Number = "42", Expiry = new DateTime(3000, 1, 1)}}},
                {"id3", new Employee { WorkPermit = new WorkPermit{ Number = "102", Expiry = new DateTime(3000, 1, 1)}}},
            };
            var actual = sut.GetWorkPermit("id2").AsEnumerable().Single();
            actual.Number.Should().Be("42");
        }

        [Fact]
        public void ayw_should_return_0_for_empty_list()
        {
            var sut = new List<Employee>();
            sut.AverageYearsWorkedAtTheCompany().Should().Be(0);
        }

        [Fact]
        public void ayw_should_return_1_for_oneelement_list()
        {
            var sut = new List<Employee> {new Employee {JoinedOn = new DateTime(2000, 1, 1), LeftOn = new DateTime(2010, 1, 1)}};
            sut.AverageYearsWorkedAtTheCompany().Should().Be(10);
        }

        [Fact]
        public void ayw_should_return_1_for_oneelement_list_still_working()
        {
            var sut = new List<Employee> {new Employee {JoinedOn = DateTime.Now.AddYears(-10)}};
            sut.AverageYearsWorkedAtTheCompany().Should().Be(10);
        }
    }
}