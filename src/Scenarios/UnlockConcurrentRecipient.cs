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
				EnvelopeId     = "000000000000000000000000",
				RecipientEmail = "email_of_recipient_which_locked_signing"
			};

			try
			{
				BulksignResult<string> result = client.UnlockConcurrentRecipient(token, model);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"{model.EnvelopeId} was unlocked");
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
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