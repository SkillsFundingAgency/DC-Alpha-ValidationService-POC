using BusinessRules.POC.Extensions;
using DCT.ILR.Model;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace BusinessRules.POC.Tests.Extensions
{
    public class MessageLearnerLearningDeliveryExtensionTests
    {

        [Fact]
        public void LearningDeliveryFAMCodeForType_Null()
        {
            var learningDelivery = new MessageLearnerLearningDelivery
            {
                LearningDeliveryFAM = null
            };

            learningDelivery.LearningDeliveryFAMCodeForType("Type").Should().BeNull();
        }

        [Fact]
        public void LearningDeliveryFAMCodeForType_NotFound()
        {
            var learningDelivery = new MessageLearnerLearningDelivery
            {
                LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryFAM() { LearnDelFAMType = "Type"}
                }
            };

            learningDelivery.LearningDeliveryFAMCodeForType("TypeNotFound").Should().BeNull();
        }

        [Fact]
        public void LearningDeliveryFAMCodeForType_Duplicate()
        {
            var learningDelivery = new MessageLearnerLearningDelivery
            {
                LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryFAM() { LearnDelFAMType = "Type", LearnDelFAMCode = "CodeOne" },
                    new MessageLearnerLearningDeliveryLearningDeliveryFAM() { LearnDelFAMType = "Type", LearnDelFAMCode = "CodeTwo" },
                }
            };

            learningDelivery.LearningDeliveryFAMCodeForType("Type").Should().Be("CodeOne");
        }

        [Fact]
        public void LearningDeliveryFAMCodeForType_Single()
        {
            var learningDelivery = new MessageLearnerLearningDelivery
            {
                LearningDeliveryFAM = new MessageLearnerLearningDeliveryLearningDeliveryFAM[]
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryFAM() { LearnDelFAMType = "TypeOne", LearnDelFAMCode = "CodeOne" },
                    new MessageLearnerLearningDeliveryLearningDeliveryFAM() { LearnDelFAMType = "TypeTwo", LearnDelFAMCode = "CodeTwo" },
                }
            };

            learningDelivery.LearningDeliveryFAMCodeForType("TypeTwo").Should().Be("CodeTwo");
        }

    }
}
