using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class RestartEnvelopes
	{
		//replace this with your own expired envelope id 
		private const string EXPIRED_ENVELOPE_ID = "000000000000000000000000";

		public void RunSample()
		{
			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit Authentication.cs and set your own API key there");
				return;
			}

			BulksignApiClient client = new BulksignApiClient();

			try
			{
				BulksignResult<string> result = client.RestartEnvelope(token, EXPIRED_ENVELOPE_ID);

				if (result.IsSuccessful)
				{
					Console.WriteLine("Envelope was successfully restarted");
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
				}
			}
			catch (BulksignException bex)
			{
				//handle failed request here. See
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}