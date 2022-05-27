using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SigningImprints
	{
		public void GetSigningImprints()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			BulksignResult<string[]> result = api.GetSignatureImprints(token);

			if (result.IsSuccessful)
				Console.WriteLine($"Found {result.Response.Length} signature imprints");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}
	}
}