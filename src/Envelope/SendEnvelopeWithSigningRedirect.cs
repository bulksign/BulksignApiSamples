using System;
using System.Diagnostics;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	
	//This sample shows sending an envelope and then redirecting 
	//immediately for signing
	public class SendEnvelopeWithSigningRedirect
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

			ApiResult<string> temporaryFile = client.StoreTemporaryFile(token,new FileInput()
			{
				FileContent = FileUtility.GetFileContent("bulksign_test_sample.pdf"),
				Filename    = "bulksign_test_sample.pdf"
			});


			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType = EnvelopeTypeApi.Serial;

			envelope.ExpirationDays = 10;


			envelope.Recipients =
			[
				new RecipientApiModel()
				{
					Email = "your_email_address_here",
					Index = 1,
					Name = "Test",
					RecipientType = RecipientTypeApi.Signer,
				}
			];

			//we specify as input the identifiers of the previously uploaded files 
			envelope.Documents =
			[
				new DocumentApiModel()
				{
					FileIdentifier = new FileIdentifier()
					{
						Identifier = temporaryFile.Result
					}
				}
			];

			try
			{
				ApiResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Envelope id is {result.Result.EnvelopeId}, opening browser and redirecting for signing");

					try
					{
						string signingUrl = result.Result.RecipientAccess[0].SigningUrl;
						
						//open browser for signing
						Process.Start(signingUrl);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						return;
					}
					
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