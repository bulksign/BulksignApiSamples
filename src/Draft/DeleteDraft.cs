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
			BulksignResult<string> draftResult = client.CreateDraftFromFile(token,File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf"),"test.pdf");

			if (!draftResult.IsSuccessful)
			{
				Console.WriteLine($"Draft could noty be created : {draftResult.ErrorMessage}");
				return;
			}

			BulksignResult<string> result = client.DeleteDraft(token, draftResult.Result);

			if (result.IsSuccessful)
			{
				Console.WriteLine($"Draft with id '{result.Result}' was deleted");
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