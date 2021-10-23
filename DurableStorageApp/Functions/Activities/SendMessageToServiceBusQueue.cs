using Azure.Messaging.ServiceBus;
using DurableStorageApp.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DurableStorageApp.Functions.Activities
{
    public class SendMessageToServiceBusQueue
    {
        [FunctionName("SendMessageToServiceBusQueue")]
        public static async Task<string> SendMessageToAzureServiceBusQueueAsync([ActivityTrigger] CloudBlobItem uploadedcloudBlob, ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation($"Received event data with an uploaded cloud blob {uploadedcloudBlob.Name} with format {uploadedcloudBlob.FileType}.");
            //Config settings for Azure Service Bus
            var azureServiceBusConfig = new ConfigurationBuilder()
                 .SetBasePath(executionContext.FunctionAppDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();

            var serviceBusConnection = azureServiceBusConfig["AzureServiceBusConnectionString"];
            var serviceBusQueue = azureServiceBusConfig["ServiceBusQueueName"];
            string composedMessage = "";

            try
            {
                if (uploadedcloudBlob != null)
                {
                    log.LogInformation($"Composing message to be sent to the queue");

                    composedMessage = $"A blob image {uploadedcloudBlob.Name} was uploaded to Azure Service Bus Queue azdurablefunctioncloudqueue. </br> " +
                                         $"Blob Type: {uploadedcloudBlob.FileType} </br> " +
                                         $"Blob URL: {uploadedcloudBlob.BlobUrl} </br> " +
                                         $"Message sent via Azure Durable Functions App";

                    await using (ServiceBusClient client = new ServiceBusClient(serviceBusConnection))
                    {
                        //Create sender
                        ServiceBusSender sender = client.CreateSender(serviceBusQueue);

                        //Create message
                        ServiceBusMessage message = new ServiceBusMessage(composedMessage);

                        //Send Message to ServiceBus Queue
                        await sender.SendMessageAsync(message);
                        log.LogInformation($"Sent a message to Service Bus Queue: {serviceBusQueue}");
                        return composedMessage;
                    }
                }
                else
                    return composedMessage;
            }
            catch (Exception ex)
            {
                log.LogInformation($"Something went wrong sending the message to the queue : {serviceBusQueue}. Exception {ex.InnerException}");
                throw;
            }
        }
    }
}
