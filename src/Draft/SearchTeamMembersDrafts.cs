using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SearchTeamMembersDrafts
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
				ApiResult<ItemResultApiModel[]> result = client.SearchTeamMembersDrafts(token, "test");

				if (result.IsSuccess)
				{
					Console.WriteLine($"Found {result.Result.Length} team member drafts");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.SearchTeamMembersDrafts));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.SearchTeamMembersDrafts));
			}
		}
	}
}