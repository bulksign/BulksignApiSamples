using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class MultipleSignersConcurrentFlow
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
			envelope.EnvelopeType = EnvelopeTypeApi.Concurrent;
			envelope.DisableRecipientNotifications = false;
			envelope.Name = "Test envelope";

			envelope.Recipients = new[]
			{
				//the Index property will determine the order in which the recipients will sign the document(s). 

				new RecipientApiModel()
				{
					Name = "Bulksign First User",
					Email = "email@email.com",
					RecipientType = RecipientTypeApi.Signer
				},
				new RecipientApiModel()
				{
					Name = "Bulksign Second User",
					Email = "contact@bulksign.com",
					RecipientType = RecipientTypeApi.Signer,
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel()
				{
					Index = 1,
					FileName = "bulksign_test_sample.pdf",
					FileContentByteArray = new FileContentByteArray()
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