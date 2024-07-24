using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SendEnvelopeDocumentNetworkShare
	{

		//NOTE : specifying a network path for input files ONLY works on the on-premise version of Bulksign

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

			envelope.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Email = "test@test.com",
					Index = 1,
					Name = "Test",
					RecipientType = RecipientTypeApi.Signer
				}
			};

			
			envelope.Documents = new[]
			{
				new DocumentApiModel()
				{
					FileNetworkShare = new FileNetworkShare()
					{
						Path = @"\\DocumentShare\\mydocument.pdf"
					}
				},
				new DocumentApiModel()
				{
					FileNetworkShare = new FileNetworkShare()
					{
						Path = @"\\DocumentShare\\other.pdf"
					}
				},
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
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}