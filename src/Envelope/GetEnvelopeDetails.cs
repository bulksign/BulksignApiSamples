using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEnvelopeDetailsSample
	{
		//replace this with your own envelope id 
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
				ApiResult<EnvelopeDetailsResultApiModel> result = client.GetEnvelopeDetails(token, ENVELOPE_ID);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Envelope '{ENVELOPE_ID}' has name : '{result.Result.Name}' ");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetEnvelopeDetails));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetEnvelopeDetails));
			}
		}
	}
}