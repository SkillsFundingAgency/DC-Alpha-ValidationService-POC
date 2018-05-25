﻿using System;
using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;
using DCT.TestDataGenerator.Functor;

namespace DCT.TestDataGenerator
{
    public class RuleToFunctorParser
    {
        private Dictionary<string, List<ILearnerMultiMutator>> _ruleFunctors;
        private ILearnerCreatorDataCache _cache;

        public RuleToFunctorParser(ILearnerCreatorDataCache cache)
        {
            _ruleFunctors = new Dictionary<string, List<ILearnerMultiMutator>>();
            _cache = cache;
        }

        public void AddRuleToFunctor(string ruleName, ILearnerMultiMutator instance)
        {
            if (_ruleFunctors.ContainsKey(ruleName))
            {
                _ruleFunctors[ruleName].Add(instance);
            }
            else
            {
                _ruleFunctors.Add(ruleName, new List<ILearnerMultiMutator>() { instance });
            }
        }

        public Dictionary<FilePreparationDateRequired, List<string>> FilePreparationDateRequiredToRules(IEnumerable<ActiveRuleValidity> rules)
        {
            var result = new Dictionary<FilePreparationDateRequired, List<string>>();
            foreach (var rule in rules)
            {
                IEnumerable<ILearnerMultiMutator> functors = RuleToFunctor(rule.RuleName);
                foreach (var flag in Enum.GetValues(typeof(FilePreparationDateRequired)))
                {
                    IEnumerable<ILearnerMultiMutator> filteredFunctors = functors.Where(s => s.FilePreparationDate() == (FilePreparationDateRequired)flag);
                    if (!result.ContainsKey((FilePreparationDateRequired)flag))
                    {
                        result.Add((FilePreparationDateRequired)flag, new List<string>());
                    }

                    foreach (var s in filteredFunctors)
                    {
                        result[(FilePreparationDateRequired)flag].Add(s.RuleName());
                    }
                }
            }

            return result;
        }

        public void CreateFunctors(Action<ILearnerMultiMutator> addFunctor)
        {
            var type = typeof(DCT.TestDataGenerator.Functor.ILearnerMultiMutator);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            foreach (var t in types)
            {
                var i = (ILearnerMultiMutator)Activator.CreateInstance(t);
                AddRuleToFunctor(i.RuleName(), i);
                addFunctor(i);
            }
        }

        internal IEnumerable<string> RuleNames()
        {
            return _ruleFunctors.Keys;
        }

        internal IEnumerable<ILearnerMultiMutator> RuleToFunctor(string ruleName)
        {
            return _ruleFunctors[ruleName];
        }

        internal int GenerateAndMutate(
           string ruleName,
           bool valid,
           int currentLearnerIndex,
           List<XmlTriplet> triplets,
           ref long ULNIndex)
        {
            XmlTriplet triplet = triplets.First();
            int result = 0;
            LearnerGenerator lg = new LearnerGenerator(_cache);
            foreach (var functor in _ruleFunctors[ruleName])
            {
                foreach (var funcy in functor.LearnerMutators(_cache))
                {
                    GenerationOptions options = lg.CreateGenerationOptions(funcy.LearnerType);
                    funcy.DoMutateOptions(options);
                    if (options.OverrideUKPRN.HasValue)
                    {
                        var ftrip = triplets.Where(s => s.UKPRN == options.OverrideUKPRN.Value);
                        if (ftrip.Count() == 0)
                        {
                            triplet = new XmlTriplet()
                            {
                                UKPRN = options.OverrideUKPRN.Value
                            };
                            triplets.Add(triplet);
                        }
                    }
                    else
                    {
                        triplet = triplets.First();
                    }

                    lg.Options = options;
                    List<MessageLearner> generated = lg.Generate(ruleName, funcy.LearnerType, currentLearnerIndex, ref ULNIndex);
                    generated.ForEach(s =>
                    {
                        triplet.FileRuleLearners.Add(new FileRuleLearner()
                        {
                            ExclusionRecord = funcy.ExclusionRecord,
                            RuleName = ruleName,
                            LearnRefNumber = s.LearnRefNumber,
                            ValidLines = funcy.ValidLines,
                            InvalidLines = funcy.InvalidLines,
                            Valid = valid
                        });
                        funcy.DoMutateLearner(s, valid);
                        if (lg.Options.CreateDestinationAndProgression)
                        {
                            MessageLearnerDestinationandProgression prog = lg.GenerateProgression(ruleName, s);
                            funcy.DoMutateProgression?.Invoke(prog, valid);
                            triplet.Progressions.Add(prog);
                        }
                    });
                    triplet.Learners.AddRange(generated);
                    currentLearnerIndex += generated.Count;
                    result += generated.Count;
                }
            }

            return result;
        }
    }
}