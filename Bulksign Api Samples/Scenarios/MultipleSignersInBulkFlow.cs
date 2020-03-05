using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class MultipleSignersInBulkFlow
	{
		public void SendBundle()
		{
			BulkSignApi api = new BulkSignApi();

			BundleApiModel bb = new BundleApiModel();
			bb.DaysUntilExpire = 10;
			bb.Message = "Please sign this document";
			bb.Subject = "Please Bulksign this document";
			bb.Name = "Test bundle";

			//the Index must be set to 1 for all signer for the flow to be switched to bulk mode.

			RecipientApiModel recipient = new RecipientApiModel();
			recipient.Name = "Bulksign Test";
			recipient.Email = "contact@bulksign.com";
			recipient.Index = 1;
			recipient.RecipientType = RecipientTypeApi.Signer;


			RecipientApiModel secondRecipient = new RecipientApiModel();
			secondRecipient.Name = "Bulksign Test";
			secondRecipient.Email = "contact@bulksign.com";
			secondRecipient.Index = 1;
			secondRecipient.RecipientType = RecipientTypeApi.Signer;


			bb.Recipients = new[]
			{
					 recipient, secondRecipient
				};

			DocumentApiModel document = new DocumentApiModel();
			document.Index = 1;
			document.FileName = "test.pdf";
			document.FileContentByteArray = new FileContentByteArray()
			{
				ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
			};

			bb.Documents = new[] {document};

			AuthorizationApiModel token = new ApiKeys().GetAuthorizationToken();

			if (string.IsNullOrEmpty(token.UserToken))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}


			BulksignResult<SendBundleResultApiModel> result = api.SendBundle(token, bb);

			Console.WriteLine("Api call is successfull: " + result.IsSuccessful);

			if (result.IsSuccessful)
			{
				Console.WriteLine("Access code for recipient " + result.Response.AccessCodes[0].RecipientName + " is " + result.Response.AccessCodes[0].AccessCode);
				Console.WriteLine("Bundle id is : " + result.Response.BundleId);
			}
		}
	}

}