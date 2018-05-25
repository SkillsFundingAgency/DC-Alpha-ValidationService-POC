using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ReferenceData;
using DCT.ILR.Model;
using System;
using System.Linq;

namespace BusinessRules.POC.RuleLearnDelFAMType66
{
    public interface IFetchSpecificFundModelsLDsWithLearnStartDate : IShortRule<MessageLearner, MessageLearner>
    {
    }
    
    public class FetchSpecificFundModelsLDsWithLearnStartDate : IFetchSpecificFundModelsLDsWithLearnStartDate
    {
        private readonly DateTime _apprencticeProgAllowedStartDate;

        public FetchSpecificFundModelsLDsWithLearnStartDate(IReferenceData<string, string> referenceData)
        {            
            _apprencticeProgAllowedStartDate = Convert.ToDateTime(referenceData.Get("ApprencticeProgAllowedStartDate"));

        }
        public MessageLearner Evaluate(MessageLearner objectToValidate)
        {
            objectToValidate.LearningDelivery = objectToValidate.LearningDelivery.Where(x => x.FundModel == 35 && x.LearnStartDate >= _apprencticeProgAllowedStartDate).ToArray();

            return objectToValidate;
        }
    }
}
