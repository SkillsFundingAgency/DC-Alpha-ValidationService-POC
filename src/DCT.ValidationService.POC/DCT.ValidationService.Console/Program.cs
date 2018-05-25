using Autofac;
using BusinessRules.POC.Configuration;
using BusinessRules.POC.RuleManager;
using DCT.ILR.Model;
using DCT.ValidationService.Service;
using DCT.ValidationService.Service.Interface;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using BusinessRules.POC.Interfaces;
using DCT.LARS.Model;
using DCT.LARS.Model.Interface;
using DCT.ULN.Model;
using DCT.ULN.Model.Interface;
using DCT.ValidationService.Service.Implementation;
using DCT.ValidationService.Service.ReferenceData.Implementation;
using DCT.ValidationService.Service.ReferenceData.Interface;

namespace DCT.ValidationService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            Message message;

            var stream = new FileStream(@"Files\ILR-10006341-1718-20171107-113456-01.xml", FileMode.Open);

            using (var reader = XmlReader.Create(stream))
            {
                var serializer = new XmlSerializer(typeof(Message));
                message = serializer.Deserialize(reader) as Message;            
            }            

            IValidationService validationService = new ValidationService.Service.Implementation.RuleManagerValidationService(new RuleManager(ConfigureBuilderScope()), null);

            var validationErrors = validationService.Validate(message);

            stopWatch.Stop();
        }

        private static ContainerBuilder ConfigureBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<BusinessLogicAutofacModule>();
            builder.RegisterModule<ValidationServiceServiceModule>();

            return builder;
        }

        private static ILifetimeScope ConfigureBuilderScope()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<BusinessLogicAutofacModule>();
            builder.RegisterType<ULNv2>().As<IULNv2Context>();
            builder.RegisterType<LARSContext>().As<ILARSContext>();
            builder.RegisterType<ReferenceDataCache>().As<IReferenceDataCache>();
            builder.RegisterType<LearnerValidationErrorHandler>().As<IValidationErrorHandler<MessageLearner>>();
            var container = builder.Build();
            return container.BeginLifetimeScope();
        }
    }
}
