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

		try
		{
			ApiResult<string> draftResult = client.CreateDraftFromFile(token,File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf"),"test.pdf");

			if (!draftResult.IsSuccess)
			{
				Console.WriteLine($"Draft could noty be created : {draftResult.ErrorMessage}");
				return;
			}

			ApiResult<DraftDetailsResultApiModel> result = client.GetDraftDetails(token, draftResult.Result);

			if (result.IsSuccess)
			{
				Console.WriteLine($"Draft '{result.Result.Name}', id {result.Result.DraftId}");
			}
			else
			{
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
			}
		}
		catch (BulksignApiException bex)
		{
			//handle failed request here
			Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
		}
	}
}