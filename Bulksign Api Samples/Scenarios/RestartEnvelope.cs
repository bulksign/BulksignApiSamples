using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class RestartEnvelopes
	{
        //replace this with your own expired envelope id 
        const string EXPIRED_ENVELOPE_ID = "000000000000000000000000";

		public void Restart()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			BulksignResul<string> result = api.RestartEnvelope(token, EXPIRED_ENVELOPE_ID);

			if (result.IsSuccessful)
            {
				Console.WriteLine($"Envelope was successfully restarted");
            }
			else
            {
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
            }
		}

	}
}