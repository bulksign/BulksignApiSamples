using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetDrafts
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
				BulksignResult<DraftItemResultApiModel[]> result = client.GetDrafts(token);

				if (result.IsSuccessful == false)
				{
					Console.WriteLine($"Request failed : RequestId {result.RequestId}, ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
				}
				else
				{
					Console.WriteLine(result.Response.Length + " drafts found");
				}
			}
			catch (BulksignException bex)
			{
				//handle failed request here. See
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}