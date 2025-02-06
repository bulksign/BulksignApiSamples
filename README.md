# Bulksign API Samples
Bulksign API sample code in C# 

This repository contains sample C# code for interacting with Bulksign API. It's using the [Bulksign dotNet SDK](https://www.nuget.org/packages/BulksignSdk).
The SDK targets NET Standard 2.0 and the sample code can be used from both .NET and .NET Framework (feel free to copy the samples into your .NET Framework project).
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

<br/>
<br/>

### Looking for GRPC API sample code ?

Please see this repository  https://github.com/bulksign/GRPC-API-Samples
<br/>
<br/>

### API Documentation


- API high level overview and FAQ is available <a href="https://bulksign.com/docs/api.htm" target="_blank">here</a>

- Swagger definition and documentation about all  API properties and their meaning is available <a href="https://bulksign.com/webapi/swagger" target="_blank">here</a>

<br/>
<br/>

### Samples and scenarios included

[SingleDocumentApproverAndSigner.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/SingleDocumentApproverAndSigner.cs) : simplest scenario, shows how to send a document for approving and signing with Bulksign. 

[MultipleSignersInSerialFlow.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/MultipleSignersInSerialFlow.cs) : shows how to send a document for signing with multiple recipients in serial mode.

[DisableEmailNotifications.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/DisableEmailNotifications.cs) : shows how to disable email notifications for a specific bundle.

[MultipleSignersInBulkFlow.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/MultipleSignersInBulkFlow.cs) : shows how to send a document for signing to multiple recipients in <a href="https://bulksign.com/Public/Features"> bulk mode. <a/>

[AddNewSignatureToDocument.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/AddNewSignatureToDocument.cs) : shows you how to add a new signature field to a document and assign it to the recipient.

[AllowRecipientDelegation.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/AllowRecipientDelegation.cs) : shows you how to enable recipient delegation for a envelope.

[PreventFinishedDocumentToAllSigners.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/PreventFinishedDocumentToAllSigners.cs) : how to prevent signers from receiving a copy of the finished document.

[CustomDocumentAccess.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/CustomDocumentAccess.cs) : shows to assign different documents to different recipients in same envelope.

[OverwriteFormFieldValues.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/OverwriteFormFieldValues.cs.cs) : example of setting a pdf form field value when sending the envelope for signing.

[AddTextAnnotationsToDocument.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/AddTextAnnotationsToDocument.cs) : example of adding new text annotations to documents.

[OpenIdConnectAuthenticationForSigner.cs](https://github.com/bulksign/BulksignAPISamples/blob/master/src/Envelope/OpenIdConnectAuthenticationForSigner.cs) : example of using an OpenId Connection authentication for a signer.

++ <a href="https://github.com/bulksign/BulksignAPISamples/tree/master/src/">and more samples</a>
