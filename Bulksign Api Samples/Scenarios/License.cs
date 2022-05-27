using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples.Scenarios
{
	public class License
	{
		public void GetLicenseInformation()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			BulksignResult<LicenseResultApiModel> result = api.GetLicense(token);

			if (result.IsSuccessful)
				Console.WriteLine("Contact was successfully added");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}

	}
}