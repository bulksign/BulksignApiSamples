using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SendEnvelopeADSignerAuthentication
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
					Name = "Bulksign Test",
					Email = "recipient_email@test.com",
					Index = 2,
					RecipientType = RecipientTypeApi.Signer,
					
					//this will enable AD authentication for the signer
					//in "Details" fields just set the name of the AD user
					RecipientAuthenticationMethods = new []
					{
						new RecipientAuthenticationApiModel()
						{
							AuthenticationType = RecipientAuthenticationTypeApi.ActiveDirectory,
							Details = "aduser"  //set the actual domain name of the user
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
				ApiResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccess)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail + " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("Envelope id is : " + result.Result.EnvelopeId);
				}
				else
				{
					Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
				}
			}
			catch (BulksignApiException bex)
			{
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}