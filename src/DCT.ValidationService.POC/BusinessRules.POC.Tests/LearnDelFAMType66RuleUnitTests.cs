using BusinessRules.POC.Interfaces;
using BusinessRules.POC.RuleLearnDelFAMType66;
using Moq;
using System;
using System.Collections.Generic;
using Autofac;
using BusinessRules.POC.Configuration;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;
using Xunit;
using DCT.ValidationService.Service;
using DCT.ILR.Model;
using System.Linq.Expressions;

namespace BusinessRules.POC.Tests
{
    public class LearnDelFAMType66RuleUnitTests
    {
        private IContainer _container;
        private ILifetimeScope _scope;

        public LearnDelFAMType66RuleUnitTests()
        {

        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnDelFAMType66AgeLessThan24_ReturnsTrue()
        {

            //arrange
            var learnerDobNotNullMock = new Mock<ISharedRule<MessageLearner, bool>>();
            learnerDobNotNullMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>()))
                .Returns(true);

            var fetchSpecificFundModelMock = new Mock<IFetchSpecificFundModelsLDsWithLearnStartDate>();
            fetchSpecificFundModelMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>()))
                .Returns(new MessageLearner()
                {
                    LearningDelivery = new MessageLearnerLearningDelivery[]
                    {
                        new MessageLearnerLearningDelivery()
                        {
                        }
                    }
                });

            var checkAgeLimitMock = new Mock<IPickValidLdsWithAgeLimitFamTypeAndCode>();
            checkAgeLimitMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(It.IsAny<List<MessageLearnerLearningDelivery>>());

            var learnerDelFam66ExclusionRuleMock = new Mock<ILearnerDelFam66ExclusionRule>();
            learnerDelFam66ExclusionRuleMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(false);

            var learnerValidationErrorHandlerMock = new Mock<IValidationErrorHandler<MessageLearner>>();
            
            var validator = new LearnDelFAMType66Validator(learnerDobNotNullMock.Object, 
                fetchSpecificFundModelMock.Object, checkAgeLimitMock.Object, learnerDelFam66ExclusionRuleMock.Object, null);

            //act
            validator.Validate(new MessageLearner());

            //assert
        }

        [Fact]
        [Trait("Category", "LearnDelFAMType66-Rule")]
        public void LearnDelFAMType66LDsFundModelIsNot35_ReturnsTrue()
        {
            //arrange
            var learnerDobNotNullMock = new Mock<ISharedRule<MessageLearner, bool>>();
            learnerDobNotNullMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>()))
                .Returns(false);

            var fetchSpecificFundModelMock = new Mock<IFetchSpecificFundModelsLDsWithLearnStartDate>();
            fetchSpecificFundModelMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>()))
                .Returns(new MessageLearner() { LearningDelivery = new MessageLearnerLearningDelivery[0] });

            var checkAgeLimitMock = new Mock<IPickValidLdsWithAgeLimitFamTypeAndCode>();
            checkAgeLimitMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(It.IsAny<List<MessageLearnerLearningDelivery>>());

            var learnerDelFam66ExclusionRuleMock = new Mock<ILearnerDelFam66ExclusionRule>();
            learnerDelFam66ExclusionRuleMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(false);
            
            var validator = new LearnDelFAMType66Validator(learnerDobNotNullMock.Object,
                fetchSpecificFundModelMock.Object, checkAgeLimitMock.Object, learnerDelFam66ExclusionRuleMock.Object, null);

            //act
            validator.Validate(new MessageLearner());

            //assert
        }

       

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnDelFAMType66LearnersAgeLessthan25_ReturnsTrue()
        {
            //arrange
            var learnerDobNotNullMock = new Mock<ISharedRule<MessageLearner, bool>>();
            learnerDobNotNullMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>()))
                .Returns(false);

            var fetchSpecificFundModelMock = new Mock<IFetchSpecificFundModelsLDsWithLearnStartDate>();
            fetchSpecificFundModelMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>()))
                .Returns(new MessageLearner() { LearningDelivery = new MessageLearnerLearningDelivery[] { new MessageLearnerLearningDelivery() } });

            var checkAgeLimitMock = new Mock<IPickValidLdsWithAgeLimitFamTypeAndCode>();
            checkAgeLimitMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(new List<MessageLearnerLearningDelivery>());

            var learnerDelFam66ExclusionRuleMock = new Mock<ILearnerDelFam66ExclusionRule>();
            learnerDelFam66ExclusionRuleMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(false);

            var validator = new LearnDelFAMType66Validator(learnerDobNotNullMock.Object,
                fetchSpecificFundModelMock.Object, checkAgeLimitMock.Object, learnerDelFam66ExclusionRuleMock.Object, null);

            //act
            validator.Validate(new MessageLearner());


            //assert
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact]
        public void LearnDelFAMType66_LearnersAgeGreaterThan25_And_LARsNVQ_Valid_ReturnsFalse()
        {
            //arrange
            var learnerDobNotNullMock = new Mock<ISharedRule<MessageLearner, bool>>();
            learnerDobNotNullMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>()))
                .Returns(false);

            var fetchSpecificFundModelMock = new Mock<IFetchSpecificFundModelsLDsWithLearnStartDate>();
            fetchSpecificFundModelMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>()))
                .Returns(new MessageLearner() { LearningDelivery = new MessageLearnerLearningDelivery[] { new MessageLearnerLearningDelivery() } });

            var checkAgeLimitMock = new Mock<IPickValidLdsWithAgeLimitFamTypeAndCode>();
            checkAgeLimitMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(new List<MessageLearnerLearningDelivery>()
            {
                new MessageLearnerLearningDelivery()
                {
                    FundModel = 35
                }
            });

            var learnerValidationErrorHandlerMock = new Mock<IValidationErrorHandler<MessageLearner>>();

            Expression<Action<IValidationErrorHandler<MessageLearner>>> handle = veh => veh.Handle(It.IsAny<MessageLearner>(), RuleNameConstants.LearnDelFam66);
            learnerValidationErrorHandlerMock.Setup(handle);
            
            var learnerDelFam66ExclusionRuleMock = new Mock<ILearnerDelFam66ExclusionRule>();
            learnerDelFam66ExclusionRuleMock.Setup(x => x.Evaluate(It.IsAny<MessageLearner>())).Returns(false);
            
            var validator = new LearnDelFAMType66Validator(learnerDobNotNullMock.Object,
                fetchSpecificFundModelMock.Object, checkAgeLimitMock.Object, learnerDelFam66ExclusionRuleMock.Object, learnerValidationErrorHandlerMock.Object);

            //act
            validator.Validate(new MessageLearner());                        
            
        }

        [Trait("Category", "LearnDelFAMType66-Rule")]
        [Fact(Skip = "IOC")]
        public void LearnDelFAMType66_LearnersAgeGreaterThan25_And_LARsNVQ_Valid_WithIoC_ReturnsFalse()
        {
            //arrange
            if(_container == null) ConfigureIoCContainer();
            using (var scope = _container.BeginLifetimeScope())
            {
                //var validator1 = new LearnDelFAMType66Validator(scope.ResolveKeyed<ISharedRule<Learner, bool>>(SharedRuleNames.LearnerDobShouldNotBeNull),
                //    );
                var learnerDobNotNull =
                    scope.ResolveKeyed<ISharedRule<MessageLearner, bool>>(RuleNameConstants.LearnerDoBShouldNotBeNull);
                var fetchSpecificFundModel = scope.Resolve<IFetchSpecificFundModelsLDsWithLearnStartDate>();

                var checkAgeLimit = scope.Resolve<IPickValidLdsWithAgeLimitFamTypeAndCode>();
                var learnerDelFam66ExclusionRule = scope.Resolve<ILearnerDelFam66ExclusionRule>();

                var validator = new LearnDelFAMType66Validator(learnerDobNotNull, fetchSpecificFundModel, checkAgeLimit, learnerDelFam66ExclusionRule, null);

                //act
                validator.Validate(new MessageLearner()
                {
                    DateOfBirth = new DateTime(1982, 01, 01),
                    LearningDelivery = new MessageLearnerLearningDelivery[]
                    {
                        new MessageLearnerLearningDelivery()
                        {
                            LearnAimRef = "22",
                            FundModel = 36,
                            LearnStartDate = new DateTime(2016, 05, 5)
                        },
                        new MessageLearnerLearningDelivery()
                        {
                            FundModel = 35,
                            LearnAimRef = "Z0007835",
                            LearnStartDate = new DateTime(2017, 06, 11),
                            LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                            {
                                new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                                {
                                    LearnDelFAMCode = "1",
                                    LearnDelFAMType = "FFI"
                                }
                            }
                        }
                    },
                    LearnerEmploymentStatus = new MessageLearnerLearnerEmploymentStatus[]
                    {
                        new MessageLearnerLearnerEmploymentStatus()
                        {
                            DateEmpStatApp = new DateTime(2015, 01, 01),
                            EmpId = 1111,
                            EmpStat = 15,
                            EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[0]
                        }
                    }
                });                
            }
        }

        private void ConfigureIoCContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<BusinessLogicAutofacModule>();
            builder.RegisterModule<ValidationServiceServiceModule>();
            _container = builder.Build();
            _scope = _container.BeginLifetimeScope();
        }
    }
}
