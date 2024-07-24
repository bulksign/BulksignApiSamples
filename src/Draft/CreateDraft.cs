using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class DeleteDraft
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

			DraftApiModel newDraft = new DraftApiModel()
			{
				Name = "Test Draft",
				AllowRecipientDelegation = false,
				CertifyDocumentsBeforeSending = false,
				EnableBatchSign = true
			};

			BulksignResult<string> result = client.CreateDraft(token,newDraft);

			if (result.IsSuccessful)
			{
				Console.WriteLine($"Draft with id '{result.Result}' was created");
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