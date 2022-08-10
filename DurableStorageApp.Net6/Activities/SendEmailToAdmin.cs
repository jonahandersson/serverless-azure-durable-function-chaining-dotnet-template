using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using DurableStorageApp.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace DurableStorageApp.Net6.Activities
{
    public static class SendEmailToAdmin
    {
        [FunctionName("SendEmailNotification")]
        public static async Task<bool> SendEmailNotification([ActivityTrigger] CloudBlobItem uploadedBlob, ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation($"BLOB already saved to queue.");

            try
            {
                //Config settings for Azure Service Bus
                var sendGridAPIConfig = new ConfigurationBuilder()
                     .SetBasePath(executionContext.FunctionAppDirectory)
                     .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables()
                     .Build();

                var apiKey = sendGridAPIConfig["SendGridAPIKey"];
                var adminEmail = sendGridAPIConfig["Admin_Email"];
                var adminName = sendGridAPIConfig["Admin_Name"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(adminEmail, adminName);

                List<EmailAddress> recipients = new List<EmailAddress>
                {
                  new EmailAddress("jonah.andersson@forefront.se", "Jonah @Forefront")
                };

                var subject = "New BLOB Uploaded on Azure Service Bus Queue ";
                var htmlContent = @"<p> A new cloud blob file added to Azure Service Bus queue.  BLOB Url:
                                  </a>" + uploadedBlob.BlobUrl + "</a><br> Message from Jonahs app. </p>";
                var displayRecipients = false; // set this to true if you want recipients to see each others mail id
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipients, subject, "", htmlContent, displayRecipients);
                var isEmailSent = await client.SendEmailAsync(msg);

                if (isEmailSent.IsSuccessStatusCode)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                //Error handling
                log.LogError($"Receiving Service Bus Queue Message failed and email not sent: {ex.InnerException}");
                throw;
            }
        }
    }
}
