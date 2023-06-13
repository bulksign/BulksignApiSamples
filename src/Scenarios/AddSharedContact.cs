using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class AddSharedContact
	{
		public void RunSample()
		{
			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
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
				BulksignResult<string> result = client.AddSharedContact(token, contact);

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
				//handle failed request here. See
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}