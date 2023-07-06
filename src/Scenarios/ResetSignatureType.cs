using Bulksign.Api;
namespace Bulksign.ApiSamples;

public class ResetSignatureType
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
			ResetSignatureTypeApiModel model = new ResetSignatureTypeApiModel()
			{
				EnvelopeId = "your_envelope_id",

				//the new type of signature which will be assigned to the recipient
				SignatureType = Api.ResetSignatureType.ClickToSign,

				//we'll specify the recipient by the index (in this sample being the first recipient)
				ByIndex = new FindRecipientByIndexApiModel()
				{
					Index = 1
				}
			};

			BulksignResult<int> result = client.ResetSignatureType(token, model);

			if (result.IsSuccessful)
			{
				Console.WriteLine($"{result.Result} signature fields were changed");
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