using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class LicenseInformation
	{
		public void RunSample()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			BulksignResult<LicenseResultApiModel> result = api.GetLicense(token);

			if (result.IsSuccessful)
			{
				Console.WriteLine($"Remaining envelopes : {result.Response.EnvelopesTotal - result.Response.EnvelopesUsed}");
			}
			else
			{
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
			}
		}
	}
}