# Bulksign API Samples
Bulksign API sample code in C# 

This repository contains sample C# code for interacting with Bulksign API. This is using the [Bulksign dotNet SDK](https://www.nuget.org/packages/BulksignSdk) and contains projects for .NET 7. The SDK targets NET Standard 2.0 and can also be used with .NET Framework  (feel free to copy the samples into your .NET Framework project) 
<br/>
<br/>

### Running the code

- create a [Bulksign](http://bulksign.com) account , login and go to Settings\My API Keys.
- copy the value of the "Default" key
- edit ApiKeys.cs and replace the API_KEY constant with the copied key value
- build and run the project (edit Program.cs to run the different samples)
<br/>
<br/>

### Target on-premise Bulksign instance

To target a specific Bulksign instance, specify the root path to Bulksign WebAPI

```
BulksignApiClient api = new BulksignApiClient("https://__your_instance__/webapi/");
```


### Looking for GRPC API sample code ?

Please see this repository  https://github.com/bulksign/GRPC-API-Samples


### API Documentation


- API high level overview and FAQ is available <a href="https://bulksign.com/docs/api.htm" target="_blank">here</a>

- Swagger definition and documentation about all  API properties and their meaning is available <a href="https://bulksign.com/webapi/swagger" target="_blank">here</a>


### Samples and scenarios included

[SingleDocumentApproverAndSigner.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/SingleDocumentApproverAndSigner.cs) : simplest scenario, shows how to send a document for approving and signing with Bulksign. 

[MultipleSignersInSerialFlow.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/MultipleSignersInSerialFlow.cs) : shows how to send a document for signing with multiple recipients in serial mode.

[DisableEmailNotifications.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/DisableEmailNotifications.cs) : shows how to disable email notifications for a specific bundle.

[MultipleSignersInBulkFlow.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/MultipleSignersInBulkFlow.cs) : shows how to send a document for signing to multiple recipients in <a href="https://bulksign.com/Public/Features"> bulk mode. <a/>

[AddNewSignatureToDocument.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/AddNewSignatureToDocument.cs) : shows you how to add a new signature field to a document and assign it to the recipient.

[AllowRecipientDelegation.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/AllowRecipientDelegation.cs) : shows you how to enable recipient delegation for a bundle.

[PreventFinishedDocumentToAllSigners.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/PreventFinishedDocumentToAllSigners.cs) : how to prevent signers from receiving a copy of the finished document.

[CustomDocumentAccess.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/CustomDocumentAccess.cs) : shows to assign different documents to different recipients in same bundle.

[SetFormFieldValues.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/SetFormFieldValues.cs) : example of setting a pdf form field value when sending the bundle for signing.

[AddTextAnnotationsToDocument.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/AddTextAnnotationsToDocument.cs) : example of adding new text annotations to documents.

[OpenIdConnectAuthenticationForSigner.cs.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Scenarios/OpenIdConnectAuthenticationForSigner.cs) : example of using an OpenId Connection authentication for a signer.

++ <a href="https://github.com/bulksign/BulksignAPISamples/tree/master/src/Scenarios">and more </a>
