using Bulksign.Api;

namespace Bulksign.ApiSamples;

//this sample shows how to add a new user to your organization
public class AddUserToOrganization
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
            NewUserApiModel invite = new NewUserApiModel()
            {
                Email = "email@email.com",
                FirstName = "Test",
                LastName = "Test",
                Role = UserRoleApiType.User,
                
                UILanguage = "es-ES",   
                DefaultDraftLanguage = "es-ES",   //specify the default language 
                
                NotificationRecipientRejected = true,
                NotificationEnvelopeCompleted = true,
                NotificationRecipientOpenedSignStep = false,
                NotificationRecipientSigned = true          
            };

            ApiResult<string> result = client.AddUserToOrganization(token, invite);

            if (result.IsSuccess)
            {
                Console.WriteLine($"User was successfully added");
            }
            else
            {
                FailedRequestHandler.HandleFailedRequest(result, nameof(client.AddUserToOrganization));
            }
        }
        catch (Exception ex)
        {
            FailedRequestHandler.HandleException(ex, nameof(client.AddUserToOrganization));
        }
    }
}