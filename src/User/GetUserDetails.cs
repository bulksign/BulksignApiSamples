using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetUserDetails
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
				ApiResult<UserDetailsApiModel> result = client.GetUserDetails(token);

				if (result.IsSuccess == false)
				{
					Console.WriteLine($"Request failed : RequestId {result.RequestId}, ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetUserDetails));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetUserDetails));
			}
		}
	}
}