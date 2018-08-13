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
            => people.GetEmployee(employeeId).Bind((v) => v.WorkPermit).Where(IsActive);

        private static bool IsActive(WorkPermit permit) => permit.Expiry > DateTime.Now;

        public static Option<double> AverageYearsWorkedAtTheCompany(this IEnumerable<Employee> employees)
        {
            var years = employees.Bind((e) => e.LeftOn.Map((d) => GetDiffInYear(e.JoinedOn, d))).ToList();

            return years.Any() ? years.Average() : new Option<double>();
        }

        private static double GetYearsWorked(this Employee employee) =>
            employee.LeftOn.Match(
                () => GetDiffInYear(employee.JoinedOn, DateTime.Now),
                (v) => GetDiffInYear(employee.JoinedOn, v)
            );

        private static double GetDiffInYear(DateTime from, DateTime to) => (to - from).Days / 365.0;
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
        public void should_return_WorkPermit()
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
        public void ayw_should_return_none_for_empty_list()
        {
            var sut = new List<Employee>();
            sut.AverageYearsWorkedAtTheCompany().Should().Be(new Option<double>());
        }

        [Fact]
        public void ayw_should_return_none_for_list_with_working_employees()
        {
            var sut = new List<Employee>
            {
                new Employee {JoinedOn = new DateTime(2000, 1, 1)},
                new Employee {JoinedOn = new DateTime(2006, 1, 1)},
                new Employee {JoinedOn = new DateTime(3006, 1, 1)},
            };
            sut.AverageYearsWorkedAtTheCompany().Should().Be(new Option<double>());
        }

        [Fact]
        public void ayw_should_return_1_for_one_element_list()
        {
            var sut = new List<Employee> {new Employee {JoinedOn = new DateTime(2000, 1, 1), LeftOn = new DateTime(2010, 1, 1)}};
            (sut.AverageYearsWorkedAtTheCompany().AsEnumerable().Single() - 10).Should().BeInRange(-0.01, 0.01);
        }
        
        [Fact]
        public void ayw_should_return_6_for_three_element_list()
        {
            var sut = new List<Employee>
            {
                new Employee {JoinedOn = new DateTime(2000, 1, 1), LeftOn = new DateTime(2010, 1, 1)},
                new Employee {JoinedOn = new DateTime(2006, 1, 1), LeftOn = new DateTime(2010, 1, 1)},
                new Employee {JoinedOn = new DateTime(3006, 1, 1), LeftOn = new DateTime(3010, 1, 1)},
            };
            (sut.AverageYearsWorkedAtTheCompany().AsEnumerable().Single() - 6).Should().BeInRange(-0.01, 0.01);
        }

        [Fact]
        public void ayw_should_consider_only_employees_that_left()
        {
            var sut = new List<Employee>
            {
                new Employee {JoinedOn = new DateTime(2000, 1, 1)},
                new Employee {JoinedOn = new DateTime(2006, 1, 1)},
                new Employee {JoinedOn = new DateTime(3000, 1, 1), LeftOn = new DateTime(3010, 1, 1)},
            };
            (sut.AverageYearsWorkedAtTheCompany().AsEnumerable().Single() - 10).Should().BeInRange(-0.01, 0.01);
        }
    }
}