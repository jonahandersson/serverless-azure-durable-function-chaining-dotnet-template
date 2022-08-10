using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableStorageApp.Net6.ClientTriggers
{
    public class BlobTrigger
    {
        [FunctionName("BlobTrigger")]
        public void Run([BlobTrigger("photoscontainer/{name}", Connection = "StorageConnectionString")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            try
            {
                log.LogInformation($"Started orchestration trigged by BLOB trigger. A blob item with name = '{name}'");
                log.LogInformation($"BLOB Name {myBlob}");

                // Function input comes from the request content.
                if (myBlob != null)
                {
                    //TODO: Work on the object 
                }
            }
            catch (Exception ex)
            {
                //TODO Errorhandling
                log.LogError("Something went wrong. Error : " + ex.InnerException);
                throw;
            }
        }
    }
}
