# Bulksign API Samples
Bulksign API sample code (in C#).

This repository contains sample C# code for interacting with Bulksign API. This is using the [Bulksign dotNet SDK](https://www.nuget.org/packages/BulksignSdk)
<br/>
<br/>

### Running the code

- create a [Bulksign](http://bulksign.com) account
- login, go to Settings\My API Keys.
- copy the value of the "Default" key
- edit ApiKeys.cs and replace the API_KEY and EMAIL constants with the token value and your email address.
- build and run the project 
<br/>
<br/>

### Target on-premise instance

To target a specific Bulksign instance, specify the root to WebAPI

```
	BulksignApiClient api = new BulksignApiClient("https://__your_instance__/webapi/");
```


### Samples and scenarios included

[SingleDocumentApproverAndSigner.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/SingleDocumentApproverAndSigner.cs) : simplest scenario, shows how to send a document for approving and signing with Bulksign. 

[MultipleSignersInSerialFlow.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/MultipleSignersInSerialFlow.cs) : shows how to send a document for signing with multiple recipients in serial mode.

[DisableEmailNotifications.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/DisableEmailNotifications.cs) : shows how to disable email notifications for a specific bundle.

[MultipleSignersInBulkFlow.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/MultipleSignersInBulkFlow.cs) : shows how to send a document for signing to multiple recipients in <a href="https://bulksign.com/Public/Features"> bulk mode. <a/>

[AddNewSignatureToDocument.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/AddNewSignatureToDocument.cs) : shows you how to add a new signature field to a document and assign it to the recipient.

[AllowRecipientDelegation.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/AllowRecipientDelegation.cs) : shows you how to enable recipient delegation for a bundle.

[PreventFinishedDocumentToAllSigners.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/PreventFinishedDocumentToAllSigners.cs) : how to prevent signers from receiving a copy of the finished document.

[CustomDocumentAccess.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/CustomDocumentAccess.cs) : shows to assign different documents to different recipients in same bundle.

[SetFormFieldValues.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/SetFormFieldValues.cs) : example of setting a pdf form field value when sending the bundle for signing.

[AddTextAnnotationsToDocument.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/AddTextAnnotationsToDocument.cs) : example of adding new text annotations to documents.

[OpenIdConnectAuthenticationForSigner.cs.cs](https://github.com/bulksign/Bulksign-API-sample/blob/master/Bulksign%20Api%20Samples/Scenarios/OpenIdConnectAuthenticationForSigner.cs) : example of using an OpenId Connection authentication for a signer.

++ <a href="https://github.com/bulksign/Bulksign-API-sample/tree/master/Bulksign%20Api%20Samples/Scenarios">and more </a>
