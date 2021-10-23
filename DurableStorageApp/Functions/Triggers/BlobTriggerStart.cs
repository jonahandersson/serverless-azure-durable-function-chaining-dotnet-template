using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DurableStorageApp.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableStorageApp.Functions.Triggers
{
    public static class BlobTriggerStart
    {
        [FunctionName("BlobTriggerStart")]
        public static async Task BlobTriggerClientFunction([BlobTrigger("photoscontainer/{name}", Connection ="StorageConnectionString")] CloudBlockBlob myBlob, string name, 
            ILogger log, [DurableClient] IDurableOrchestrationClient starter)
        {
            try
            {
                log.LogInformation($"Started orchestration trigged by BLOB trigger. A blob item with name = '{name}'");
                log.LogInformation($"BLOB Name {myBlob.Name}");

                // Function input comes from the request content.
                if (myBlob != null)
                {
                    var newUploadedBlobItem = new CloudBlobItem
                    {
                        Name = myBlob.Name,
                        BlobUrl = myBlob.Uri.AbsoluteUri.ToString(),
                        Metadata = (Dictionary<string, string>)myBlob.Metadata,
                        FileType = myBlob.BlobType.ToString(),
                        Size = myBlob.Name.Length.ToString(),
                        ETag = myBlob.Properties.ETag.ToString()
                    };

                    var instanceId = await starter.StartNewAsync("AzureStorageOrchestrator", newUploadedBlobItem);
                    log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
                }
                else
                {
                    log.LogError($"The blob was trigged but myCloudBlob was empty");
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
