using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetOrganizationAutomaticSigningProfiles
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
				ApiResult<AutomaticSigningProfileResultApiModel[]> result = client.GetOrganizationAutomaticSigningProfiles(token);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Found {result.Result.Length} organization automatic signing profiles ");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetOrganizationAutomaticSigningProfiles));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetOrganizationAutomaticSigningProfiles));
			}
		}
	}
}