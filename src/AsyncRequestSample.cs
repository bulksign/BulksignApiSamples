


using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetDraftsAsync
	{
		public  async Task RunSample()
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
				ApiResult<DraftItemResultApiModel[]> result = await client.GetDraftsAsync(token);

				if (result.IsSuccess == false)
				{
					Console.WriteLine($"Request failed : RequestId {result.RequestId}, ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetDrafts));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetDrafts));
			}
		}
	}
	
	
}