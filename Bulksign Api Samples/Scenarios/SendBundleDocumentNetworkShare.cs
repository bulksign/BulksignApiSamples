using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples.Scenarios
{
	public class SendBundleDocumentNetworkShare
	{

		public void SendBundle()
		{
			BulkSignApi api = new BulkSignApi();


			AuthorizationApiModel token = new ApiKeys().GetAuthorizationToken();

			if (string.IsNullOrEmpty(token.UserToken))
			{
				Console.WriteLine("Please edit ApiKeys.cs and put your own token/email");
				return;
			}

			BundleApiModel bundle = new BundleApiModel();

			bundle.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Email = "test@test.com",
					Index = 1,
					Name = "Test",
					RecipientType = RecipientTypeApi.Signer
				}
			};


			bundle.Documents = new []
			{
				new DocumentApiModel()
				{
					FileNetworkShare = new FileNetworkShare()
					{
						Path = @"\\DocumentShare\\mydocument.pdf"
					}
				},
				new DocumentApiModel()
				{
					FileNetworkShare = new FileNetworkShare()
					{
						Path = @"\\DocumentShare\\other.pdf"
					}
				},
			};

			BulksignResult<SendBundleResultApiModel> result = api.SendBundle(token, bundle);

			if (result.IsSuccessful)
			{
				Console.WriteLine("Access code for recipient " + result.Response.AccessCodes[0].RecipientName + " is " + result.Response.AccessCodes[0].AccessCode);
				Console.WriteLine("Bundle id is : " + result.Response.BundleId);
			}
			else
			{
				Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}" );
			}

		}

	}
}
