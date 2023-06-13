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
				BulksignResult<ItemResultApiModel[]> result = client.GetEnvelopesByStatus(token, EnvelopeStatusTypeApi.InProgress);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"{result.Response.Length} envelopes found");
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