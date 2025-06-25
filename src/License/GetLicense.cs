using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class LicenseInformation
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
				ApiResult<LicenseResultApiModel> result = client.GetLicense(token);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Remaining envelopes : {result.Result.EnvelopesTotal - result.Result.EnvelopesUsed}");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetLicense));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetLicense));
			}
		}
	}
}