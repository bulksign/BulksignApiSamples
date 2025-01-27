using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEnvelopesToSign
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
				ApiResult<EnvelopeToSignResultApiModel[]> result = client.GetEnvelopesToSign(token);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Found {result.Result.Length} envelopes to sign");
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
				}
			}
			catch (BulksignApiException bex)
			{
				//handle failed request here. See
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}