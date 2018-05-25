using BusinessRules.POC.ULN;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class ULN_05Tests
    {
        [Fact]
        public void ConditionMet_True()
        {
            var rule = new ULN_05Rule(null, null);

            rule.ConditionMet(1, true).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_Uln9s()
        {
            var rule = new ULN_05Rule(null, null);

            rule.ConditionMet(9999999999, true).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False_NullReferenceData()
        {
            var rule = new ULN_05Rule(null, null);

            rule.ConditionMet(1, false).Should().BeFalse();
        }
    }
}
