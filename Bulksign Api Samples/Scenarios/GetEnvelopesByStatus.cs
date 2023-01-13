using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEnvelopesByStatus
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

			BulksignResult<ItemResultApiModel[]> result = api.GetEnvelopesByStatus(token, EnvelopeStatusTypeApi.InProgress);

			if (result.IsSuccessful)
				Console.WriteLine($"{result.Response.Length} envelopes found");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}

	}
}