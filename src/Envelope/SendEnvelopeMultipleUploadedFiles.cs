using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SendEnvelopeMultipleUploadedFiles
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

			//upload individually the PDF documents that will be part of the envelope

			ApiResult<string> temporaryFile = client.StoreTemporaryFile(token,new FileInput()
			{
				FileContent = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf"),
				Filename    = "bulksign_test_Sample.pdf"
			});


			if (temporaryFile.IsSuccess == false)
			{
				Console.WriteLine("Storing temporary file failed : " + temporaryFile.ErrorMessage);
				return;
			}


			ApiResult<string> temporarySecondFile = client.StoreTemporaryFile(token, new FileInput()
			{
				FileContent = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\Sample-Contract-Agreement-Template.pdf"),
				Filename    = "Sample-Contract-Agreement-Template.pdf"
			});

			if (temporarySecondFile.IsSuccess == false)
			{
				Console.WriteLine("Storing temporary file failed : " + temporarySecondFile.ErrorMessage);
				return;
			}




			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType = EnvelopeTypeApi.Serial;

			envelope.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Email = "contact@bulksign.com",
					Index = 1,
					Name = "Test",
					RecipientType = RecipientTypeApi.Signer
				}
			};

			//we specify as input the identifiers of the previously uploaded files 
			
			envelope.Documents = new[]
			{
				new DocumentApiModel()
				{
					FileIdentifier = new FileIdentifier()
					{
						Identifier = temporaryFile.Result
					}
				},
				new DocumentApiModel()
				{
					FileIdentifier = new FileIdentifier()
					{
						Identifier = temporarySecondFile.Result
					}
				},
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