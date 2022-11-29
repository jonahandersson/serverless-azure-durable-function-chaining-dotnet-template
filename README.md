# Azure Durable Functions (Function Chaining Example) in C# .NET (Starter Template)
##### Starter Template for Azure Serverless Durable Functions .NET Core 
*Author: <a href="https://jonahandersson.tech/" target="_blank">Jonah Andersson</a>*

#### DESCRIPTION:

This is a starter template for Serverless deveöpment with Azure Durable Functions available to those you wants to try it out.
A hands-on lab created with Azure Durable Functions with [Function Chaining](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-sequence?tabs=csharp) application pattern. 

<img src="https://docs.microsoft.com/en-us/azure/azure-functions/durable/media/durable-functions-concepts/function-chaining.png" width="600" alt="Function chaining - Azure Durable Functions">

This template is prepared as starter template that allows you to develop Azure Functions (Durable Functions) serverless workflow with integration to other APIs such as Twilio API and Sendgrid API and Azure services - Azure Storage, ServiceBus etc. 

#### OVERVIEW OF WORKFLOW 

<img src="https://jonahsstorage.blob.core.windows.net/jcaphotos/jonahandersson-serverlessdemo-functionchaining.PNG" width="600">
Azure Durable Functions - Function Chaining Example with Azure Service Bus, Twilio API, Sendgrid API and Azure BLOB Storage 

### EXPECTED RESULTS 

- Orchestration get trigged by an image or BLOB uploaded to the Azure Storage
- Chain 1 - Sends queue message to Azure Service Bus
- Chain 2 - Send SMS or make call using Twilio API
- Chain 3 - Send email to configured email address using SendGrid API 
- Chain 4 - Lab Exercise to send to Azure Cosmos DB etc. (Guide: <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-storage-blob-triggered-function" href="_blank">How to create Azure Cosmos DB Trigger</a>)
               
#### PREREQUISITES AND LAB ENVIRONMENT SETUP 

* Basics concepts of <a href="https://azure.microsoft.com/en-us/overview/serverless-computing/" target="_blank">Azure Serverless Computing</a>, <a href="https://azure.microsoft.com/en-us/services/functions/" target="_blank"> Azure Functions</a> and <a href="https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview?tabs=csharp" target="_blank">Durable Functions</a>  <br> 
 (*If you are a student of Forefront's Serverless course, great. Otherwise, check *Recommended Learning*  below*)
* Microsoft Azure account - Private or Organization subscription account <br> 
 (*If you don't have any Azure Account, sign up https://azure.microsoft.com/en-us/free/*)
* Latest version of [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
* Azure Storage Explorer (https://azure.microsoft.com/en-us/features/storage-explorer/)
* Programming Language C# .NET (You can code in other supported languages as well - see supported languages) 
* Install latest .NET Core 3.1 (LTS) or latest supported like .NET 6 https://dotnet.microsoft.com/download 
* Install [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v3%2Cwindows%2Ccsharp%2Cportal%2Cbash%2Ckeda)
* If you are using .NET Core 3.1 it will not  be supported by the end of 2022 and you need to upgrade to functions version 4.0 
* Postman for HTTP Requests/Triggers
* DEVELOPMENT LOCALLY use file local.settings.json with your own configuration strings, API keys 

#### local.settings.json (Local Development Only) 
*RECOMMENDATION:* Use [Azure Key Vault](https://docs.microsoft.com/en-us/azure/app-service/app-service-key-vault-references?tabs=azure-cli?WT.mc_id=AZ-MVP-5004251) and Managed Identities to secure your function application for Azure Durable Functions 

```yaml
{
   "IsEncrypted": false,
    "Values": {
    "AzureWebJobsStorage": "<PUT YOUR AZUREWEBJOBSSTORAGE CONNECTION STRING HERE>"
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",  
    "StorageConnectionString": "<YOUR AZURE STORAGE CONNECTION STRING HERE>"
    "AzureServiceBusConnectionString": "<PUT YOUR AZURE SERVICE BUS CONNECTION STRING HERE>",
    "ServiceBusQueueName": "blobstoragenotifierqueue",
    "Twilio_SID": "<YOUR TWILIO API SID>",
    "Twilio_Secret": "<YOUR TWILIO API SECRET KEY>",
    "Admin_Email": "<EMAIL ADDRESS TO RECEIVE EMAIL FOR SENDGRID EMAILS",
    "Admin_Name": "<YOUR NAME OR DUMMY NAME>",
    "Admin_Mobile": "<YOUR TEST MOBILE WITH COUNTRY CODE TO RECEIVE TWILIO API SMS & CALL>",
    "Twilio_Verified_Number": "<THE REGISTERED TWILIO API TRIAL ACCOUNT MOBILE NUMBER HERE>",   
    "SendGridAPIKey": "<YOUR SENDGRID API KEY HERE>",
    "CosmosDBEndPointUri": "<YOUR AZURE COSMOS DB ENDPOINT URI HERE",
    "CosmosDBKey": "<YOUR AZURE COSMOS DB KEY HERE>"
}

```

### REQUIRED AZURE SERVICES AND API INTEGRATIONS

- Azure Storage Account for Azure Function App and a BLOB container to upload image files and for the Blob Storage Trigger 
   - <a href="https://docs.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal" target="_blank">How to create an Azure Storage Account</a>
   - <a href="https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-container-create?tabs=dotnet" target="_blank">How to create a Azure Blob Storage Container</a> 
   - <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-storage-blob-triggered-function" target="_blank">How to create a Azure Blob Storage Trigger</a> 
 
- Azure Service Bus Namespace with a queue name that matches the queue name of your app configuration 
  - <a href="https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues" target="_blank">How to create a queue in an Azure Service Bus Namespace</a>
- Twilio API Account -  API Keys and Secret are used to code the logic to send SMS and make call from the function app  
  - <a href="https://www.twilio.com/docs/sms/quickstart/csharp-dotnet-framework" target="_blank">Instructions for TwilioAPI</a> 
-  SendGrid API Account -  API Keys and Secret are used to code logic in sending email 
  - <a href="https://docs.sendgrid.com/for-developers/sending-email/api-getting-started" target="_blank">Instructions for SendGrid</a> 


### WHEN DEBUGGING AND DEVELOPING LOCALLY

When developing Azure Functions locally using this project. You should see similar like this when it is finished.
It logs what is happening with your orchestration. You may also check on 

<img src="https://durablestoragefunctionss.blob.core.windows.net/photoscontainer/ServerlessLab_RunningFunctions%20Locally.PNG" width="700">

### TALKS -  Azure Durable Functions at NDC Oslo Developer Conference 2021 <br>
Click on the image below to watch the recording of my talk for this session at the NDC Olso Conferene <br>
<img src="https://pbs.twimg.com/media/FMwNCHcXMAANGaj?format=jpg&name=medium" width="500"> <br>

Watch session on YouTube https://www.youtube.com/watch?v=C199S4R7cy8 

####  RECOMMENDED LEARNING AND HANDS-ON RESOURCES 
 
- [Azure Durable Functions Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/durable?WT.mc_id=AZ-MVP-5004251) <br>
- [Microsoft Learn](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-create-first-csharp?pivots=code-editor-vscode?WT.mc_id=AZ-MVP-5004251)<br>
- [Azure Functions University on YouTube by Marc Duiker](https://www.youtube.com/channel/UCmoWqg6T-c8zEGm4sZdnwbA) <br>
- [Azure Functions University Lessons on GitHub by Marc Duiker and Community](https://github.com/marcduiker/azure-functions-university) <br>
- [My article about Azure Durable Functions at DEV Community Blog](https://dev.to/jonahandersson/azure-durable-functions-developing-serverless-stateful-workflow-4787)<br>
- [Serverless .NET Development with Azure Durable Functions by Jonah Andersson at Philippine .NET User Group (PHINUG)](https://www.youtube.com/watch?v=zByq3wB7fIQ&t=31s)<br>
- [Azure Durable Functions Fundamentals at Azure User Group Sundsvall by Jonah Andersson](https://www.youtube.com/watch?v=fDej9n-kzNM)<br>
- [Azure Serverless Community Library](https://serverlesslibrary.net/)<br>
- [Azure Storage Account](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview)<br>
- [SendGrid with Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-sendgrid?tabs=in-process%2Cfunctionsv2&pivots=programming-language-csharp)<br>

  
