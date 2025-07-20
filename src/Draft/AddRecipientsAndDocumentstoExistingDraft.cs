using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class AddRecipientsAndDocumentsToExistingDraft
	{
		public void RunSample()
		{
			//set your existing draftId here
			string existingDraftId = ".....";

			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit Authentication.cs and set your own API key there");
				return;
			}


			BulksignApiClient client = new BulksignApiClient();

			AddDocumentsOrRecipientsToDraftApiModel model = new AddDocumentsOrRecipientsToDraftApiModel()
			{
				DraftId = existingDraftId,
				Recipients =
				[
					new RecipientApiModel()
					{
						Email = "test.recipient@test.com",
						Name = "Recipient Name",
						Index = 3,
						RecipientType = RecipientTypeApi.Signer
					}
				],
				Documents =
				[
					new DocumentApiModel()
					{
						Index = 1,
						FileName = "bulksign_test_sample.pdf",
						FileContentByteArray = new FileContentByteArray()
						{
							ContentBytes = FileUtility.GetFileContent("bulksign_test_sample.pdf")
						}
					}
				]
			};

			try
			{
				ApiResult<string> result = client.AddDocumentsRecipientsToDraft(token, model);
				
				if (result.IsSuccess)
				{
					Console.WriteLine($"Data was successfully added to draft");
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.AddDocumentsRecipientsToDraft));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.AddDocumentsRecipientsToDraft));
			}
		}
	}
}