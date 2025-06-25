using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SearchTeamMembersTemplates
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
				ApiResult<ItemResultApiModel[]> result = client.SearchTeamMembersTemplates(token, "test");

				if (result.IsSuccess)
				{
					Console.WriteLine($"Found {result.Result.Length} team member templates");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.SearchTeamMembersTemplates));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.SearchTeamMembersTemplates));
			}
		}
	}
}