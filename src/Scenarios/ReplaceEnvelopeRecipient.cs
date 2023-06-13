using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class ReplaceEnvelopeRecipientSample
	{
		public void RunSample()
		{
			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient api = new BulksignApiClient();

			ReplaceEnvelopeRecipientApiModel re = new ReplaceEnvelopeRecipientApiModel
			{
				EnvelopeId  = "",
				Name        = "New Recipient Name",
				PhoneNumber = "+000000000000",
				Email       = "new_email_address",

				//here is how to set up password authentication for the new recipient
				RecipientAuthenticationMethods = new[]
				{
					new RecipientAuthenticationApiModel
					{
						AuthenticationType = RecipientAuthenticationTypeApi.Password,
						Details            = "_insert_recipient_password"
					}
				},

				//specifying the recipient to be replace can be done in multiple ways 
				//here is an example by specifying the email of the existing recipient
				ByEmail = new FindRecipientByEmailApiModel
				{
					RecipientEmail = "existing_recipient_email",
					RecipientType  = RecipientTypeApi.Signer
				}
			};

			BulksignResult<string> result = api.ReplaceEnvelopeRecipient(token,re);

			if (result.IsSuccessful)
			{
				Console.WriteLine("Recipient has been successfully replaced");
			}
			else
			{
				Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
			}
		}
	}
}