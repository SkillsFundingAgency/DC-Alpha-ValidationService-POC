using BusinessRules.POC.FileData.Interface;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ULN;
using DCT.ILR.Model;
using FluentAssertions;
using Moq;
using System;
using System.Linq.Expressions;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class ULN_03Tests
    {
        [Fact]
        public void Exclude_True()
        {
            var rule = new ULN_03Rule(null, null);

            var learningDelivery = new MessageLearnerLearningDelivery()
            {
                LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = "ACT",
                        LearnDelFAMCode = "1"
                    }
                }
            };

            rule.Exclude(learningDelivery).Should().BeTrue();
        }

        [Fact]
        public void Exclude_False_Null()
        {
            var rule = new ULN_03Rule(null, null);

            var learningDelivery = new MessageLearnerLearningDelivery()
            {
                LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = "No",
                        LearnDelFAMCode = "2"
                    }
                }
            };

            rule.Exclude(learningDelivery).Should().BeFalse();
        }

        [Fact]
        public void Exclude_False_NoMatch()
        {
            var rule = new ULN_03Rule(null, null);

            var learningDelivery = new MessageLearnerLearningDelivery()
            {
                LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                    {
                        LearnDelFAMType = "ACT",
                        LearnDelFAMCode = "2"
                    }
                }
            };

            rule.Exclude(learningDelivery).Should().BeFalse();
        }

        [Theory]
        [InlineData(25)]
        [InlineData(82)]
        [InlineData(35)]
        [InlineData(36)]
        [InlineData(81)]
        [InlineData(70)]        
        public void ConditionMet_True(long fundModel)
        {
            var rule = new ULN_03Rule(null, null);

            rule.ConditionMet(fundModel, 9999999999, new DateTime(1970, 1, 1)).Should().BeTrue();
        }

        [Fact]
        public void ConditionMet_False_Uln()
        {
            var rule = new ULN_03Rule(null, null);

            rule.ConditionMet(25, 1, new DateTime(1970, 1, 1)).Should().BeFalse();                        
        }

        [Fact]
        public void ConditionMet_False_FundModel()
        {
            var rule = new ULN_03Rule(null, null);

            rule.ConditionMet(1, 9999999999, new DateTime(1970, 1, 1)).Should().BeFalse();
        }

        [Fact]
        public void ConditionMet_False_FilePreparationDate()
        {
            var rule = new ULN_03Rule(null, null);

            rule.ConditionMet(25, 9999999999, new DateTime(2030, 1, 1)).Should().BeFalse();
        }

        [Fact]
        public void Validate_NoErrors()
        {
            var fileDataMock = new Mock<IFileData>();
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler<MessageLearner>>();

            fileDataMock.SetupGet(fd => fd.FilePreparationDate).Returns(new DateTime(1970, 1, 1));

            var rule = new ULN_03Rule(fileDataMock.Object, validationErrorHandlerMock.Object);

            var messageLearner = new MessageLearner()
            {
                ULN = 1,
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        FundModel = 2,
                    }
                }
            };

            rule.Validate(messageLearner);
        }

        [Fact]
        public void Validate_Errors()
        {
            var fileDataMock = new Mock<IFileData>();
            var validationErrorHandlerMock = new Mock<IValidationErrorHandler<MessageLearner>>();

            fileDataMock.SetupGet(fd => fd.FilePreparationDate).Returns(new DateTime(1970, 1, 1));

            var messageLearner = new MessageLearner()
            {
                ULN = 9999999999,
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        FundModel = 25,
                    },
                    new MessageLearnerLearningDelivery()
                    {
                        FundModel = 36,
                    }
                }
            };

            Expression<Action<IValidationErrorHandler<MessageLearner>>> handle = veh => veh.Handle(messageLearner, "ULN_03");

            validationErrorHandlerMock.Setup(handle);

            var rule = new ULN_03Rule(fileDataMock.Object, validationErrorHandlerMock.Object);
            rule.Validate(messageLearner);

            validationErrorHandlerMock.Verify(handle, Times.Exactly(2));
        }        
    }
}
