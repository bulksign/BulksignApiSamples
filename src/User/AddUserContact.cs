using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class AddUserContact
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

			NewContactApiModel contact = new NewContactApiModel
			{
				Address = "address",
				Company = "My Company",
				Email   = "email@domain.net",
				Name    = "Contact Name"
			};

			try
			{
				BulksignResult<string> result = client.AddContact(token, contact);

				if (result.IsSuccessful)
				{
					Console.WriteLine("Contact was successfully added");
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