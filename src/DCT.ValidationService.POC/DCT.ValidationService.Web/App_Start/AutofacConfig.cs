using Autofac;
using Autofac.Integration.WebApi;
using BusinessRules.POC.Configuration;
using BusinessRules.POC.RuleManager;
using BusinessRules.POC.RuleManager.Interface;
using DCT.ULN.Model;
using DCT.ULN.Model.Interface;
using DCT.ValidationService.Service;
using DCT.ValidationService.Service.Implementation;
using DCT.ValidationService.Service.Interface;
using DCT.ValidationService.Service.ReferenceData.Implementation;
using DCT.ValidationService.Service.ReferenceData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;

namespace DCT.ValidationService.Web.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {

            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule<BusinessLogicAutofacModule>();
            builder.RegisterModule<ValidationServiceServiceModule>();

            builder.RegisterType<RuleManagerValidationService>().As<IValidationService>().InstancePerRequest();
            builder.RegisterType<RuleManager>().As<IRuleManager>().InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();
            
            return Container;
        }

    }
}