using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples.Scenarios
{
	public class CustomDocumentAccess
	{
		public void SendBundle()
		{

			BulkSignApi api = new BulkSignApi();

			BundleApiModel bb = new BundleApiModel();
			bb.DaysUntilExpire = 10;
			bb.Message = "Please sign this document";
			bb.Subject = "Please Bulksign this document";
			bb.Name = "Test bundle";

			RecipientApiModel firstRecipient = new RecipientApiModel();
			firstRecipient.Name = "Bulksign Test";
			firstRecipient.Email = "contact@bulksign.com";
			firstRecipient.Index = 1;
			firstRecipient.RecipientType = RecipientTypeApi.Signer;

			RecipientApiModel secondRecipient = new RecipientApiModel();
			secondRecipient.Name = "Second Recipient";
			secondRecipient.Email = "second@bulksign.com";
			secondRecipient.Index = 1;
			secondRecipient.RecipientType = RecipientTypeApi.Signer;

			bb.Recipients = new[]
			{
					 firstRecipient, secondRecipient
			};

			DocumentApiModel firstDocument = new DocumentApiModel();
			firstDocument.Index = 1;
			firstDocument.FileName = "bulksign_test_Sample.pdf";
			firstDocument.FileContentByteArray = new FileContentByteArray()
			{
				ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
			};

			DocumentApiModel secondDocument = new DocumentApiModel();
			secondDocument.Index = 2;
			secondDocument.FileName = "forms.pdf";
			secondDocument.FileContentByteArray = new FileContentByteArray()
			{
				ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\forms.pdf")
			};

			bb.Documents = new[]
			{
				firstDocument, secondDocument
			};

			bb.FileAccessMode = FileAccessModeApi.Custom;

			//assign different files to different recipients

			bb.CustomFileAccess = new CustomFileAccessApiModel[2];

			//assign first file to first recipient
			bb.CustomFileAccess[0] = new CustomFileAccessApiModel()
			{
				RecipientEmail = bb.Recipients[0].Email,
				FileNames = new []{ "bulksign_test_Sample.pdf" }
			};

			//assign first file to first recipient
			bb.CustomFileAccess[1] = new CustomFileAccessApiModel()
			{
				RecipientEmail = bb.Recipients[1].Email,
				FileNames = new[]
				{
					"forms.pdf"
				}
			};

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