using FluentAssertions;
using System;
using Xunit;

namespace DCT.ILR.Model.Tests
{
    public class MessageLearnerLearningDeliveryTests
    {
        [Fact]
        public void LearnStartEndDate_Specified_False()
        {
            var learningDelivery = new MessageLearnerLearningDelivery();
                
            learningDelivery.LearnStartDateSpecified = false;
            learningDelivery.LearnStartDate = new DateTime(2018, 1, 1);

            learningDelivery.LearnStartDateNullable.Should().BeNull();            
        }

        [Fact]
        public void LearnStartEndDate_Specified_True()
        {
            var learningDelivery = new MessageLearnerLearningDelivery();

            var date = new DateTime(2018, 1, 1);

            learningDelivery.LearnStartDateSpecified = true;
            learningDelivery.LearnStartDate = date;

            learningDelivery.LearnStartDateNullable.Should().Be(date);
            learningDelivery.LearnStartDateNullable.Should().NotBeNull();
        }

        [Fact]
        public void LearnPlanEndDate_Specified_False()
        {
            var learningDelivery = new MessageLearnerLearningDelivery();

            learningDelivery.LearnPlanEndDateSpecified = false;
            learningDelivery.LearnPlanEndDate = new DateTime(2018, 1, 1);

            learningDelivery.LearnPlanEndDateNullable.Should().BeNull();
        }

        [Fact]
        public void LearnPlanEndDate_Specified_True()
        {
            var learningDelivery = new MessageLearnerLearningDelivery();

            var date = new DateTime(2018, 1, 1);

            learningDelivery.LearnPlanEndDateSpecified = true;
            learningDelivery.LearnPlanEndDate = date;

            learningDelivery.LearnPlanEndDateNullable.Should().Be(date);
            learningDelivery.LearnPlanEndDateNullable.Should().NotBeNull();
        }

        [Fact]
        public void LearnActEndDate_Specified_False()
        {
            var learningDelivery = new MessageLearnerLearningDelivery();

            learningDelivery.LearnActEndDateSpecified = false;
            learningDelivery.LearnActEndDate = new DateTime(2018, 1, 1);

            learningDelivery.LearnActEndDateNullable.Should().BeNull();
        }

        [Fact]
        public void LearnActEndDate_Specified_True()
        {
            var learningDelivery = new MessageLearnerLearningDelivery();

            var date = new DateTime(2018, 1, 1);

            learningDelivery.LearnActEndDateSpecified = true;
            learningDelivery.LearnActEndDate = date;

            learningDelivery.LearnActEndDateNullable.Should().Be(date);
            learningDelivery.LearnActEndDateNullable.Should().NotBeNull();
        }
    }
}
