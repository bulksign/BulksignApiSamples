using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEnvelopesToSign
	{
		public void GetEnvelopes()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			BulksignResult<EnvelopeToSignResultApiModel[]> result = api.GetEnvelopesToSign(token);

			if (result.IsSuccessful)
				Console.WriteLine($"Found {result.Response.Length} envelopes to sign");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}


	}
}