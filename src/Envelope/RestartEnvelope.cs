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
				ApiResult<string> result = client.RestartEnvelope(token, EXPIRED_ENVELOPE_ID);

				if (result.IsSuccess)
				{
					Console.WriteLine("Envelope was successfully restarted");
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