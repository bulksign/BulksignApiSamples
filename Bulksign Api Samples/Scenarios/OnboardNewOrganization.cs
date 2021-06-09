using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class OnboardNewOrganization
	{
		//NOTE : The CreateORganization API is ONLY available for the OnPremise version of Bulksign.
		//It will not work on https://bulksign.com

		public void CreateOrganization()
		{
			AuthenticationApiModel token = new ApiKeys().GetAuthorizationToken();

			if (string.IsNullOrEmpty(token.Token))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			BulksignResult<string> result = api.CreateOrganization(token, new CreateOrganizationApiModel()
			{
				OrganizationName = "MyOrganization",
				AdministratorEmail = "admin@email.com",
				AdministratorFirstName = "FirstName",
				AdministratorLastName = "SecondName",
				AdministratorPassword = "AdminPassword"
			});

			if (result.IsSuccessful == false)
			{
				Console.WriteLine($"Request failed, requestId {result.RequestId}, error {result.ErrorMessage} , code {result.ErrorCode}");
			}


			//make the new requests authenticated 
			AuthenticationApiModel newOrgToken = new AuthenticationApiModel()
			{
				Token = result.Response, 
				UserEmail = "admin@email.com"
			};

			//update the org settings with whichever values we require.
			//Note that all properties are optional, you should only set values for the properties that you want updated and ignore the rest

			api.UpdateOrganizationSettings(newOrgToken, new OrganizationUpdateSettingsApiModel()
			{
				EmailSenderType = EmailSenderTypeApi.Organization,

				SignatureSettings = new OrganizationSigningSettingsApiModel()
				{
					AllowRejectWithoutRejectionText = false,
					EnableLongTermValidation = true,
					ForceSignerToReadDocument = true
				}
			});

			//now add more users to this organization
			api.InviteUserToOrganization(newOrgToken, new UserInvitationApiModel()
			{
				Email = "new.user@mycompnay.com", 
				FirstName = "John", 
				LastName = "JohnLastName", 
				IsAdministrator = true
			});

			//if we need to send email in a new language also create the email templates 


			string language = "pt-BR";

			api.AddEmailTemplate(newOrgToken, new EmailTemplateApiModel()
			{
				Language = language,
				Templates = new EmailTemplateDescriptorApiModel[]
				{
					new EmailTemplateDescriptorApiModel()
					{
						TemplateType = EmailTemplateTypeApi.ActivateEmail,
						Subject = "Por favor, ative sua conta",
						Body = "......."
					}
				}
			});
		}
	}
}