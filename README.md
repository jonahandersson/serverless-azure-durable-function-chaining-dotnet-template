# Forefronts Serverless Functions in Azure (Lab Template)
##### Starter Template for Azure Serverless Durable Functions
*Author: <a href="https://jonahandersson.tech/" target="_blank">Jonah Andersson</a>*

#### LAB DESCRIPTION:

This is a starter template for Serverless course at work but available to public who wants to try it out.
A hands-on lab created with Azure Durable Functions with [Function Chaining](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-sequence?tabs=csharp) application pattern. 

This template is prepared as starter template that allows you to develop Azure Functions (Durable Functions) serverless workflow with integration to other APIs such as Twilio API and Sendgrid API and Azure services - Azure Storage, ServiceBus etc. 

#### OVERVIEW OF WORKFLOW 
![Jonah Andersson Azure Durable Functions - Function Chaining Example](https://durablestoragefunctionss.blob.core.windows.net/photoscontainer/AzDurableFunctionChaining.jpg)
 *Azure Durable Functions - Function Chaining Example with Azure Service Bus, Twilio API, Sendgrid API and Azure BLOB Storage*
               
               
#### PREREQUISITES AND LAB ENVIRONMENT SETUP 

* Basics concepts of Azure Serverless Computing and Azure Functions <br> 
 (*If you are a student of Forefront's Serverless course, great. Otherwise, check recommended Learning below*)
* Microsoft Azure account - Private or Organization subscription account <br> 
 (*If you don't have any Azure Account, sign up https://azure.microsoft.com/en-us/free/*)
* Latest version of [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
* Azure Storage Explorer (https://azure.microsoft.com/en-us/features/storage-explorer/)
* Programming Language C# .NET (You can code in other supported languages as well - see supported languages) 
* Install latest .NET Core 3.1 (LTS) https://dotnet.microsoft.com/download 
* Install [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v3%2Cwindows%2Ccsharp%2Cportal%2Cbash%2Ckeda)


### REQUIRED AZURE SERVICES AND API INTEGRATIONS

* Azure Storage Account for Function App and a BLOB container to upload image files
* Azure Service Bus Namespace with a Queue 
* Twilio API Account -  API Keys and Secret are used to code the logic to send SMS and make call from the function app  (Instructions for TwilioAPI) 
* SendGrid API Account -  API Keys and Secret are used to code logic in sending email (Instructions for SendGrid) 

####  RECOMMENDED LEARNING AND HANDS-ON RESOURCES 
 
* [**Azure Durable Functions Documentation**](https://docs.microsoft.com/en-us/azure/azure-functions/durable?WT.mc_id=AZ-MVP-5004251)
* [**Microsoft Learn**](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-create-first-csharp?pivots=code-editor-vscode?WT.mc_id=AZ-MVP-5004251)  
* [**Azure Functions University on YouTube  by Marc Duiker**](https://www.youtube.com/channel/UCmoWqg6T-c8zEGm4sZdnwbA)
* [**My article about Azure Durable Functions at DEV Community Blog**](https://dev.to/jonahandersson/azure-durable-functions-developing-serverless-stateful-workflow-4787)
* [**Serverless .NET Development with Azure Durable Functions by Jonah Andersson at Philippine .NET User Group (PHINUG)**](https://www.youtube.com/watch?v=zByq3wB7fIQ&t=31s)
* [**Azure Durable Functions Fundamentals at Azure User Group Sundsvall by Jonah Andersson**](https://www.youtube.com/watch?v=fDej9n-kzNM)
* [Azure Storage Account] (https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview)

  
