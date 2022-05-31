using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SearchTeamMembersEnvelopes
	{
		public void Search()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			BulksignResult<ItemResultApiModel[]> result = api.SearchTeamMembersEnvelopes(token, "test");

			if (result.IsSuccessful)
				Console.WriteLine($"Found {result.Response.Length} team member envelopes");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}

	}
}