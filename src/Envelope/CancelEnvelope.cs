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
				Console.WriteLine("Please edit Authentication.cs and set your own API key there");
				return;
			}

			BulksignApiClient client = new BulksignApiClient();

			try
			{
				//this works only for "InProgress" envelopes
				ApiResult<string> result = client.CancelEnvelope(token, ENVELOPE_ID);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Envelope was canceled");
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