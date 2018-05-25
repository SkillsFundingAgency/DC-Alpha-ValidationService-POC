using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Autofac;
using BusinessRules.POC.Configuration;
using BusinessRules.POC.RuleManager;
using DCT.ILR.Model;
using DCT.ValidationService.Service;
using DCT.ValidationService.Service.Interface;


namespace DCT.ValidationService.AzureFunctions
{
    public static class JobManager
    {
        [FunctionName("JobManager")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log, Binder binder)
        {

            // parse query parameter
            log.Info("Going to read the file.");

            var attributes = new Attribute[]
            {
                //new StorageAccountAttribute("MyStorageAccount"),
                new BlobAttribute("ilr-files/ILR-10006341-1718-20171107-113456-01.xml")
            };

            Message message;
            var sw = new Stopwatch();
            sw.Start();

            var ilrText = await binder.BindAsync<string>(attributes);

            sw.Stop();

            log.Info("Read the file-time it took: " + sw.Elapsed);

            sw.Restart();
            // var stream = new FileStream(@"Files\ILR-10006341-1718-20171107-113456-01.xml", FileMode.Open);

            using (var reader = new StringReader(ilrText))
            {
                var serializer = new XmlSerializer(typeof(Message));
                message = serializer.Deserialize(reader) as Message;
            }

            sw.Stop();
            log.Info("Deserailized the file-time it took: " + sw.Elapsed);
            log.Info("total learners:"  + message.Learner.Length);

            IValidationService validationService = new ValidationService.Service.Implementation.RuleManagerValidationService(new RuleManager(ConfigureBuilder()), null);

            //var validationErrors = validationService.Validate(message);
            log.Info("C# HTTP trigger function processed a request.");

            return req.CreateResponse(HttpStatusCode.OK, "Yay!Success");
        }

        private static ILifetimeScope ConfigureBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<BusinessLogicAutofacModule>();
            builder.RegisterModule<ValidationServiceServiceModule>();

            return builder.Build().BeginLifetimeScope();
        }
    }
}
