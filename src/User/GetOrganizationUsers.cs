using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class OrganizationUsers
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
				ApiResult<OrganizationUserApiModel[]> result = client.GetOrganizationUsers(token);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Found {result.Result.Length} users ");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetOrganizationUsers));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetOrganizationUsers));
			}
		}
	}
}