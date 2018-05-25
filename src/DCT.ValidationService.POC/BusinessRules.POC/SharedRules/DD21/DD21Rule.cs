using System.Linq;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using DCT.ILR.Model;

namespace BusinessRules.POC.SharedRules.DD21
{
    public interface IDD21Rule : IShortRule<MessageLearner, string>
    {
        
    }

    public class DD21Rule : IDD21Rule
    {
        private readonly IShortRule<DD28SubModel, MessageLearnerLearnerEmploymentStatus> _dd28PickMatchingEmpRecord;
        private IDD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType _dd21GetLearningDeliveriesWithSpecificEmp;
        private string _allowedFamType;
        private string _FamCodeShouldNotBe;

        public DD21Rule(IShortRule<DD28SubModel, MessageLearnerLearnerEmploymentStatus> dd28PickMatchingEmpRecord, 
            IDD21GetLearningDeliveriesWithSpecificEmpStatusMonitoringType dd21GetLearningDeliveriesWithSpecificEmp,
            IReferenceData<string, string> referenceData)
        {
            _dd28PickMatchingEmpRecord = dd28PickMatchingEmpRecord;
            _dd21GetLearningDeliveriesWithSpecificEmp = dd21GetLearningDeliveriesWithSpecificEmp;
            _allowedFamType = referenceData.Get(AppConstants.DD21AllowedFamType);
            _FamCodeShouldNotBe = referenceData.Get(AppConstants.DD21FamCodeShouldNotBe);

        }

        public string Evaluate(MessageLearner learner)
        {
            var result = "N";

            //get the emprecords with allowed empstat and emptype code
            var empRecordsWithAllowedEmpStatAndEsmType =
                _dd21GetLearningDeliveriesWithSpecificEmp.Evaluate(learner.LearnerEmploymentStatus);

            if (empRecordsWithAllowedEmpStatAndEsmType?.Count == 0) return result;

            foreach (var learningDelivery in learner.LearningDelivery)
            {
                //check if the LDFam type and code are allowed for this LD.
                var isValidFamCodeAndType = learningDelivery.LearningDeliveryFAM.Any(ldFam =>
                    ldFam.LearnDelFAMCode != _FamCodeShouldNotBe && ldFam.LearnDelFAMType == _allowedFamType);

                if(!isValidFamCodeAndType) continue;

                //find matching employment record for the learningdelivery
                var matchedEmployementRecord =
                    _dd28PickMatchingEmpRecord.Evaluate(new DD28SubModel()
                    {
                        LearnerEmploymentStatusObj = empRecordsWithAllowedEmpStatAndEsmType,
                        LearningDeliveryObject = learningDelivery
                    });

                if (matchedEmployementRecord == null) continue;
            
                //if any matched emploment record is found then return
                result = "Y";
                return result;
            }
            //if it reaches here then result is N
            return result;
        }
    }

   
}
