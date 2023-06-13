﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class AuthenticationProviders
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

			try
			{
				BulksignResult<AuthenticationProviderResultApiModel[]> result = client.GetAuthenticationProviders(token);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"Found {result.Response.Length} authentication providers");
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