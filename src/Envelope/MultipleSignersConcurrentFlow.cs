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
					FileName = "test.pdf",
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
					}
				}
			};

			try
			{
				BulksignResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccessful)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail + " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("Envelope id is : " + result.Result.EnvelopeId);
				}
				else
				{
					Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
				}
			}
			catch (BulksignException bex)
			{
				//handle failed request here.
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}