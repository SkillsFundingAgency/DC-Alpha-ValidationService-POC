using System;
using System.Collections.Generic;
using Autofac;
using BusinessRules.POC.Configuration;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;
using Moq;
using Xunit;
using DCT.ILR.Model;

namespace BusinessRules.POC.Tests
{
    public class LearnerDelFamExclusionRulesValidatorUnitTests
    {
        private IContainer _container;

        public LearnerDelFamExclusionRulesValidatorUnitTests()
        {
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnerExclusionRulesReturnTrue_SoReturnTrue()
        {
            //arrange
            var learnerExclusionRulesMock = new Mock<ILearnerDelFam66ExclusionRuleForLearner>();
            learnerExclusionRulesMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(true);

            var learnDelFamExclusionRulesMock = new Mock<ILearnerDelFam66ExclusionRuleForFam>();
            learnDelFamExclusionRulesMock.Setup(x => x.Evaluate(It.IsAny<MessageLearnerLearningDeliveryLearningDeliveryFAM>())).Returns(false);


            ILearnerDelFam66ExclusionRule learnerDelFam66ExclusionRule =
                new LearnerDelFamExclusionRulesValidator(
                    new List<ILearnerDelFam66ExclusionRuleForFam>() {learnDelFamExclusionRulesMock.Object},
                    new List<ILearnerDelFam66ExclusionRuleForLearner>() {learnerExclusionRulesMock.Object});

            //act
            var actual = learnerDelFam66ExclusionRule.Evaluate(new MessageLearner()
            {
                DateOfBirth = new DateTime(2000, 06, 11),
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2017, 06, 11),
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
            });

            //assert
            Assert.True(actual);
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnerExclusionRulesReturnfalse_LearnDelFamExclRulesReturnsTrue_SoReturnTrue()
        {
            //arrange
            var learnerExclusionRulesMock = new Mock<ILearnerDelFam66ExclusionRuleForLearner>();
            learnerExclusionRulesMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(false);

            var learnDelFamExclusionRulesMock = new Mock<ILearnerDelFam66ExclusionRuleForFam>();
            learnDelFamExclusionRulesMock.Setup(x => x.Evaluate(It.IsAny<MessageLearnerLearningDeliveryLearningDeliveryFAM>())).Returns(true);


            ILearnerDelFam66ExclusionRule learnerDelFam66ExclusionRule =
                new LearnerDelFamExclusionRulesValidator(
                    new List<ILearnerDelFam66ExclusionRuleForFam>() { learnDelFamExclusionRulesMock.Object },
                    new List<ILearnerDelFam66ExclusionRuleForLearner>() { learnerExclusionRulesMock.Object });

            //act
            var actual = learnerDelFam66ExclusionRule.Evaluate(new MessageLearner()
            {
                DateOfBirth = new DateTime(2000, 06, 11),
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2017, 06, 11),
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
            });

            //assert
            Assert.True(actual);
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void BothExclusionRulesReturnsFalse_SoReturnsFalse()
        {
            //arrange
            var learnerExclusionRulesMock = new Mock<ILearnerDelFam66ExclusionRuleForLearner>();
            learnerExclusionRulesMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(false);

            var learnDelFamExclusionRulesMock = new Mock<ILearnerDelFam66ExclusionRuleForFam>();
            learnDelFamExclusionRulesMock.Setup(x => x.Evaluate(It.IsAny<MessageLearnerLearningDeliveryLearningDeliveryFAM>())).Returns(false);


            ILearnerDelFam66ExclusionRule learnerDelFam66ExclusionRule =
                new LearnerDelFamExclusionRulesValidator(
                    new List<ILearnerDelFam66ExclusionRuleForFam>() { learnDelFamExclusionRulesMock.Object },
                    new List<ILearnerDelFam66ExclusionRuleForLearner>() { learnerExclusionRulesMock.Object });

            //act
            var actual = learnerDelFam66ExclusionRule.Evaluate(new MessageLearner()
            {
                DateOfBirth = new DateTime(2000, 06, 11),
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnStartDate = new DateTime(2017, 06, 11),
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
            });

            //assert
            Assert.False(actual);
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact(Skip = "IOC")]
        public void BothExclusionRulesReturnsFalse_WithIoC_SoReturnsFalse()
        {
            //arrange
            if(_container == null) ConfigureIoCContainer();

            using (var scope = _container.BeginLifetimeScope())
            {
                var learnerExclusionRulesObj = scope.Resolve<IEnumerable<ILearnerDelFam66ExclusionRuleForLearner>>();
                var learnDelFamExclusionRulesObj = scope.Resolve<IEnumerable<ILearnerDelFam66ExclusionRuleForFam>>();

                ILearnerDelFam66ExclusionRule learnerDelFam66ExclusionRule =
                    new LearnerDelFamExclusionRulesValidator(learnDelFamExclusionRulesObj, learnerExclusionRulesObj);

                
                //act
                var actual = learnerDelFam66ExclusionRule.Evaluate(new MessageLearner()
                {
                    DateOfBirth = new DateTime(2000, 06, 11),
                    LearningDelivery = new MessageLearnerLearningDelivery[]
                    {
                        new MessageLearnerLearningDelivery()
                        {
                            LearnStartDate = new DateTime(2017, 06, 11),                            
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
                });

                //assert
                Assert.False(actual);
            }
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnerParamIsNull_SoReturnsFalse()
        {
            //arrange
            var learnerExclusionRulesMock = new Mock<ILearnerDelFam66ExclusionRuleForLearner>();
            learnerExclusionRulesMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(true);

            var learnDelFamExclusionRulesMock = new Mock<ILearnerDelFam66ExclusionRuleForFam>();
            learnDelFamExclusionRulesMock.Setup(x => x.Evaluate(It.IsAny<MessageLearnerLearningDeliveryLearningDeliveryFAM>())).Returns(false);


            ILearnerDelFam66ExclusionRule learnerDelFam66ExclusionRule =
                new LearnerDelFamExclusionRulesValidator(
                    new List<ILearnerDelFam66ExclusionRuleForFam>() {learnDelFamExclusionRulesMock.Object},
                    new List<ILearnerDelFam66ExclusionRuleForLearner>() {learnerExclusionRulesMock.Object});

            //act
            var actual = learnerDelFam66ExclusionRule.Evaluate(It.IsAny<MessageLearner>());

            //assert
            Assert.False(actual);
        }

        private void ConfigureIoCContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<BusinessLogicAutofacModule>();
            _container = builder.Build();

        }

    }
}
