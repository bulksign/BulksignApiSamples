using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples.Scenarios
{
	public class OrganizationUsers
	{
		public void GetOrganizationUsers()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			BulksignResult<OrganizationUserApiModel[]> result = api.GetOrganizationUsers(token);

			if (result.IsSuccessful)
				Console.WriteLine($"Found {result.Response.Length} users ");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}
	}
}