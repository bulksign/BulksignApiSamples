﻿using System;
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
			BulksignResult<AuthenticationProviderResultApiModel[]> providers = client.GetAuthenticationProviders(token);


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
							Details = providers.Result.FirstOrDefault().Identifier
						}
					}
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
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}