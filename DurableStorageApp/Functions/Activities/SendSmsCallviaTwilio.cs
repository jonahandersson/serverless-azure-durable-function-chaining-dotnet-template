using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace DurableStorageApp.Functions.Activities
{
    public static class SendSmsCallviaTwilio
    {
        [FunctionName("SendSmsCallviaTwilio")]
        public static async Task<bool> SendSMSCallMessageTwilio([ActivityTrigger] string serviceBusQueueMessage, ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation($"BLOB already saved to Service Bus Queue");

            //Config settings for Azure Service Bus
            var config = new ConfigurationBuilder()
                 .SetBasePath(executionContext.FunctionAppDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();

            // You API secrets should be on the set up on a localsettings.json, app settings or keyvault
            var twilioAccountId = config["Twilio_SID"];
            var twilioSecret = config["Twilio_Secret"];
            var twilioAdminMobile = config["Admin_Mobile"];
            var twilioVerifiedNumber = config["Twilio_Verified_Number"];
            var serviceBusName = config["blobstoragenotifierqueue"];

            TwilioClient.Init(twilioAccountId, twilioSecret);
            log.LogInformation($"Composing message with ");
            try
            {
                if (serviceBusQueueMessage != null)
                {
                    //Send SMS to Azure Service Bus Admin User
                    var smsMessage = await MessageResource.CreateAsync(
                       body: $"Hi Admin! A new cloud blob file was uploaded to your Azure Storage " +
                       $" and a queue message was sent to Azure Service Bus Queue {serviceBusName}. \n" +
                       $" Queue Message was {serviceBusQueueMessage}",
                       // mediaUrl is used in Twilio if you want to send the Blob as MMS using image url
                       //mediaUrl: uploadedBlobUrl, 
                       from: new PhoneNumber(twilioVerifiedNumber),
                       to: new PhoneNumber(twilioAdminMobile)
                     );

                    //Backend logging
                    log.LogInformation($"Sms sent to the number provided. \n " +
                                        $"Message Id : {smsMessage.Sid} \n " +
                                        $"Date Sent : {smsMessage.DateSent} \n " +
                                        $"Message : {smsMessage.Body}");

                    //Initiate call reminder to admin
                    var call = CallResource.CreateAsync(
                    twiml: new Twiml("<Response><Say>Hi! Call reminder. New BLOB added to Service Bus Queue!</Say></Response>"),
                    from: new PhoneNumber(twilioVerifiedNumber),
                    to: new PhoneNumber(twilioAdminMobile)
                    );

                    //Backend logging
                    log.LogInformation($"Called admin on number provided. \n " +
                                      $"Call Id : {call.Id} \n " +
                                      $"Call Status : {call.Status} \n " +
                                      $"Call Completed : {call.IsCompleted} \n ");

                    return true;
                }
                else return false;
            }
            catch (ApiException e)
            {
                if (e.Code == 21614)
                {
                    log.LogError("Uh oh, looks like this caller can't receive SMS messages.");
                }
                throw;
            }
        }
    }
}
