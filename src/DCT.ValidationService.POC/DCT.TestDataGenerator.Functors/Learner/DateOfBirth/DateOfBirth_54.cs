﻿using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class DateOfBirth_54
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
        //    AdvancedLevelApprenticeship = 2,
        //IntermediateLevelApprenticeship = 3,
        //HigherApprenticeshipLevel4 = 20,
        //HigherApprenticeshipLevel5 = 21,
        //HigherApprenticeshipLevel6 = 22,
        //HigherApprenticeshipLevel7 = 23,
        //Traineeship = 24,
        //ApprenticeshipStandard = 25
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate16Standard, DoMutateOptions = MutateGenerationOptionsStandards, InvalidLines = 5 },
            };
        }

        public string RuleName()
        {
            return "DOB_54";
        }

        private void Mutate16Standard(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.ApprenticeshipStandard).First();
            learner.LearningDelivery[0].LearnStartDate = _options.LD.OverrideLearnStartDate.Value;
            Helpers.MutateApprenticeshipToStandard(learner);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Less16And30Days, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.YoungerLots);
            Helpers.SetEndDates(learner.LearningDelivery[0], learner.LearningDelivery[0].LearnStartDate.AddDays(372), Helpers.SetAchDate.SetAchDate);
            Helpers.SetEndDates(learner.LearningDelivery[1], learner.LearningDelivery[0].LearnActEndDate, Helpers.SetAchDate.DoNotSetAchDate);

            Helpers.SetApprenticeshipAims(learner, pta);
        }

        private void MutateGenerationOptionsStandards(GenerationOptions options)
        {
            options.CreateDestinationAndProgression = true;
            options.LD.OverrideLearnStartDate = DateTime.Parse("2016-AUG-01");
            options.LD.IncludeHHS = true;
            _options = options;
        }
    }
}
