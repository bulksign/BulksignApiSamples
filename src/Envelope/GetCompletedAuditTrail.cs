using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetCompletedAuditTrail
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
				BulksignResult<AuditTrailEntryApiModel[]> response = client.GetCompletedAuditTrail(token, "your_completed_envelope_id");

				if (response.IsSuccessful == false)
				{
					Console.WriteLine("ERROR : " + response.ErrorCode + " " + response.ErrorMessage);
					return;
				}


				//Let's say we are interested in finding all dates when signing was finished, so filter by specific AuditTrailType to find those entries

				AuditTrailEntryApiModel[] finishedDates = response.Result.Where(a => a.AuditTrailType == AuditTrailTypeApi.Finished).ToArray();


				//you can map the recipient identifier to the response of GetEnvelopeDetails to find the recipient info (email, name , etc...)

				foreach (AuditTrailEntryApiModel date in finishedDates)
				{
					Console.WriteLine($"Signer with identifier '{date.RecipientIdentifier}' finished signing at '{date.EntryDateUTC.ToString()}'");
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