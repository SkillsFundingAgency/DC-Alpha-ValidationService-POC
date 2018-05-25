using BusinessRules.POC.DateOfBirth;
using FluentAssertions;
using System;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class DateOfBirth_48Tests
    {
        [Fact]
        public void Exclude_True()
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.Exclude(25).Should().BeTrue();
        }

        [Fact]
        public void Exclude_False()
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.Exclude(24).Should().BeFalse();
        }

        [Fact]
        public void LearnerConditionMet_True()
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.LearnerConditionMet(new DateTime(2018, 1, 1)).Should().BeTrue();
        }

        [Fact]        
        public void LearnerConditionMet_False()
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.LearnerConditionMet(null).Should().BeFalse();
        }

        [Fact]
        public void DD04ConditionMet_True()
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.DD04ConditionMet(new DateTime(2017, 12, 1), new DateTime(2017, 8, 1), new DateTime(2018, 6, 1)).Should().BeTrue();
        }

        [Theory]
        [InlineData(2015, 1, 1)]
        [InlineData(2019, 1, 1)]
        public void DD04ConditionMet_False(int year, int month, int day)
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.DD04ConditionMet(new DateTime(year, month, day), new DateTime(2017, 8, 1), new DateTime(2018, 6, 1)).Should().BeFalse();
        }

        [Fact]
        public void DD04ConditionMet_Null()
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.DD04ConditionMet(null, new DateTime(2017, 8, 1), new DateTime(2018, 6, 1)).Should().BeFalse();
        }

        [Fact]
        public void DD07ConditionMet_True()
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.DD07ConditionMet("Y").Should().BeTrue();
        }

        [Theory]
        [InlineData("N")]
        [InlineData("AnythingElse")]
        public void DD07ConditionMet_False(string dd07)
        {
            var rule = new DateOfBirth_48Rule(null, null, null, null);

            rule.DD07ConditionMet(dd07).Should().BeFalse();
        }           
    }
}
