using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class Version
	{
		public void GetVersionInformation()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			string version = api.GetVersion();

			Console.WriteLine($"Bulksign version : {version}");
		}
	}
}