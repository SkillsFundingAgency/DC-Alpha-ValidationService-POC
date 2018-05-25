using System.Collections.Generic;
using System.Linq;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.SharedRules.DD28;
using DCT.ILR.Model;

namespace BusinessRules.POC.SharedRules
{
    public class DD28Rule : ISharedRule<MessageLearner, string>
    {
        private readonly IEnumerable<IDD28RuleCriteria> _dd28CriteriaRules;
        private readonly IDD28PickMatchingEmpRecord _dd28PickMatchingEmpRecord;

        public DD28Rule(IDD28PickMatchingEmpRecord dd28PickMatchingEmpRecord,
           IEnumerable<IDD28RuleCriteria> dd28CriteriaRules)
        {
            _dd28PickMatchingEmpRecord = dd28PickMatchingEmpRecord;

            //subrules to be exuected with OR condition
            _dd28CriteriaRules = dd28CriteriaRules;
        }

        public string Evaluate(MessageLearner objectToValidate)
        {
            var result = "N";

            foreach (var learningDelivery in objectToValidate.LearningDelivery)
            {
                //find matching employment record for the learningdelivery
                var matchedEmployementRecord =
                    _dd28PickMatchingEmpRecord.Evaluate(new DD28SubModel()
                    {
                        LearnerEmploymentStatusObj = objectToValidate.LearnerEmploymentStatus,
                        LearningDeliveryObject = learningDelivery
                    });

                if (matchedEmployementRecord == null) return result;

                //pass the matched emp records and learning delivery to subrules
                var isAnyCreteriaValid = _dd28CriteriaRules.Any(criteria => criteria.Evaluate(new DD28SubModel()
                {
                    LearningDeliveryObject = learningDelivery,
                    LearnerEmploymentStatusObj = new List<MessageLearnerLearnerEmploymentStatus>() { matchedEmployementRecord }
                }));

                if (!isAnyCreteriaValid) continue;
                //if any rule returns true then return Y
                result = "Y";
                return result;
            }
            //if it reaches here then result is N
            return result;

        }

        
        
      
    }
}
