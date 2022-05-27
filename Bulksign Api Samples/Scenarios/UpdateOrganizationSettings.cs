using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class UpdateOrganizationSettings
	{
		public void Update()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			//we need to set only the values you want updated
			OrganizationUpdateSettingsApiModel newSettings = new OrganizationUpdateSettingsApiModel();

			newSettings.Name = "New Organization Name";

			BulksignResult<string> result = api.UpdateOrganizationSettings(token, newSettings);

			if (result.IsSuccessful)
				Console.WriteLine("Organization settings were successfully updated");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}
	}
}