using System;
using System.IO;
using System.Linq;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class OpenIdConnectAuthenticationForSigner
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

			//this will return all authentication providers defined per organization
			//Obviously you need to define at least 1 provider for this to work
			ApiResult<AuthenticationProviderResultApiModel[]> providers = client.GetAuthenticationProviders(token);


			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType = EnvelopeTypeApi.Serial;
			envelope.Name = "Test envelope";

			envelope.Recipients = new[]
			{
				new RecipientApiModel
				{
					Name = "Bulksign Test",
					Email = "recipient_email@test.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer,

					//set the user authentication here and use the first retrieved provider
					RecipientAuthenticationMethods = new[]
					{
						new RecipientAuthenticationApiModel()
						{
							AuthenticationType = RecipientAuthenticationTypeApi.AuthenticationProvider,
							Details = providers.Result.FirstOrDefault()?.Identifier
						}
					}
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel
				{
					Index = 1,
					FileName = "bulksign_test_sample.pdf",
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