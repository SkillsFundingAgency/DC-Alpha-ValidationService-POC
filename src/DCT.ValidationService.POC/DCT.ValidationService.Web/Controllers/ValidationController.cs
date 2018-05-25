using Autofac;
using BusinessRules.POC.Configuration;
using BusinessRules.POC.RuleManager;
using DCT.ILR.Model;
using DCT.ValidationService.Service;
using DCT.ValidationService.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using DCT.ValidationService.Web.Models;
using System.Linq;
using System.Diagnostics;

namespace DCT.ValidationService.Web.Controllers
{
    public class ValidationController : ApiController
    {
        private readonly IValidationService _validationService;

        public ValidationController(IValidationService validationService)
        {
            _validationService = validationService;
        }

        // GET: api/Validation
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Validation/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Validation
        public IEnumerable<string> Post([FromBody]IlrContext ilrContext)
        {
            try
            {
                var startDateTime = DateTime.Now;

                Message message = new Message();
                //try
                //{
                var stopwatch = new Stopwatch();

                stopwatch.Start();

                string xml;

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

                var cloudStorageAccountElapsed = stopwatch.ElapsedMilliseconds;

                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                var cloudBlobClientElapsed = stopwatch.ElapsedMilliseconds;

                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(ilrContext.ContainerReference);

                var cloudBlobContainerElapsed = stopwatch.ElapsedMilliseconds;

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(ilrContext.Filename);

                var cloudBlockBlobElapsed = stopwatch.ElapsedMilliseconds;

                xml = cloudBlockBlob.DownloadText();

                var blob = stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();

                using (var reader = XmlReader.Create(new StringReader(xml)))
                {
                    var serializer = new XmlSerializer(typeof(Message));
                    message = serializer.Deserialize(reader) as Message;
                }

                var deserialize = stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();

                var results = _validationService.Validate(message).ToList();

                var validate = stopwatch.ElapsedMilliseconds;

                return new List<string>()
                {
                    string.Format("Validation API Request Start Time : {0}", startDateTime.ToString("d/M/yyyy H:mm:ss.fff")),
                    string.Format("Errors : {0}", results.Count()),
                    string.Format("Blob Client : {0}", cloudBlobClientElapsed),
                    string.Format("Blob Container : {0}", cloudBlobContainerElapsed),
                    string.Format("Blob Block Blob : {0}", cloudBlockBlobElapsed),
                    string.Format("Blob Download Text : {0}", blob),
                    string.Format("Deserialize ms : {0}", deserialize),
                    string.Format("Validation ms : {0}", validate),
                    string.Format("Validation API Request End Time : {0}", DateTime.Now.ToString("d/M/yyyy H:mm:ss.fff")),
                };
            }
            catch(Exception ex)
            {
                return new List<string>()
                {
                    ex.Message
                };
            }
        }

        // PUT: api/Validation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Validation/5
        public void Delete(int id)
        {
        }


        private static ContainerBuilder ConfigureBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<BusinessLogicAutofacModule>();
            builder.RegisterModule<ValidationServiceServiceModule>();

            return builder;
        }
    }
}
