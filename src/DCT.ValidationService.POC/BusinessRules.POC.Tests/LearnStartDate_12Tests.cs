using BusinessRules.POC.Interfaces;
using BusinessRules.POC.LearnStartDate;
using BusinessRules.POC.SharedRules;
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
    public class LearnStartDate_12Tests
    {
        [Fact]
        public void ConditionMet_True()
        {
            var rule = new LearnStartDate_12Rule(null, null, null);

            rule.ConditionMet(new DateTime(2018, 8, 1), new DateTime(2016, 7, 31), "Y").Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_LearnStartDate_WithinYear()
        {
            var rule = new LearnStartDate_12Rule(null, null, null);

            rule.ConditionMet(new DateTime(2019, 1, 1), new DateTime(2018, 7, 31), "Y").Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False_LearnStartDate_BeforeAcademicYearEnd()
        {
            var rule = new LearnStartDate_12Rule(null, null, null);

            rule.ConditionMet(new DateTime(2017, 1, 1), new DateTime(2018, 7, 31), "Y").Should().BeFalse();
        }
        
        [Fact]
        public void ConditionMet_False_DD07()
        {
            var rule = new LearnStartDate_12Rule(null, null, null);

            rule.ConditionMet(new DateTime(2018, 8, 1), new DateTime(2016, 7, 31), "N").Should().BeFalse();
        }

        [Fact]
        public void Validate_NoErrors()
        {
            var learningDelivery = new MessageLearnerLearningDelivery()
            {
                LearnStartDate = new DateTime(2017, 1, 1),
            };

            var learner = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    learningDelivery
                }
            };

            var validationDataMock = new Mock<IValidationData>();
            var dd07Mock = new Mock<IDD07Rule>();

            validationDataMock.SetupGet(vd => vd.AcademicYearEnd).Returns(new DateTime(2017, 8, 1));
            dd07Mock.Setup(dd => dd.Evaluate(learningDelivery)).Returns("Y");

            var rule = new LearnStartDate_12Rule(dd07Mock.Object, validationDataMock.Object, null);

            rule.Validate(learner);
        }

        [Fact]
        public void Validate_Errors()
        {
            var learningDelivery = new MessageLearnerLearningDelivery()
            {
                LearnStartDate = new DateTime(2020, 1, 1),
                ProgType = 1
            };

            var learner = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    learningDelivery,
                }
            };

            var validationDataMock = new Mock<IValidationData>();
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler<MessageLearner>>();
            var dd07Mock = new Mock<IDD07Rule>();

            validationDataMock.SetupGet(vd => vd.AcademicYearEnd).Returns(new DateTime(2018, 7, 31));
            dd07Mock.Setup(dd => dd.Evaluate(learningDelivery)).Returns("Y");

            Expression<Action<IValidationErrorHandler<MessageLearner>>> handle = veh => veh.Handle(learner, "LearnStartDate_12");

            validationErrorHandlerMock.Setup(handle);

            var rule = new LearnStartDate_12Rule(dd07Mock.Object, validationDataMock.Object, validationErrorHandlerMock.Object);

            rule.Validate(learner);

            validationErrorHandlerMock.Verify(handle, Times.Once);
        }
    }
}
