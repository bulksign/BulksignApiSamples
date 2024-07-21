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

            BulksignResult<string> result = client.AddUserToOrganization(token, invite);

            if (result.IsSuccessful == false)
            {
                Console.WriteLine($"Request failed : RequestId {result.RequestId}, ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
            }
            else
            {
                Console.WriteLine($"User was successfully added");
            }
        }
        catch (BulksignException bex)
        {
            //handle failed request here
            Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
        }
    }
        
}