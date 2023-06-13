using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class CancelEnvelopes
	{
        //replace this with your own "InProgress" envelope which will be canceled 
        const string ENVELOPE_ID = "000000000000000000000000";

		public void RunSample()
		{
			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient client = new BulksignApiClient();

			try
			{
				//this works only for "InProgress" envelopes
				BulksignResult<string> result = client.CancelEnvelope(token, ENVELOPE_ID);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"Envelope was canceled");
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