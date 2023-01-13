using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEnvelopeStatus
	{
		public void GetEnvelope()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			BulksignResult<EnvelopeStatusTypeApi> result = api.GetEnvelopeStatus(token , "your_envelope_id");

			if (result.IsSuccessful)
				Console.WriteLine($"Envelope status is {result.Response.ToString()}");
			else
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
		}


	}
}