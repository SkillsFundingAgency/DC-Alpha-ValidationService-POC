using BusinessRules.POC.DerivedData.Interface;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ULN;
using DCT.ILR.Model;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class ULN_04Tests
    {     
        [Fact]
        public void ConditionMet_True()
        {
            var dd01Mock = new Mock<IDD01>();

            dd01Mock.Setup(dd => dd.Derive(1000000043)).Returns("N");

            var rule = new ULN_04Rule(dd01Mock.Object, null);

            rule.ConditionMet(1000000043).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False()
        {
            var dd01Mock = new Mock<IDD01>();

            dd01Mock.Setup(dd => dd.Derive(1000000004)).Returns("4");

            var rule = new ULN_04Rule(dd01Mock.Object, null);

            rule.ConditionMet(1000000004).Should().BeFalse();
        }

        [Fact]
        public void Validate_NoErrors()
        {
            var learner = new MessageLearner()
            {
                ULN = 1000000043,
            };

            var dd01Mock = new Mock<IDD01>();

            dd01Mock.Setup(dd => dd.Derive(1000000043)).Returns("Y");

            var rule = new ULN_04Rule(dd01Mock.Object, null);

            rule.Validate(learner);
        }

        [Fact]
        public void Validate_Error()
        {
            var learner = new MessageLearner()
            {
                ULN = 1000000042,
            };

            var dd01Mock = new Mock<IDD01>();

            dd01Mock.Setup(dd => dd.Derive(1000000042)).Returns("N");

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler<MessageLearner>>();            

            Expression<Action<IValidationErrorHandler<MessageLearner>>> handle = veh => veh.Handle(learner, "ULN_04");

            validationErrorHandlerMock.Setup(handle);

            var rule = new ULN_04Rule(dd01Mock.Object, validationErrorHandlerMock.Object);

            rule.Validate(learner);

            validationErrorHandlerMock.Verify(handle, Times.Exactly(1));
        }
    }
}
