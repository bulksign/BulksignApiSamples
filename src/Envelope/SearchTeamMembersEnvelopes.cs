using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SearchTeamMembersEnvelopes
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
				ApiResult<ItemResultApiModel[]> result = client.SearchTeamMembersEnvelopes(token, "test");

				if (result.IsSuccess)
				{
					Console.WriteLine($"Found {result.Result.Length} team member envelopes");
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