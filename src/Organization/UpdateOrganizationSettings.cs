using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class UpdateOrganizationSettings
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

			//NOTE : we need to set only the values that we want to update. Everything lese must be null
			
			
			//Let's say we want to update the org name and one of the policies
			
			OrganizationUpdateSettingsApiModel newSettings = new OrganizationUpdateSettingsApiModel();

			newSettings.Settings = new OrganizationBasicSettingsApiModel()
			{
				Name = "New Organization Name"
			};

			newSettings.Policies = new OrganizationPoliciesApiModel()
			{
				ForceBatchSign = true
			};

			try
			{
				ApiResult<string> result = client.UpdateOrganizationSettings(token, newSettings);

				if (result.IsSuccess)
				{
					Console.WriteLine("Organization settings were successfully updated");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.UpdateOrganizationSettings));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.UpdateOrganizationSettings));
			}
		}
	}
}
