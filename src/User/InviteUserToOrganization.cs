using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class InviteUserToOrganization
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

            UserInvitationApiModel invite = new UserInvitationApiModel()
            {
                Email = "email@email.com",
                FirstName = "Test",
                LastName = "Test",
                Role = UserRoleApiType.User
            };

            
            //to specify a custom role just use
            // UserInvitationApiModel invite = new UserInvitationApiModel()
            // {
            //     Email = "email@email.com",
            //     FirstName = "Test",
            //     LastName = "Test",
            //     Role = UserRoleApiType.Custom,
            //     CustomRoleName = "myCustomRole"
            // };
            
            
            ApiResult<string> result = client.InviteUserToOrganization(token, invite);

            if (result.IsSuccess == false)
            {
                Console.WriteLine($"Request failed : RequestId {result.RequestId}, ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
            }
            else
            {
                Console.WriteLine($"User was successfully added");
            }
        }
        catch (BulksignApiException bex)
        {
            //handle failed request here
            Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
        }
    }
        
}