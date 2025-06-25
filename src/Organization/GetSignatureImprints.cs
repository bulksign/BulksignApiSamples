using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetSignatureImprints
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
				ApiResult<string[]> result = client.GetSignatureImprints(token);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Found {result.Result.Length} signature imprints");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetSignatureImprints));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetSignatureImprints));
			}
		}
	}
}