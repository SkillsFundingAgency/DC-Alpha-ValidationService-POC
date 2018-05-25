using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.SharedRules
{
    /// <summary>
    /// 
    /// </summary>
    public class DD04Rule : ISharedRule<MessageLearner, List<DD04Result>>
    {

        public List<DD04Result> Evaluate(MessageLearner objectToValidate)
        {
            var result = new List<DD04Result>();

            //find valid start dates  is the earliest value of LearningDelivery.LearnStartDate for all programme aims with 
            //LearningDelivery.AimType = 1 and
            //the same value of Learner.LearnRefNumber, LearningDelivery.ProgType, LearningDelivery.FworkCode(only include for apprenticeships(not apprenticeship standards)) and LearningDelivery.PwayCode as this aim(only include for apprenticeships).
            var validProgStartDatesList = objectToValidate.LearningDelivery.Where(x => x.AimType == 1)
                          .GroupBy(code => new DD04ValidLDKey { ProgType = code.ProgType.ToString(),FworkCode= code.FworkCode.ToString(),PwayCode= code.PwayCode.ToString() })
                          .Select(grp => new DD04ValidLDResult { Key = grp.Key, Value = grp.OrderBy(x => x.LearnStartDate).FirstOrDefault() })
                          .ToList();

            //loop through the LDs passed in and populate the calculated prog startdate from the above for each LD
            foreach (var ld in objectToValidate.LearningDelivery)
            {
                var matchedLDStartdate = validProgStartDatesList.Where(x => x.Key.PwayCode == ld.PwayCode.ToString() 
                            && x.Key.ProgType == ld.ProgType.ToString() && x.Key.FworkCode == ld.FworkCode.ToString()).Select(x=> x.Value).FirstOrDefault();

                if (matchedLDStartdate != null)
                    result.Add(new DD04Result() { LearningDelivery = ld, StartDateOfProgramme = matchedLDStartdate.LearnStartDate });
                else
                    result.Add(new DD04Result() { LearningDelivery = ld, StartDateOfProgramme = null });

            }

            return result;
        }

       
    }

    public class DD04ValidLDKey
    {
        public string ProgType { get; set; }
        public string FworkCode { get; set; }
        public string PwayCode { get; set; }
    }

    public class DD04ValidLDResult
    {
        public DD04ValidLDKey Key { get; set; }
        public MessageLearnerLearningDelivery Value { get; set; }
    }

    

    public class DD04Result
    {
        public MessageLearnerLearningDelivery LearningDelivery { get; set; }
        public DateTime? StartDateOfProgramme { get; set; }

    }

}
