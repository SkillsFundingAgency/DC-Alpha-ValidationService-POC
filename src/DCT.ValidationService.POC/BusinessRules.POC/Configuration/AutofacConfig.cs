using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac.Features.AttributeFilters;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.RuleLearnDelFAMType66;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;
using BusinessRules.POC.RuleR105;
using BusinessRules.POC.SharedRules;
using BusinessRules.POC.SharedRules.DD28;
using BusinessRules.POC.SharedRules.DD29;
using Module = Autofac.Module;
using BusinessRules.POC.ExternalData.Interface;
using DCT.ILR.Model;
using BusinessRules.POC.ValidationData.Interface;

namespace BusinessRules.POC.Configuration
{
    public class BusinessLogicAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register local modules here.

            var currentAssembly = Assembly.GetExecutingAssembly();

            //register all implementations of R105SubRule 
            //builder.RegisterType<R105PickLdFamActTypes>().As<IR105PickLdFamACTTypes>().SingleInstance();
            //builder.RegisterType<LearningDeliveryNoOverlappingDatesRule>().As<ILearningDeliveryNoOverlappingDatesRule>()
            //    .SingleInstance();
            //builder.RegisterType<PickValidLdsWithAgeLimitFamTypeAndCode>().As<IPickValidLdsWithAgeLimitFamTypeAndCode>()
            //    .SingleInstance();
            //builder.RegisterType<FetchSpecificFundModelsLDsWithLearnStartDate>()
            //    .As<IFetchSpecificFundModelsLDsWithLearnStartDate>()
            //    .SingleInstance();
            //builder.RegisterType<LearnerDelFamExclusionRulesValidator>().As<ILearnerDelFam66ExclusionRule>()
            //    .SingleInstance();
            //builder.RegisterType<DateHelper>().As<IDateHelper>()
            //    .SingleInstance();
            //builder.RegisterType<ValidateLARNotionalNVQLevelRule>().As<IValidateLARNotionalNVQLevelRule>()
            //    .SingleInstance();

            builder.RegisterType<DD28PickMatchingEmpRecord>().As<IShortRule<DD28SubModel, MessageLearnerLearnerEmploymentStatus>>().InstancePerRequest();

            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(t => t.IsClass)
                .AsImplementedInterfaces()
                .Except<IRule<MessageLearner>>()
                .Except<ISharedRule<MessageLearner, string>>()
                .Except<ISharedRule<List<MessageLearnerLearningDelivery>, string>>()
                .Except<ILearnerDelFam66ExclusionRuleForFam>()
                .InstancePerRequest();



            //regsiter all implementations of ILearnerDelFam66ExclusionRuleForFam
            //            builder
            //                .RegisterAssemblyTypes(currentAssembly)
            //                .AssignableTo<ILearnerDelFam66ExclusionRuleForFam>()
            //                .AsImplementedInterfaces()
            //                .SingleInstance();

            builder.RegisterType<ValidationData.ValidationData>().As<IValidationData>().InstancePerRequest();

            builder
                .RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<IReferenceData<string,string>>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<IExternalData<string, List<string>>>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<IDD28RuleCriteria>()
                .AsImplementedInterfaces()
                .InstancePerRequest();
            //          builder
            //                .RegisterAssemblyTypes(currentAssembly)
            //                .AssignableTo<IRule<Learner>>()
            //                .AsImplementedInterfaces()
            //                .WithAttributeFiltering()
            //                .SingleInstance();

            //register all rules by rule name. So that they can be explicitly resolved using
            // the following : var r = container.ResolveKeyed<IRule<Learner>>(RuleNames.LearnDelFam66);
            // or via attributes [KeyFilter("")]
            builder
                .RegisterType<LearnDelFAMType66Validator>().Keyed<IRule<MessageLearner>>(RuleNameConstants.LearnDelFam66)
                .WithAttributeFiltering()
                .InstancePerRequest();
                

           // builder.RegisterType<R105Validator>().Keyed<IRule<MessageLearner>>(RuleNameConstants.R105).InstancePerRequest();

            builder.RegisterType<LearnerDoBShouldNotBeNullRule>()
                .Keyed<ISharedRule<MessageLearner, bool>>(RuleNameConstants.LearnerDoBShouldNotBeNull).InstancePerRequest();


            builder.RegisterType<DD28Rule>()
              .Keyed<ISharedRule<MessageLearner, string>>(RuleNameConstants.DD28).InstancePerRequest();
            builder.RegisterType<DD29Rule>()
                .Keyed<ISharedRule<MessageLearner, string>>(RuleNameConstants.DD29).InstancePerRequest();

            builder.RegisterType<DD04Rule>()
                .Keyed<ISharedRule<MessageLearner, List<DD04Result>>>(RuleNameConstants.DD04).InstancePerRequest();


            //builder.RegisterAssemblyTypes(typeof(BaseLearningDeliveryStartDateRule).Assembly)
            //    .Where(t => t.IsSubclassOf(typeof(BaseLearningDeliveryStartDateRule)))
            //    .As<BaseLearningDeliveryStartDateRule>();


            //builder
            //   .RegisterType<LearnStartDate02Rule>()
            //   .Keyed<ILearnStartDate02Rule>(RuleNames.LearnStartDate02Rule)
            //   .SingleInstance();

            //builder
            //   .RegisterType<LearnStartDate03Rule>()
            //   .Keyed<ILearningDeliveryStartDateRule<LearningDelivery>>(RuleNames.LearnStartDate03Rule)
            //   .SingleInstance();

            //builder
            //   .RegisterType<LearnStartDate05Rule>()
            //   .Keyed<ILearningDeliveryStartDateRule<LearningDelivery>>(RuleNames.LearnStartDate05Rule)
            //   .SingleInstance();

            //builder
            //   .RegisterType<LearnStartDate12Rule>()
            //   .Keyed<ILearningDeliveryStartDateRule<LearningDelivery>>(RuleNames.LearnStartDate12Rule)
            //   .SingleInstance();

        }


    }
}
