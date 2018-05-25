using BusinessRules.POC.Helpers;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.RuleDOB48;
using BusinessRules.POC.SharedRules;
using DCT.ILR.Model;
using DCT.ValidationService.Service.Implementation;
using System;
using Xunit;

namespace BusinessRules.POC.Tests
{
    public class DoB48UnitTests
    {
        private DoB48Validator _dob48Validator;

        public DoB48UnitTests()
        {
            _dob48Validator = new DoB48Validator(new DD07IsYRule(new DD07Rule(new ValidationData.ValidationData())),
                new LearnerDoBShouldNotBeNullRule(), new DD04IsInRangeRule(new DD04Rule(), new ReferenceDataFromSettingsFile(), new DateHelper()), new IsLearnerBelowSchoolAge(new DateHelper()), new LearnerValidationErrorHandler());

        }

        [Trait("Category", "DoB48-Rule")]
        [Fact]
        public void DoB48RuleForLearnerDoBIsnotNullSuccess()
        {
            //arrange          
            var ldObj = new MessageLearner()
            {
                DateOfBirth = new DateTime(1989, 11, 01),
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 1,
                        LearnAimRef = "ZPROG001",
                        LearnStartDate = new DateTime(2011, 05, 15)
                    },
                     new MessageLearnerLearningDelivery()
                    {
                        AimType = 2,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 1,
                        LearnAimRef = "60005623",
                        LearnStartDate = new DateTime(2011, 05, 15)
                    },
                      new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 3,
                        LearnAimRef = "ZPROG001",
                        LearnStartDate = new DateTime(2011, 06, 15)
                    }, new MessageLearnerLearningDelivery()
                    {
                        AimType = 5,
                        FworkCode = 548,
                        ProgType = 2,
                        PwayCode = 3,
                        AimSeqNumber = 100,
                        LearnAimRef = "sdf asdf",
                        LearnStartDate = new DateTime(2011, 05, 15)
                    }, new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        FworkCode = 546,
                        ProgType = 2,
                        PwayCode = 1,
                        AimSeqNumber = 100,
                        LearnAimRef = "189213",
                        LearnStartDate = new DateTime(2012, 08, 21)
                    }
                }
            };

            //act
            var results = _dob48Validator.Validate(ldObj);

            //assert
            Assert.True(results);
        }

        [Trait("Category", "DoB48-Rule")]
        [Fact]
        public void DoB48RuleForDoBIsNullSuccess()
        {
            //arrange          
            var ldObj = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 1,
                        LearnAimRef = "ZPROG001",
                        LearnStartDate = new DateTime(2011, 05, 15)
                    },
                     new MessageLearnerLearningDelivery()
                    {
                        AimType = 2,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 1,
                        LearnAimRef = "60005623",
                        LearnStartDate = new DateTime(2011, 05, 15)
                    },
                      new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 3,
                        LearnAimRef = "ZPROG001",
                        LearnStartDate = new DateTime(2011, 06, 15)
                    }, new MessageLearnerLearningDelivery()
                    {
                        AimType = 5,
                        FworkCode = 548,
                        ProgType = 2,
                        PwayCode = 3,
                        AimSeqNumber = 100,
                        LearnAimRef = "sdf asdf",
                        LearnStartDate = new DateTime(2011, 05, 15)
                    }, new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        FworkCode = 546,
                        ProgType = 2,
                        PwayCode = 1,
                        AimSeqNumber = 100,
                        LearnAimRef = "189213",
                        LearnStartDate = new DateTime(2012, 08, 21)
                    }
                }
            };

            //act
            var results = _dob48Validator.Validate(ldObj);

            //assert
            Assert.True(results);
        }


        [Trait("Category", "DoB48-Rule")]
        [Fact]
        public void DoB48RuleWhenLearnerChangesTurns16AndProgramStartsAfter1Aug()
        {
            //arrange          
            var ldObj = new MessageLearner()
            {
                DateOfBirth = new DateTime(2001, 12, 09),
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 1,
                        LearnAimRef = "ZPROG001",
                        LearnStartDate = new DateTime(2017, 09, 15)
                    },
                     new MessageLearnerLearningDelivery()
                    {
                        AimType = 2,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 1,
                        LearnAimRef = "60005623",
                        LearnStartDate = new DateTime(2017, 09, 15)
                    },
                      new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        AimSeqNumber = 100,
                        FworkCode = 549,
                        ProgType = 2,
                        PwayCode = 3,
                        LearnAimRef = "ZPROG001",
                        LearnStartDate = new DateTime(2017, 06, 15)
                    }, new MessageLearnerLearningDelivery()
                    {
                        AimType = 5,
                        FworkCode = 548,
                        ProgType = 2,
                        PwayCode = 3,
                        AimSeqNumber = 100,
                        LearnAimRef = "sdf asdf",
                        LearnStartDate = new DateTime(2017, 05, 15)
                    }, new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        FworkCode = 546,
                        ProgType = 2,
                        PwayCode = 1,
                        AimSeqNumber = 100,
                        LearnAimRef = "189213",
                        LearnStartDate = new DateTime(2005, 08, 21)
                    }
                }
            };

            //act
            var results = _dob48Validator.Validate(ldObj);

            //assert
            Assert.False(results);
        }
    }
}
