using Autofac;
using BusinessRules.POC.FileData;
using BusinessRules.POC.FileData.Interface;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using DCT.LARS.Model;
using DCT.LARS.Model.Interface;
using DCT.ULN.Model;
using DCT.ULN.Model.Interface;
using DCT.ValidationService.Service.Implementation;
using DCT.ValidationService.Service.ReferenceData.Implementation;
using DCT.ValidationService.Service.ReferenceData.Interface;

namespace DCT.ValidationService.Service
{
    public class ValidationServiceServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register local modules here.
            
            builder.RegisterType<ULNv2>().As<IULNv2Context>();
            builder.RegisterType<LARSContext>().As<ILARSContext>();
            builder.RegisterType<ReferenceDataCache>().As<IReferenceDataCache>().InstancePerRequest();
            builder.RegisterType<FileData>().As<IFileData>().InstancePerRequest();
            builder.RegisterType<LearnerValidationErrorHandler>().As<IValidationErrorHandler<MessageLearner>>().InstancePerRequest();
        }
    }
}
