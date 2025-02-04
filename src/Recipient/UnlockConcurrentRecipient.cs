using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class UnlockConcurrentRecipientSample
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

			UnlockConcurrentRecipientApiModel model = new UnlockConcurrentRecipientApiModel
			{
				EnvelopeId     = "000000000000000000000000", //replace with correct envelope_id
				RecipientEmail = "email_of_recipient_which_locked_signing"
			};

			try
			{
				ApiResult<string> result = client.UnlockConcurrentRecipient(token, model);

				if (result.IsSuccess)
				{
					Console.WriteLine($"{model.EnvelopeId} was unlocked");
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
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