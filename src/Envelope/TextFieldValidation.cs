﻿using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class TextFieldValidation
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
			envelope.EnvelopeType = EnvelopeTypeApi.Serial;
			envelope.Name = "Test envelope";

			envelope.Recipients = new[]
			{
				new RecipientApiModel
				{
					Name = "Bulksign Test",
					Email = "contact@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel
				{
					Index = 1,
					FileName = "test.pdf",
					FileContentByteArray = new FileContentByteArray
					{
						ContentBytes = FileUtility.GetFileContent("forms.pdf")
					},
					FieldAssignments = new[]
					{
						new AssignmentApiModel
						{
							AssignedToRecipientEmail = envelope.Recipients[0].Email,
							Fields = new[]
							{
								new FieldAssignmentApiModel
								{
									FieldId = "Text1",
									IsRequired = true,

									//enable numeric validation and also range (only numbers between 3-28 are allowed)
									TextBoxValidation = new InputValidationApiModel
									{
										Tooltip = "Please enter a numeric value between 3 and 28",
										Placeholder = "3 to 28",
										ValidationType = TextInputValidationTypeApi.Number,
										NumberValidation = new NumberValidationApiModel
										{
											AllowFloatingPointNumbers = false,
											Range = new NumericRangeApiModel
											{
												From = "3",
												To = "28"
											}
										}
									}
								},
								//phone number - the allowed format is +{countrycode}{phone}
								new FieldAssignmentApiModel
								{
									FieldId = "Text1",
									IsRequired = true,
									TextBoxValidation = new InputValidationApiModel
									{
										Tooltip = "Please enter your phone number in format +{countrycode}{phone}",
										ValidationType = TextInputValidationTypeApi.PhoneNumber
									}
								},
								//email address
								new FieldAssignmentApiModel
								{
									FieldId = "Text1",
									IsRequired = true,
									TextBoxValidation = new InputValidationApiModel
									{
										Tooltip = "Please enter a email address",
										ValidationType = TextInputValidationTypeApi.EmailAddress
									}
								},
								//custom regex
								new FieldAssignmentApiModel
								{
									FieldId = "Text1",
									IsRequired = true,
									TextBoxValidation = new InputValidationApiModel
									{
										Tooltip = "Please enter the custom value",
										ValidationType = TextInputValidationTypeApi.Regex,
										RegexValidation = new RegexValidationApiModel
										{
											Regex = "d(b+)d"
										}
									}
								},
								//date, you must provide the format
								new FieldAssignmentApiModel
								{
									FieldId = "Text1",
									IsRequired = true,
									TextBoxValidation = new InputValidationApiModel
									{
										Tooltip = "Please enter the date in format yyyy-mm-dd",
										ValidationType = TextInputValidationTypeApi.Date,
										DateValidation = new DateValidationApiModel
										{
											DateFormat = "yyyy-mm-dd"
										}
									}
								},
								//time, format is hh:mm
								new FieldAssignmentApiModel
								{
									FieldId = "Text1",
									IsRequired = true,
									TextBoxValidation = new InputValidationApiModel
									{
										Tooltip = "Please enter the time in format hh:mm",
										ValidationType = TextInputValidationTypeApi.Time
									}
								}
							}
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