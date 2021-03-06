using Bulksign.Api;
using System;
using System.IO;

namespace Bulksign.ApiSamples
{
	public class AddNewSignatureToDocument
	{

		public void SendEnvelope()
		{

			AuthenticationApiModel token = new ApiKeys().GetAuthorizationToken();

			if (string.IsNullOrEmpty(token.Token))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}



			BulkSignApi api = new BulkSignApi();

			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.DaysUntilExpire = 10;
			envelope.DisableSignerEmailNotifications = false;



			envelope.Recipients = new []
			{
				new RecipientApiModel()
				{
					Name = "Bulksign Test",
					Email = "contact@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				} 
			};

		
			envelope.Documents = new[]
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
							AssignedToRecipientEmail = envelope.Recipients[0].Email
						}
					}
				}, 
			};
			

			BulksignResult<SendEnvelopeResultApiModel> result = api.SendEnvelope(token, envelope);

			if (result.IsSuccessful)
			{
				Console.WriteLine("Access code for recipient " + result.Response.RecipientAccess[0].RecipientEmail + " is " + result.Response.RecipientAccess[0].AccessCode);
				Console.WriteLine("EnvelopeId is : " + result.Response.EnvelopeId);
			}
			else
			{
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
			}

		}
	}
}