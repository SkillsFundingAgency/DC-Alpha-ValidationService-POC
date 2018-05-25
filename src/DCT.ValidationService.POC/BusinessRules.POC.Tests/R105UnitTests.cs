using BusinessRules.POC.RuleR105;
using System;
using System.Collections.Generic;
using Xunit;
using FluentValidation;
using DCT.ValidationService.Service.Implementation;
using DCT.ILR.Model;

namespace BusinessRules.POC.Tests
{
    public class R105UnitTests
    {
        private R105Validator _r105RuleValidator;

        public R105UnitTests()
        {
            _r105RuleValidator = new R105Validator(
                new R105PickLdFamActTypes(),
                new LearningDeliveryNoOverlappingDatesRule(),
                new LearnerValidationErrorHandler()
            );
        }

        [Trait("Category", "R105-Rule")]
        [Fact]      
        public void R105RuleWithNoOverlappingDates()
        {
            //arrange          
            var learner = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 21)
                            },
                           
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ABC"
                            }
                        }
                    },
                     new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 2,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                             new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "3",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            },
                        }
                     }
                }
            };

            

            //act
            _r105RuleValidator.Validate(learner);
            
            //assert
        }

        [Trait("Category", "R105-Rule")]
        [Fact]
        public void WithOverlappingDates_Returns_False()
        {
            //arrange          
            var ldObj = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 10)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 21)
                            }

                        }
                    },
                     new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 2,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {


                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "3",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            }
                        }
                     }
                }
            };

            //act
            _r105RuleValidator.Validate(ldObj);

            //assert
        }

        [Trait("Category","R105-Rule")]
        [Fact]
        public void RuleWithOverlappingDates2_Return_False()
        {
            //arrange          
            var ldObj = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 10)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 5),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 5)
                            }
                        }
                    },
                    new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 09, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 10, 10)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 10, 5),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 5)
                            }

                        }
                    }
                }
            };

            //act
            _r105RuleValidator.Validate(ldObj);

            //assert
        }

        [Trait("Category", "R105-Rule")]
        [Fact]
        public void WithOverlappingDatesThreeCodes()
        {
            //arrange          
            var ldObj = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 21)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 01),
                                LearnDelFAMDateTo = new DateTime(2017, 11, 09)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "3",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 11, 21),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            }
                        }
                    }
                }
            };



            //act
            _r105RuleValidator.Validate(ldObj);

            //assert
        }

        [Trait("Category", "R105-Rule")]
        [Fact]
        public void WithOverlappingDatesButSameCodes()
        {
            //arrange          
            var ldObj = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "ACT",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            }
                        }
                    }
                }
            };

            //act
            _r105RuleValidator.Validate(ldObj);

            //assert
        }

        [Trait("Category", "R105-Rule")]
        [Fact]
        public void WithNoDifferentFAMTypeReturnsTrue()
        {
            //arrange          
            var ldObj = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        LearnAimRef = "",
                        AimSeqNumber = 1,
                        LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                        {
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "1",
                                LearnDelFAMType = "XYZ",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            },
                            new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                            {
                                LearnDelFAMCode = "2",
                                LearnDelFAMType = "BCD",
                                LearnDelFAMDateFrom = new DateTime(2017, 12, 10),
                                LearnDelFAMDateTo = new DateTime(2017, 12, 21)
                            }
                        }
                    }
                }
            };



            //act
            _r105RuleValidator.Validate(ldObj);

            //assert
        }


    }
}
