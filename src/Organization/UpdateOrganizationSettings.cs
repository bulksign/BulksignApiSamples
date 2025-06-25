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

			//we need to set only the values you want updated
			OrganizationUpdateSettingsApiModel newSettings = new OrganizationUpdateSettingsApiModel();

			newSettings.Name = "New Organization Name";

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
