using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class Version
	{
		public void RunSample()
		{
			//NOTE : this is the only API which doesn't require authentication. It's usually used for health checks

			BulksignApiClient api = new BulksignApiClient();

			string version = api.GetVersion();

			Console.WriteLine($"Bulksign version : {version}");
		}
	}
}