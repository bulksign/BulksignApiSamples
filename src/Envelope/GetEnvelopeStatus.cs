using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEnvelopeStatus
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

			try
			{
				ApiResult<EnvelopeStatusTypeApi> result = client.GetEnvelopeStatus(token, "your_envelope_id");

				if (result.IsSuccess)
				{
					Console.WriteLine($"Envelope status is {result.Result.ToString()}");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetEnvelopeStatus));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetEnvelopeStatus));
			}
		}
	}
}