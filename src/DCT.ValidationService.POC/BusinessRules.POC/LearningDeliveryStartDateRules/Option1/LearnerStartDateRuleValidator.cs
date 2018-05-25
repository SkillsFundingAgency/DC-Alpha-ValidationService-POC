using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules;
using DCT.ILR.Model;
using System;
using System.Threading.Tasks;

namespace BusinessRules.POC.LearningDeliveryRules.Option1
{
    public sealed class LearnerStartDateRuleValidator// : IRule<MessageLearner>
    {
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;
        private readonly IReferenceData<string, string> _referenceData;
        private readonly IDD07Rule _dd07IsYRule;
        private const string DDO7_RULE_NO = "N";
        private const string DDO7_RULE_YES = "Y";

        public LearnerStartDateRuleValidator(IValidationErrorHandler<MessageLearner> validationErrorHandler,
                                             IDD07Rule dd07IsYRule,
                                             IReferenceData<string, string> referenceData)
        {
            _validationErrorHandler = validationErrorHandler;
            _referenceData = referenceData;
            _dd07IsYRule = dd07IsYRule;
        }


        public bool Validate(MessageLearner learner)
        {

            if (learner.LearningDelivery == null)
            {
                return true;
            }
            

            foreach (var learningDelivery in learner.LearningDelivery)
            {
                ValidateLearnStartDate02(learningDelivery, learner);
                ValidateLearnStartDate03(learningDelivery, learner);
                ValidateLearnStartDate05(learningDelivery, learner);
                ValidateLearnStartDate12(learningDelivery, learner);
            }

            return true;
        }

        

        /// <summary>
        /// The Learning start date must not be more than 10 years before the start of the current teaching year
        /// </summary>
        /// <param name="ld"></param>
        /// <returns></returns>
        internal void ValidateLearnStartDate02(MessageLearnerLearningDelivery ld, MessageLearner learner)
        {
            var academicStart = DateTime.Parse(_referenceData.Get("AcademicYearStart"));
            var result = (ld.LearnStartDate.AddYears(10) - academicStart).TotalDays > 0;
            if (!result)
                _validationErrorHandler.Handle(learner, "Error - ValidateLearnStartDate02");
        }

        /// <summary>
        /// If the learning aim is not part of an apprenticeship or traineeship, then the Learning start date must not be after the current teaching year
        /// </summary>
        /// <param name="ld"></param>
        /// <returns></returns>
        internal void ValidateLearnStartDate03(MessageLearnerLearningDelivery ld, MessageLearner learner)
        {
            var dd07Result = _dd07IsYRule.Evaluate(ld);
            var academicStart = DateTime.Parse(_referenceData.Get("AcademicYearStart"));

            var result = dd07Result == DDO7_RULE_NO &&
                            ld.ProgType != 24 &&
                            (ld.LearnStartDate - academicStart.AddDays(364)).TotalDays > 0;
            if (result)
                _validationErrorHandler.Handle(learner, "Error - ValidateLearnStartDate03");
        }

        /// <summary>
        /// The Learning start date must be after the learner's Date of birth
        /// </summary>
        /// <param name="ld"></param>
        /// <returns></returns>
        internal void ValidateLearnStartDate05(MessageLearnerLearningDelivery ld, MessageLearner learner)
        {
            var result = learner.DateOfBirth >= ld.LearnStartDate;
            if (result)
                _validationErrorHandler.Handle(learner, "Error - ValidateLearnStartDate05");

        }

        /// <summary>
        /// If the learning aim is part of an apprenticeship, then the Learning start date must not be more than one 
        /// year after the end of the current teaching year
        /// </summary>
        /// <param name="ld"></param>
        /// <param name="learner"></param>
        internal  void ValidateLearnStartDate12(MessageLearnerLearningDelivery ld, MessageLearner learner)
        {
            var dd07Result = _dd07IsYRule.Evaluate(ld);
            var academicStart = DateTime.Parse(_referenceData.Get("AcademicYearStart"));

            var result = dd07Result == DDO7_RULE_YES &&
                            ld.ProgType != 24 &&
                            (ld.LearnStartDate - academicStart.AddDays(364)).TotalDays > 0;
            if (result)
                _validationErrorHandler.Handle(learner, "Error - ValidateLearnStartDate12");
        }
    }
}
