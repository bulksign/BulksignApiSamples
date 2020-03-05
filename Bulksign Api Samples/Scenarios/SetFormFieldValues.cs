using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SetFormFieldValues
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

			bb.Recipients = new[]
			{
					 firstRecipient
			};

			DocumentApiModel document = new DocumentApiModel();
			document.Index = 2;
			document.FileName = "forms.pdf";
			document.FileContentByteArray = new FileContentByteArray()
			{
				ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\forms.pdf")
			};

			//set pdf from fields values

			document.OverwriteValues = new OverwriteFieldValueApiModel[2];
			document.OverwriteValues[0] = new OverwriteFieldValueApiModel()
			{
				FieldName = "Text1",
				FieldValue = "This is a test text"
			};

			document.OverwriteValues[1] = new OverwriteFieldValueApiModel()
			{
				FieldName = "Group3",
				FieldValue = "Choice2"
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