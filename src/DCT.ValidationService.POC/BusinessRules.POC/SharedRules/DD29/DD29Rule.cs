using System.Collections.Generic;
using System.Linq;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.ExternalData.Interface;
using DCT.ILR.Model;

namespace BusinessRules.POC.SharedRules.DD29
{
    public interface IDD29Rule : ISharedRule<MessageLearner, string>
    {
       
    }

    /// <summary>
    /// 
    /// </summary>
    public class DD29Rule : IDD29Rule
    {
        private readonly ILARSCategoryRefData _larsExternalData;
        private readonly IReferenceData<string, string> _referenceData;

        public DD29Rule(ILARSCategoryRefData larsExternalData, IReferenceData<string, string> referenceData )
        {
            _larsExternalData = larsExternalData;
            _referenceData = referenceData;
        }
        public string Evaluate(MessageLearner learner)
        {
            if (learner?.LearningDelivery == null)  return "N";

            var allowedLARSCategoryRefs = _referenceData.Get(AppConstants.DD29LARSCategoryRef).Split(',').Select(r => int.Parse(r)).ToList();

            foreach (var learningDelivery in learner.LearningDelivery)
            {
                var larsResult = _larsExternalData.Get(learningDelivery.LearnAimRef) ?? new List<int>();

                if (learningDelivery.ProgType.ToString() == _referenceData.Get(AppConstants.DD29LearningDeliveryProgType) &&
                    larsResult.Intersect(allowedLARSCategoryRefs).Any())
                    return "Y";
            }

            return "N";
        }
    }
}
