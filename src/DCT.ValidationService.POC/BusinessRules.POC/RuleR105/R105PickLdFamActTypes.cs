using DCT.ILR.Model;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.RuleR105
{
    /// <summary>
    /// returns list of Learning deliveries which has more than one FAM type = ACT 
    /// </summary>
    public class R105PickLdFamActTypes : IR105PickLdFamACTTypes
    {

        public IEnumerable<MessageLearnerLearningDeliveryLearningDeliveryFAM> Evaluate(MessageLearner learner)
        {
            return learner?.LearningDelivery?
                .SelectMany(ld=> ld.LearningDeliveryFAM)
                .Where(ldFAM => ldFAM.LearnDelFAMType == LearningDeliveryFAMTypeConstants.ACT.ToString());           
        }
    }

  
    public interface ILearningDeliveryNoOverlappingDatesRule
    {
        bool Evaluate(IEnumerable<MessageLearnerLearningDeliveryLearningDeliveryFAM> learningDeliverys);
    }

    public interface IR105PickLdFamACTTypes
    {
        IEnumerable<MessageLearnerLearningDeliveryLearningDeliveryFAM> Evaluate(MessageLearner learner);
    }

}
