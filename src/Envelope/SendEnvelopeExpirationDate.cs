using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SendEnvelopeExpirationDate
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

			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType = EnvelopeTypeApi.Serial;


			//the envelope expiration date can be specified in 2 ways :


			//relative expiration to the send timestamp, for example this envelope will be valid for 10 days since it will be sent
			envelope.ExpirationDays = 10;


			//absolute expiration date where an UTC timestamp is specified.
			//2 things to note here :
			//- the minimum expiration date for an envelope is 3h, if you specify a smaller window of time you'll get an error
			//- if the expiration date is higher than the maximum allowed date (which by default is 28 days), the expiration date will be reset automatically to the max allowed date
			
			//envelope.ExpirationDateAbsolute = "2034-08-10T16:26:46Z";


			//expiration date is optional, if it not set the envelope will be valid for the maximum allowed number of days (by default is 28)


			envelope.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Email = "your_email_address_here",
					Index = 1,
					Name = "Test",
					RecipientType = RecipientTypeApi.Signer,
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