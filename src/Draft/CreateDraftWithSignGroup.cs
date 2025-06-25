using System.Data;
using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class CreateDraftWithSignGroup
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

			ApiResult<OrganizationSignGroupApiModel[]> resultGroups = client.GetOrganizationSignGroups(token);

			if (resultGroups.IsSuccess)
			{
				Console.WriteLine($"Found {resultGroups.Result.Length} sign groups");
			}
			else
			{
				FailedRequestHandler.HandleFailedRequest(resultGroups, nameof(client.GetOrganizationSignGroups));
				return;
			}

			if (resultGroups.Result.Any() == false)
			{
				Console.WriteLine($"No usable sign groups found. Please create at least 1 SignGroup from Settings->Sign Groups ");
				return;
			}

			OrganizationSignGroupApiModel? signGroup = resultGroups.Result.FirstOrDefault();
			
			DraftApiModel newDraft = new DraftApiModel()
			{
				Name                          = "Test Draft",
				AllowRecipientDelegation      = false,
				CertifyDocumentsBeforeSending = false,
				EnableBatchSign               = true
			};

			
			//now create the draft recipients based on the SignGroup
			newDraft.Recipients = new RecipientApiModel[signGroup.Members.Length];

			//"copy" the recipients from SignGroup to the draft
			
			for (int i = 0; i < newDraft.Recipients.Length; i++)
			{
				newDraft.Recipients[i] = new RecipientApiModel()
				{
					Email = signGroup.Members[i].Email,
					Name  = signGroup.Members[i].Name,
					RecipientType = RecipientTypeApi.Signer,
					SignNotificationChannel = SignNotificationChannelTypeApi.Email,
					SignGroup = signGroup.Name,
					PhoneNumber = signGroup.Members[i].PhoneNumber
				};
			}
			
			
			
			ApiResult<string> result = client.CreateDraft(token, newDraft);

			if (resultGroups.IsSuccess)
			{
				Console.WriteLine($"Draft with id '{result.Result}' was created");
			}
			else
			{
				FailedRequestHandler.HandleFailedRequest(result, nameof(client.CreateDraft));
			}
		}
		catch (Exception ex)
		{
			FailedRequestHandler.HandleException(ex, nameof(client.CreateDraft));
		}
	}
}