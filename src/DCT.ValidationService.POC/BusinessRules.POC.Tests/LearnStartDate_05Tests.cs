using BusinessRules.POC.Interfaces;
using BusinessRules.POC.LearnStartDate;
using BusinessRules.POC.ValidationData.Interface;
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
    public class LearnStartDate_05Tests
    {
        [Fact]
        public void ConditionMet_True()
        {
            var rule = new LearnStartDate_05Rule(null);

            rule.ConditionMet(new DateTime(2018, 1, 1), new DateTime(2017, 8, 1)).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_NullDateOfBirth()
        {
            var rule = new LearnStartDate_05Rule(null);

            rule.ConditionMet(null, new DateTime(2017, 8, 1)).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False_DateOfBirth()
        {
            var rule = new LearnStartDate_05Rule(null);

            rule.ConditionMet(new DateTime(1988, 2, 10), new DateTime(2017, 8, 1)).Should().BeFalse();
        }

        [Fact]
        public void Validate_NoErrors()
        {
            var learner = new MessageLearner()
            {
                DateOfBirth = new DateTime(1988, 2, 10),
                DateOfBirthSpecified = true,
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2015, 1, 1),
                    }
                }
            };
            
            var rule = new LearnStartDate_05Rule(null);

            rule.Validate(learner);
        }

        [Fact]
        public void Validate_Errors()
        {
            var learner = new MessageLearner()
            {
                DateOfBirth = new DateTime(2018, 1, 1),
                DateOfBirthSpecified = true,
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2005, 1, 1),
                    }
                }
            };
                        
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler<MessageLearner>>();

            Expression<Action<IValidationErrorHandler<MessageLearner>>> handle = veh => veh.Handle(learner, "LearnStartDate_05");

            validationErrorHandlerMock.Setup(handle);

            var rule = new LearnStartDate_05Rule(validationErrorHandlerMock.Object);

            rule.Validate(learner);

            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}
