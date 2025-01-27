using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class DownloadEnvelopeCompletedDocuments
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

			try
			{
				ApiResult<byte[]> result = client.DownloadEnvelopeCompletedDocuments(token, "your_envelope_id");

				if (result.IsSuccess)
				{
					//the result here will by a byte[] of a zip file which contains all signed documents + audit trail file
					Console.WriteLine($"File size :  {result.Result.Length}");
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
				}
			}
			catch (BulksignApiException bex)
			{
				//handle failed request here. See
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}