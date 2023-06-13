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
				BulksignResult<LicenseResultApiModel> result = client.GetLicense(token);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"Remaining envelopes : {result.Response.EnvelopesTotal - result.Response.EnvelopesUsed}");
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