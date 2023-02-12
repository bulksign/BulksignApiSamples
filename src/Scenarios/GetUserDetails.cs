using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetUserDetails
	{
		public void RunSample()
		{

			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			BulksignResult<UserDetailsApiModel> result = api.GetUserDetails(token);
			
			//check if the result was successful

			if (result.IsSuccessful == false)
			{
				Console.WriteLine($"Request failed : RequestId {result.RequestId}, ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
			}
			else
			{
				Console.WriteLine($"User name is : {result.Response.FirstName} {result.Response.LastName}");
			}


		}

	}
}