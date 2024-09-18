using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetEnvelopeDetailsSample
	{
		//replace this with your own envelope id 
		const string ENVELOPE_ID = "000000000000000000000000";

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
				BulksignResult<EnvelopeDetailsResultApiModel> result = client.GetEnvelopeDetails(token, ENVELOPE_ID);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"Envelope '{ENVELOPE_ID}' has name : '{result.Result.Name}' ");
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