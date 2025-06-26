using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEnvelopesByStatus
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
				ApiResult<ItemResultApiModel[]> result = client.GetEnvelopesByStatus(token, EnvelopeStatusTypeApi.InProgress);

				if (result.IsSuccess)
				{
					Console.WriteLine($"{result.Result.Length} envelopes found");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetEnvelopesByStatus));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetEnvelopesByStatus));
			}
		}
	}
}