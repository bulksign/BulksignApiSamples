using Bulksign.Api;
using System;
using System.IO;

namespace Bulksign.ApiSamples
{
	public class AddNewSignatureToDocument
	{

		public void SendBundle()
		{

			AuthorizationApiModel token = new ApiKeys().GetAuthorizationToken();

			if (string.IsNullOrEmpty(token.UserToken))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}



			BulkSignApi api = new BulkSignApi();

			BundleApiModel bundle = new BundleApiModel();
			bundle.DaysUntilExpire = 10;
			bundle.DisableNotifications = false;



			bundle.Recipients = new []
			{
				new RecipientApiModel()
				{
					Name = "Bulksign Test",
					Email = "contact@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				} 
			};

		
			bundle.Documents = new[]
			{
				new DocumentApiModel()
				{
					Index = 1,
					FileName = "singlepage.pdf",
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\singlepage.pdf")
					},
					NewSignatures = new []
					{
						new NewSignatureApiModel()
						{
							//width,height, left and top values are in pixels
							Height = 100,
							Width = 250,
							PageIndex = 1,
							Left = 20,
							Top = 30,
							//assign the signature field to the recipient. The assignment is done by the email address
							AssignedToRecipientEmail = bundle.Recipients[0].Email
						}
					}
				}, 
			};
			

			BulksignResult<SendBundleResultApiModel> result = api.SendBundle(token, bundle);

			if (result.IsSuccessful)
			{
				Console.WriteLine("Access code for recipient " + result.Response.AccessCodes[0].RecipientName + " is " + result.Response.AccessCodes[0].AccessCode);
				Console.WriteLine("Bundle id is : " + result.Response.BundleId);
			}
			else
			{
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
			}

		}
	}
}