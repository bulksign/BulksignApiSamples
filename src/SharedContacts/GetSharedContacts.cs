﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetSharedContacts
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
				ApiResult<ContactResultApiModel[]> result = client.GetSharedContacts(token);

				if (result.IsSuccess)
				{
					Console.WriteLine($" {result.Result.Length} contacts found ");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetSharedContacts));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetSharedContacts));
			}
		}
	}
}