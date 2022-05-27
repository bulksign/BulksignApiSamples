﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class AddSharedContact
	{
		public void AddContact()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			NewContactApiModel contact = new NewContactApiModel
			{
				Address = "address",
				Company = "My Company",
				Email   = "email@domain.net",
				Name    = "Contact Name"
			};

			BulksignResult<string> result = api.AddSharedContact(token, contact);

			if (result.IsSuccessful)
				Console.WriteLine("Contact was successfully added");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}
	}
}