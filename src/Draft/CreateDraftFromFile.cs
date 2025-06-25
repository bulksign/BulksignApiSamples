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

<<<<<<< HEAD
			if (result.IsSuccess)
||||||| d14f9fe
			if (!result.IsSuccess)
			{
				Console.WriteLine($"Draft could noty be created : {result.ErrorMessage}");
				return;
			}
			else
=======
			if (!result.IsSuccess)
			{
				Console.WriteLine($"Draft could noty be created : {result.ErrorMessage}");
			}
			else
>>>>>>> 3f883de436c0335894c102c2832ea4cf8153327a
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