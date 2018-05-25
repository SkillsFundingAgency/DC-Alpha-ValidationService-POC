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
    public class LearnStartDate_02Tests
    {
        [Fact]
        public void ConditionMet_True()
        {
            var rule = new LearnStartDate_02Rule(null, null);

            rule.ConditionMet(new DateTime(2005, 1, 1), new DateTime(2017, 8, 1)).Should().BeTrue();
        }

        [Theory]
        [InlineData(2016, 1, 1)]
        [InlineData(2030, 1, 1)]
        public void ConditionMet_False(int year, int month, int day)
        {
            var rule = new LearnStartDate_02Rule(null, null);

            rule.ConditionMet(new DateTime(year, month, day), new DateTime(2017, 8, 1)).Should().BeFalse();
        }

        [Fact]
        public void Validate_NoErrors()
        {
            var learner = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2015, 1, 1),
                    }
                }
            };

            var validationDataMock = new Mock<IValidationData>();

            validationDataMock.SetupGet(vd => vd.AcademicYearStart).Returns(new DateTime(2017, 8, 1));

            var rule = new LearnStartDate_02Rule(validationDataMock.Object, null);

            rule.Validate(learner);
        }

        [Fact]
        public void Validate_Errors()
        {
            var learner = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2005, 1, 1),
                    }
                }
            };

            var validationDataMock = new Mock<IValidationData>();

            validationDataMock.SetupGet(vd => vd.AcademicYearStart).Returns(new DateTime(2017, 8, 1));

            var validationErrorHandlerMock = new Mock<IValidationErrorHandler<MessageLearner>>();

            Expression<Action<IValidationErrorHandler<MessageLearner>>> handle = veh => veh.Handle(learner, "LearnStartDate_02");

            validationErrorHandlerMock.Setup(handle);

            var rule = new LearnStartDate_02Rule(validationDataMock.Object, validationErrorHandlerMock.Object);

            rule.Validate(learner);

            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}
