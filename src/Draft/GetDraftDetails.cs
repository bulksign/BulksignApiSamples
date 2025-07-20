using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class GetDraftDetails
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

		ApiResult<string> draftResult = null;
			
		try
		{
			draftResult = client.CreateDraftFromFile(token, FileUtility.GetFileContent("bulksign_test_sample.pdf"),"bulksign_test_sample.pdf");

			if (draftResult.IsSuccess == false)
			{
				FailedRequestHandler.HandleFailedRequest(draftResult, nameof(client.CreateDraftFromFile));
				return;
			}
		}
		catch (Exception ex)
		{
			FailedRequestHandler.HandleException(ex, nameof(client.GetDraftDetails));
			return;
		}
		
		try
		{

			ApiResult<DraftDetailsResultApiModel> result = client.GetDraftDetails(token, draftResult.Result);

			if (result.IsSuccess)
			{
				Console.WriteLine($"Draft '{result.Result.Name}', id {result.Result.DraftId}");
			}
			else
			{
				FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetDraftDetails));
			}
		}
		catch (Exception ex)
		{
			FailedRequestHandler.HandleException(ex, nameof(client.GetDraftDetails));
		}
	}
}