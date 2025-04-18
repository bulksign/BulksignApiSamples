using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class CreateSignGroup
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
            //NOTE : it requires at least 2 members to create a SignGroup
            
            OrganizationSignGroupApiModel signGroup = new OrganizationSignGroupApiModel()
            {
                Name = "External Service Company",
                Members =
                [
                    new SignGroupMemberApiModel()
                    {
                        Name = "John Fruscinante",
                        Email = "john@notarealemail.com"
                    },
                    new SignGroupMemberApiModel()
                    {
                        Name = "Debra Messing",
                        Email = "debra@notarealemail.com"
                    },
                    new SignGroupMemberApiModel()
                    {
                        Name = "Percuro Messuni",
                        Email = "percuro@notarealemail.com"
                    }
                ]
            };
            
            
            ApiResult<string> result = client.CreateOrganizationSignGroup(token, signGroup);

            if (result.IsSuccess)
            {
                Console.WriteLine("SignGroup was successfully created");
            }
            else
            {
                Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
            }
        }
        catch (BulksignApiException bex)
        {
            //handle failed request here
            Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
        }
    }
}