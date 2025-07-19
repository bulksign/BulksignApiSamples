using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class OverwriteFormFieldValues
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

			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType    = EnvelopeTypeApi.Serial;
			envelope.Name            = "Test envelope";

			envelope.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Name = "Bulksign Test",
					Email = "enter_your_email_address",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel()
				{
					Index = 2,
					FileName = "forms.pdf",
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = FileUtility.GetFileContent("forms.pdf")
					},

					OverwriteValues = new []
					{
						//overwrite textbox value 
						new OverwriteFieldValueApiModel()
						{
							FieldName = "Text1",
							FieldValue = "This is a test text"
						},
						//select a specific radio button 
						new OverwriteFieldValueApiModel()
						{
							FieldName = "Group3",
							FieldValue = "Choice2"
						},

						//select a checkbox 
						new OverwriteFieldValueApiModel()
						{
							FieldName = "Check Box2",
							FieldValue = "True"
						},

						//selected value in combobox
						new OverwriteFieldValueApiModel()
						{
							FieldName = "Dropdown5",
							FieldValue = "Item3"
						},

						//selected value in combobox
						new OverwriteFieldValueApiModel()
						{
							FieldName = "List Box4",
							FieldValue = "Item2"
						}

					}
				}
			};

			try
			{
				ApiResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccess)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail + " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("Envelope id is : " + result.Result.EnvelopeId);
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