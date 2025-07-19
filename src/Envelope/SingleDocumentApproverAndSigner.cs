using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SingleDocumentApproverAndSigner
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
			envelope.Name = "Test envelope";

			envelope.Recipients = new[]
			{
				new RecipientApiModel
				{
					Name = "Approver Recipient",
					Email = "recipient_email_approver@test.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Approver
				},
				new RecipientApiModel
				{
					Name = "Bulksign Test",
					Email = "recipient_email@test.com",
					Index = 2,
					RecipientType = RecipientTypeApi.Signer
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel
				{
					Index = 1,
					FileName = "test.pdf",
					FileContentByteArray = new FileContentByteArray
					{
						ContentBytes = FileUtility.GetFileContent("bulksign_test_sample.pdf")
					}
				}
			};

			try
			{
				ApiResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccess)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail + " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("Envelope id is : " + result.Result.EnvelopeId);
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