using BusinessRules.POC.Extensions;
using DCT.ILR.Model;
using FluentAssertions;
using System;
using Xunit;

namespace BusinessRules.POC.Tests.Extensions
{
    public class MessageLearnerExtensionTests
    {

        [Fact]
        public void EarliestLearningDeliveryLearnStartDateFor_NullLearningDelivery()
        {
            var learner = new MessageLearner();

            learner.LearningDelivery = null;

            learner.EarliestLearningDeliveryLearnStartDateFor(1, 1, 1, 1).Should().BeNull();
        }

        [Fact]
        public void EarliestLearningDeliveryLearnStartDateFor_NoMatch()
        {
            var learner = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        ProgType = 1,
                        FworkCode = 1,
                        PwayCode = 1,
                    }
                }
            };

            learner.EarliestLearningDeliveryLearnStartDateFor(1, 1, 1, 2).Should().BeNull();
        }

        [Fact]
        public void EarliestLearningDeliveryLearnStartDateFor_SingleMatch()
        {
            var learnStartDate = new DateTime(2017, 1, 1);

            var learner = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        ProgType = 1,
                        FworkCode = 1,
                        PwayCode = 1,
                        LearnStartDate = learnStartDate
                    },
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        ProgType = 1,
                        FworkCode = 1,
                        PwayCode = 2,
                    }
                }
            };

            learner.EarliestLearningDeliveryLearnStartDateFor(1, 1, 1, 1).Should().Be(learnStartDate);
        }

        [Fact]
        public void EarliestLearningDeliveryLearnStartDateFor_OrderedMatch()
        {
            var earliestLearnStartDate = new DateTime(2017, 1, 1);
            var latestLearnStartDate = new DateTime(2018, 1, 1);

            var learner = new MessageLearner()
            {
                LearningDelivery = new MessageLearnerLearningDelivery[]
                {
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        ProgType = 1,
                        FworkCode = 1,
                        PwayCode = 1,
                        LearnStartDate = earliestLearnStartDate
                    },
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        ProgType = 1,
                        FworkCode = 1,
                        PwayCode = 1,
                        LearnStartDate = latestLearnStartDate
                    },
                    new MessageLearnerLearningDelivery()
                    {
                        AimType = 1,
                        ProgType = 1,
                        FworkCode = 1,
                        PwayCode = 2,
                    }
                }
            };

            learner.EarliestLearningDeliveryLearnStartDateFor(1, 1, 1, 1).Should().Be(earliestLearnStartDate);
        }
    }
}
