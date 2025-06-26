using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class CustomDocumentAccess
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
			envelope.EnvelopeType    = EnvelopeTypeApi.Serial;
			envelope.Name            = "Test envelope";

			envelope.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Name = "Bulksign Test",
					Email = "contact@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				},

				new RecipientApiModel()
				{
					Name = "Second Recipient",
					Email = "second@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			};



			envelope.Documents = new[]
			{
				new DocumentApiModel()
				{
					Index = 1,
					FileName = "bulksign_test_Sample.pdf",
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
					}
				},
				new DocumentApiModel()
				{
					Index = 2,
					FileName = "forms.pdf",
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\forms.pdf")
					}
				}
			};

			envelope.FileAccessMode = FileAccessModeTypeApi.Custom;

			//assign different files to different recipients

			envelope.CustomFileAccess = new[]
			{
				//assign first file to first recipient
				new CustomFileAccessApiModel()
				{
					RecipientEmail = envelope.Recipients[0].Email,
					FileNames = new []{ "bulksign_test_Sample.pdf" }
				},

				//assign first file to first recipient
				new CustomFileAccessApiModel()
				{
					RecipientEmail = envelope.Recipients[1].Email,
					FileNames = new[]
					{
						"forms.pdf"
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