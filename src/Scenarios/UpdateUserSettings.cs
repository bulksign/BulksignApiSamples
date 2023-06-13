﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class UpdateUserSettings
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

			//only set the properties that you want modified
			UserUpdateSettingsApiModel model = new UserUpdateSettingsApiModel()
			{
				JobTitle = "My job",
				DefaultDraftLanguage = "en-US",
				NotificationRecipientOpenedSignStep = true,
				LastName = "MyLastName"
			};

			try
			{
				//this will update all NON-NULL values that we send
				BulksignResult<string> result = client.UpdateUserSettings(token, model);

				if (result.IsSuccessful == false)
				{
					Console.WriteLine($"Request failed : RequestId {result.RequestId}, ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
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