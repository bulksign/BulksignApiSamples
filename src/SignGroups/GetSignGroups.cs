using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class GetSignGroups
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
			ApiResult<OrganizationSignGroupApiModel[]> result = client.GetOrganizationSignGroups(token);

			if (result.IsSuccess)
			{
				Console.WriteLine($"Found {result.Result.Length} sign groups");
			}
			else
			{
				FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetOrganizationSignGroups));
			}
		}
		catch (Exception ex)
		{
			FailedRequestHandler.HandleException(ex, nameof(client.GetOrganizationSignGroups));
		}
	}
}