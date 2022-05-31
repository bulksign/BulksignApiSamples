﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SearchTeamMembersDrafts
	{
		public void Search()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			BulksignResult<ItemResultApiModel[]> result = api.SearchTeamMembersDrafts(token, "test");

			if (result.IsSuccessful)
				Console.WriteLine($"Found {result.Response.Length} team member drafts");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}


	}
}