using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class ReplaceEnvelopeRecipientSample
	{
		public void Run()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();


            ReplaceEnvelopeRecipientApiModel re = new ReplaceEnvelopeRecipientApiModel()
            {
                EnvelopeId = "",
                Name = "New Recipient Name",
                PhoneNumber = "+000000000000",
                Email = "new_email_Address",

                //here is how to set up password authentication for the new recipient
                RecipientAuthenticationMethods = new RecipientAuthenticationApiModel[]
                {
                    new RecipientAuthenticationApiModel()
                    {
                        AuthenticationType = RecipientAuthenticationTypeApi.Password,
                        Details = "dfhsjkhfjh3244983"
                    }
                },

                //specifying the recipient to be replace can be done in multiple ways 
                //here is an example by specifying the email of the existing recipient
                ByEmail = new FindRecipientByEmailApiModel()
                {
                    RecipientEmail = "existing_recipient_email",
                    RecipientType = RecipientTypeApi.Signer
                }
            };


			BulksignResult<string> result = api.ReplaceEnvelopeRecipient(token, re);

			if (result.IsSuccessful)
            {
				Console.WriteLine($"Recipient has been successfully replaced");
            }
			else
            {
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
            }

		}

	}
}