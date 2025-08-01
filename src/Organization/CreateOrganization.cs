﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class CreateOrganization
	{
		//NOTE : The CreateOrganization API is ONLY available for the OnPremise version of Bulksign.
		//It will NOT work on SAAS version at https://bulksign.com

		public void RunSample()
		{
			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit Authentication.cs and set your own API key there");
				return;
			}

			BulksignApiClient client = new BulksignApiClient();

			ApiResult<string> result = null;
			
			try
			{
				result = client.CreateOrganization(token, new CreateOrganizationApiModel()
				{
					OrganizationName = "MyOrganization",
					AdministratorEmail = "admin@email.com",
					AdministratorFirstName = "FirstName",
					AdministratorLastName = "SecondName",
					AdministratorPassword = "AdminPassword"
				});

				if (result.IsSuccess == false)
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.CreateOrganization));
					return;
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.CreateOrganization));
				return;
			}

			//make the new requests authenticated 
			AuthenticationApiModel newOrgToken = new AuthenticationApiModel()
			{
				Key = result.Result, 
				UserEmail = "admin@email.com"
			};

			//update the org settings with whichever values we require.
			//Note that all properties are optional, you should only set values for the properties that you want updated and ignore the rest

			client.UpdateOrganizationSettings(newOrgToken, new OrganizationUpdateSettingsApiModel()
			{
				SignatureSettings = new OrganizationSigningSettingsApiModel()
				{
					AllowRejectWithoutRejectionText = false,
					EnableLongTermValidation = true,
					ForceSignerToReadDocument = true
				}
			});

			//now add more users to this organization
			client.InviteUserToOrganization(newOrgToken, new UserInvitationApiModel()
			{
				Email = "new.user@mycompnay.com", 
				FirstName = "John", 
				LastName = "JohnLastName", 
				Role = UserRoleApiType.Administrator
			});

			//if we need to send email in a new language also create the email templates 


			string language = "pt-BR";

			client.AddEmailTemplate(newOrgToken, new EmailTemplateApiModel()
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