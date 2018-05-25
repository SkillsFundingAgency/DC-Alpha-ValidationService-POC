using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.RuleManager.Interface;
using DCT.ValidationService.Service.ReferenceData.Interface;
using DCT.ILR.Model;

namespace BusinessRules.POC.RuleManager
{
    public class RuleManager : IRuleManager
    {
        private ILifetimeScope _scope;

        public RuleManager(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IValidationErrorHandler<MessageLearner> ExecuteRules(IEnumerable<MessageLearner> learners)
        {
            var cache = _scope.Resolve<IReferenceDataCache>();

            var ulns = learners.Select(l => l.ULN).Distinct().ToList();

            var learnAimRefs = learners.SelectMany(l => l.LearningDelivery).Select(ld => ld.LearnAimRef).Distinct().ToList();

            cache.Populate(ulns, learnAimRefs);

            var rules = _scope.Resolve<IEnumerable<IRule<MessageLearner>>>().ToList();
            
            rules = rules.ToList();
            rules.AddRange(rules);
            rules.AddRange(rules);            
            rules.AddRange(rules);
            rules.AddRange(rules);
            rules.AddRange(rules);
            rules.AddRange(rules);

            rules = rules.Take(600).ToList();

            //  var item2 = _scope.ResolveKeyed<IRule<Learner>>(RuleNames.LearnDelFam66);

            //foreach (var learner in learners)
            //{
            //    foreach (var item in rules)
            //    {
            //        item.Validate(learner);
            //    }
            //}

            //            foreach (var learner in learners)
            //            {
            //                foreach (var item in rules)
            //                {
            //                    validationResults.Add(item.Validate(learner));
            //                }
            //            }            

            Parallel.ForEach(learners, learner =>
            {
                foreach (var item in rules)
                {
                    item.Validate(learner);
                }
            });

            //            Parallel.ForEach(learners, learner =>
            //            {
            //                Parallel.ForEach(rules, rule =>
            //                {
            //                    validationResults.Add(rule.Validate(learner));
            //                });
            //            });

            //            foreach(var learner in learners)
            //            {
            //                Parallel.ForEach(rules, rule =>
            //                {
            //                    validationResults.Add(rule.Validate(learner));
            //                });
            //            };

            return _scope.Resolve<IValidationErrorHandler<MessageLearner>>();            
        }
    }
}
