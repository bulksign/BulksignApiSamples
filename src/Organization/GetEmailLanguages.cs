using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEmailLanguages
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
				ApiResult<string[]> result = client.GetEmailLanguages(token);

				if (result.IsSuccess)
				{
					foreach (string lang in result.Result)
					{
						Console.WriteLine($"Found email language {lang} ");
					}
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetEmailLanguages));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetEmailLanguages));
			}
		}
	}
}