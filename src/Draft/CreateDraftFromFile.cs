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
			ApiResult<string> result = client.CreateDraftFromFile(token,File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf"),"test.pdf");

			if (!result.IsSuccess)
			{
				Console.WriteLine($"Draft could noty be created : {result.ErrorMessage}");
			}
			else
			{
				Console.WriteLine($"Draft with id '{result.Result}' was created");
			}
		}
		catch (BulksignApiException bex)
		{
			//handle failed request here
			Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
		}
	}
}