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
			ApiResult<string> draftResult = client.CreateDraftFromFile(token, FileUtility.GetFileContent("bulksign_test_sample.pdf"),"bulksign_test_sample.pdf");

			if (!draftResult.IsSuccess)
			{
				Console.WriteLine($"Draft could noty be created : {draftResult.ErrorMessage}");
				return;
			}

			ApiResult<string> result = client.DeleteDraft(token, draftResult.Result);

			if (result.IsSuccess)
			{
				Console.WriteLine($"Draft with id '{result.Result}' was deleted");
			}
			else
			{
				FailedRequestHandler.HandleFailedRequest(result, nameof(client.DeleteDraft));
			}
		}
		catch (Exception ex)
		{
			FailedRequestHandler.HandleException(ex, nameof(client.DeleteDraft));
		}
	}
}