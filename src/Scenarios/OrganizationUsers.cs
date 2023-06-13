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
				BulksignResult<OrganizationUserApiModel[]> result = client.GetOrganizationUsers(token);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"Found {result.Response.Length} users ");
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
				}
			}
			catch (BulksignException bex)
			{
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}