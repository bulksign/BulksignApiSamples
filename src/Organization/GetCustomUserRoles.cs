using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetCustomUserRoles
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
				ApiResult<string[]> result = client.GetCustomUserRoles(token);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Found {result.Result.Length} custom user roles");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetCustomUserRoles));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetCustomUserRoles));
			}
		}
	}
}