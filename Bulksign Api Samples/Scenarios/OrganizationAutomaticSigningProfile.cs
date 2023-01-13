﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class OrganizationAutomaticSigningProfile
	{

		public void GetOrganizationAutomaticSigningProfiles()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			BulksignResult<AutomaticSigningProfileResultApiModel[]> result = api.GetOrganizationAutomaticSigningProfiles(token);

			if (result.IsSuccessful)
				Console.WriteLine($"Found {result.Response.Length} organization automatic signing profiles ");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}
	}
}