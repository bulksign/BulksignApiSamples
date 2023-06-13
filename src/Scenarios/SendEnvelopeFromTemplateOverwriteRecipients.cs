﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SendEnvelopeFromTemplateOverwriteRecipients
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

			//replace the identifier with your template Id
			string templateId = "__your+template_identifier__";

			EnvelopeFromTemplateApiModel model = new EnvelopeFromTemplateApiModel()
			{
				ReplaceRecipients = new[]
				{
					new TemplateReplaceRecipientApiModel()
					{
						//determine the recipient that we are replacing by specifying the email address
						ByEmail = new FindRecipientByEmailApiModel() 
						{
							RecipientEmail = "a@a.com",
							RecipientType = RecipientTypeApi.Signer
						},
						//specify the information for the new recipient
						Name = "Test A",
						Email = "myemail@email.com"
					},
					new TemplateReplaceRecipientApiModel()
					{
						ByEmail = new FindRecipientByEmailApiModel()
						{
							RecipientEmail = "b@b.com",
							RecipientType = RecipientTypeApi.Signer
						},
						Name = "Test B",
						Email = "myemailbb@email.com"
					}
				},
				TemplateId = templateId
			};

			try
			{
				BulksignResult<SendEnvelopeResultApiModel> result = client.SendEnvelopeFromTemplate(token, model);

				if (result.IsSuccessful)
				{
					Console.WriteLine("Access code for recipient " + result.Response.RecipientAccess[0].RecipientEmail + " is " + result.Response.RecipientAccess[0].AccessCode);
					Console.WriteLine("EnvelopeId is : " + result.Response.EnvelopeId);
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