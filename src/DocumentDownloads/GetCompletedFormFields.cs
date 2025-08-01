﻿using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class GetCompletedFormFields
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

			//set your own envelopeId here
			string envelopeId = "..............";

			try
			{
				ApiResult<RecipientFormFillApiModel[]> result = client.GetCompletedFormFields(token, envelopeId);

				if (result.IsSuccess)
				{

					foreach (RecipientFormFillApiModel model in result.Result)
					{
						Console.WriteLine($"Processing form fields for recipient {model.RecipientEmail}");

						foreach (FormFillResultApiModel fieldModel in model.FormFillResult)
						{
							switch (fieldModel.FieldType)
							{
								case FormFieldTypeApi.TextBox:
									break;
								case FormFieldTypeApi.RadioButton:
									break;
								case FormFieldTypeApi.CheckBox:
									break;
								case FormFieldTypeApi.ComboBox:
									break;
								case FormFieldTypeApi.ListBox:
									break;
								case FormFieldTypeApi.Signature:
									break;
								case FormFieldTypeApi.Attachment:
									break;
								default:
									Console.WriteLine("Invalid form field type");
									break;
							}
						}
					}
				}
				else
				{
					FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetCompletedFormFields));
				}
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.GetCompletedFormFields));
			}
		}
	}
}