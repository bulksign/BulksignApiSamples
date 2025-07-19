using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class AddAttachment
	{
		public void RunSample()
		{
			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit Authentication.cs and set your own API key there");
				return;
			}

			BulksignApiClient client = new BulksignApiClient();

			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType = EnvelopeTypeApi.Serial;
			envelope.DisableRecipientNotifications = false;

			envelope.Recipients =
			[
				new RecipientApiModel
				{
					Name          = "Recipient First",
					Email         = "add_email_address_here",
					Index         = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			];

			envelope.Documents =
			[
				new DocumentApiModel
				{
					Index    = 1,
					FileName = "singlepage.pdf",
					FileContentByteArray = new FileContentByteArray
					{
						ContentBytes = FileUtility.GetFileContent("bulksign_test_sample.pdf")
					},
					NewAttachments = new[]
					{
						new NewAttachmentApiModel
						{
							AssignedToRecipientEmail = "add_email_address_here",
							Id                       = "id_picture",
							Text                     = "Please attach your ID scanned image"
						},
						new NewAttachmentApiModel
						{
							AssignedToRecipientEmail = "add_email_address_here",
							Id                       = "id_passport",
							Text                     = "Please attach an image of your passport"
						}
					}
				}
			];

			try
			{
				ApiResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccess)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail + " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("EnvelopeId is : " + result.Result.EnvelopeId);
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.SendEnvelope));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.SendEnvelope));
			}
		}
	}
}