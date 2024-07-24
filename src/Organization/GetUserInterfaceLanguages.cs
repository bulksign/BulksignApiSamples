using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetUserInterfaceLanguages
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
				BulksignResult<string[]> result = client.GetUserInterfaceLanguages(token);

				if (result.IsSuccessful)
				{
					foreach (string lang in result.Result)
					{
						Console.WriteLine($"Found UI language {lang} ");
					}
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