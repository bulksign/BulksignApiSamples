using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class CreateDraftFromFile
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
			ApiResult<string> result = client.CreateDraftFromFile(token, FileUtility.GetFileContent("bulksign_test_sample.pdf"),"bulksign_test_sample.pdf");

			if (result.IsSuccess)
			{
				Console.WriteLine($"Draft with id '{result.Result}' was created");
			}
			else
			{
				FailedRequestHandler.HandleFailedRequest(result, nameof(client.CreateDraftFromFile));
			}
		}
		catch (Exception ex)
		{
			FailedRequestHandler.HandleException(ex, nameof(client.CreateDraftFromFile));
		}
	}
}