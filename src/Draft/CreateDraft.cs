using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class CreateDraft
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

			ApiResult<string> result = client.CreateDraft(token,newDraft);

			if (result.IsSuccess)
			{
				Console.WriteLine($"Draft with id '{result.Result}' was created");
			}
			else
			{
				FailedRequestHandler.HandleFailedRequest(result, nameof(client.CreateDraft));
			}
		}
		catch (Exception ex)
		{
			FailedRequestHandler.HandleException(ex, nameof(client.CreateDraft));
		}
	}
}