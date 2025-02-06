using Bulksign.Api;
using Bulksign.DomainLogic.Api;

namespace Bulksign.ApiSamples
{
	public class HandleErrorsSampleCode
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
			envelope.DisableRecipientNotifications = false;

			envelope.Recipients = new[]
			{
				new RecipientApiModel
				{
					Name = "Recipient First",
					Email = "add_email_address_here",  //please enter a valid email address 
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel
				{
					Index = 1,
					FileName = "singlepage.pdf",
					FileContentByteArray = new FileContentByteArray
					{
						ContentBytes =
							File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_sample.pdf")
					}
				}
			};

			try
			{
				ApiResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccess)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail +
					                  " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("EnvelopeId is : " + result.Result.EnvelopeId);

					return;
				}

				//the request failed 
				Console.WriteLine($"Request failed errorCode {result.ErrorCode}, errorMessage : {result.ErrorMessage} , requestId {result.RequestId}");

				//handle the expected error codes
				switch (result.ErrorCode)
				{
					case ApiErrorCode.API_ERROR_CODE_NO_RECIPIENTS:
						break;
					
					case ApiErrorCode.API_ERROR_CODE_FIELD_ASSIGNMENT_INVALID_RECIPIENT_IDENTIFIER:
						break;
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