using System;
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
				ApiResult<SendEnvelopeResultApiModel> result = client.SendEnvelopeFromTemplate(token, model);

				if (result.IsSuccess)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail + " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("EnvelopeId is : " + result.Result.EnvelopeId);
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.SendEnvelope));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.SendEnvelope));
			}
		}
	}
}