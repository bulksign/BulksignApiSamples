using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetSharedContacts
	{
		public void GetContacts()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			BulksignResult<ContactResultApiModel[]> result = api.GetSharedContacts(token);

			if (result.IsSuccessful)
				Console.WriteLine($" {result.Response.Length} contacts found ");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}
	}
}