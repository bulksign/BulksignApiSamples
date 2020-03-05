using Bulksign.Api;
using System;
using System.IO;

namespace Bulksign.ApiSamples
{
	public class AddNewSignatureToDocument
	{

		public void SendBundle()
		{

			BulkSignApi api = new BulkSignApi();

			BundleApiModel bb = new BundleApiModel();
			bb.DaysUntilExpire = 10;
			bb.DisableNotifications = false;

			RecipientApiModel recipient = new RecipientApiModel();
			recipient.Name = "Bulksign Test";
			recipient.Email = "contact@bulksign.com";
			recipient.Index = 1;
			recipient.RecipientType = RecipientTypeApi.Signer;

			DocumentApiModel document = new DocumentApiModel();
			document.Index = 1;
			document.FileName = "singlepage.pdf";

			//add new signature
			document.NewSignatures = new NewSignatureApiModel[1];

			//width,height, left and top values are in pixels
			var newSig = new NewSignatureApiModel();
			newSig.Height = 100;
			newSig.Width = 250;
			newSig.PageIndex = 1;
			newSig.Left = 20;
			newSig.Top = 30;
			//assign the signature field to the recipient. The assignment is done by the email address
			newSig.AssignedToRecipientEmail = recipient.Email;

			document.NewSignatures[0] = newSig;

			document.FileContentByteArray = new FileContentByteArray()
			{
				ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\singlepage.pdf")
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