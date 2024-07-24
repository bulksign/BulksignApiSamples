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
				Recipients = new[]
				{
					new RecipientApiModel()
					{
						Email = "test.recipient@test.com",
						Name = "Recipient Name",
						Index = 3,
						RecipientType = RecipientTypeApi.Signer
					}
				},
				Documents = new[]
				{
					new DocumentApiModel()
					{
						Index = 1,
						FileName = "myfile.pdf",
						FileContentByteArray = new FileContentByteArray()
						{
							ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
						}
					}
				}
			};

			try
			{
				BulksignResult<string> result = client.AddDocumentsRecipientsToDraft(token, model);
				
				if (result.IsSuccessful)
				{
					Console.WriteLine($"Data was successfully added to draft");
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
}