using BusinessRules.POC.Helpers;
using BusinessRules.POC.Helpers.Interface;
using BusinessRules.POC.RuleLearnDelFAMType66;
using DCT.ILR.Model;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class CheckAgeLimitAndFAMTypeAndCodeUnitTests
    {
        private PickValidLdsWithAgeLimitFamTypeAndCode _rule;

        public CheckAgeLimitAndFAMTypeAndCodeUnitTests()
        {
        }

        [Fact]
        [Trait("Category", "LearnDelFAMType66-Rule")]
        public void AgeLessThan24ReturnsZeroItems()
        {
            var validateLARSNVQMock = new Mock<IValidateLARNotionalNVQLevelRule>();
            validateLARSNVQMock.Setup(x => x.Evaluate(It.IsAny<MessageLearnerLearningDelivery>())).Returns(true);

            var dateHelperMock = new Mock<IDateHelper>();
            dateHelperMock.Setup(x => x.GetAge(It.IsAny<DateTime>(), It.IsAny<DateTime?>())).Returns(23);

            _rule = new PickValidLdsWithAgeLimitFamTypeAndCode(dateHelperMock.Object, validateLARSNVQMock.Object);

            var parameter = new MessageLearner()
            {
                DateOfBirth = new DateTime(2000, 06, 11),
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2017, 06,11),
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "EEI"
                            }
                        }
                    }
                }
            };


            var actual = _rule.Evaluate(parameter);
            Assert.Empty(actual);

        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void AgeGreaterThan24_Returns_OneItems()
        {
            var validateLARSNVQMock = new Mock<IValidateLARNotionalNVQLevelRule>();
            validateLARSNVQMock.Setup(x => x.Evaluate(It.IsAny<MessageLearnerLearningDelivery>())).Returns(true);

            var dateHelperMock = new Mock<IDateHelper>();
            dateHelperMock.Setup(x => x.GetAge(It.IsAny<DateTime>(), It.IsAny<DateTime?>())).Returns(25);

            _rule = new PickValidLdsWithAgeLimitFamTypeAndCode(dateHelperMock.Object, validateLARSNVQMock.Object);
                        
            var parameter = new MessageLearner()
            {
                DateOfBirth = new DateTime(2000, 06,11),
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2017, 06,11),
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "FFI"
                            }
                        }
                    }
                }
            };


            var actual = _rule.Evaluate(parameter);
            Assert.NotEmpty(actual);
            Assert.Single(actual);

        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void AgeGreaterThan24_But_WrongFAMCode_Returns_ZeroItems()
        {
            var validateLARSNVQMock = new Mock<IValidateLARNotionalNVQLevelRule>();
            validateLARSNVQMock.Setup(x => x.Evaluate(It.IsAny<MessageLearnerLearningDelivery>())).Returns(true);

            var dateHelperMock = new Mock<IDateHelper>();
            dateHelperMock.Setup(x => x.GetAge(It.IsAny<DateTime>(), It.IsAny<DateTime?>())).Returns(25);

            _rule = new PickValidLdsWithAgeLimitFamTypeAndCode(dateHelperMock.Object, validateLARSNVQMock.Object);

            var parameter = new MessageLearner()
            {
                DateOfBirth = new DateTime(2000, 06,11),
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2017, 06,11),

                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "FFI"
                            }
                        }
                    }
                }
            };

            var actual = _rule.Evaluate(parameter);
            Assert.Empty(actual);
        }


    }
}
