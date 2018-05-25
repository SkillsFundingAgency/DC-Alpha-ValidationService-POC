using DCT.ILR.Model;
using System.Collections.Generic;

namespace BusinessRules.POC.Models
{
    public class DD28SubModel
    {
        public MessageLearnerLearningDelivery LearningDeliveryObject { get; set; }
        public IEnumerable<MessageLearnerLearnerEmploymentStatus> LearnerEmploymentStatusObj { get; set; }

    }
}