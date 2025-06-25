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
				ApiResult<string> result = client.AddSharedContact(token, contact);

				if (result.IsSuccess)
				{
					Console.WriteLine("Contact was successfully added");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.AddSharedContact));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.AddSharedContact));
			}
		}
	}
}