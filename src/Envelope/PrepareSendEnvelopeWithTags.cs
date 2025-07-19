using System;
using System.Collections.Generic;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class PrepareSendEnvelopeWithTags
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

			PrepareEnvelopeApiModel prepare = new PrepareEnvelopeApiModel();

			//flag that determines if the PDF documents should be parsed for tags
			prepare.DocumentParseOptions = new DocumentParseOptionApiModel
			{
				ParseTags = true,
				DeleteTagText = true
			};

			prepare.Files = new[]
			{
				new FileInput
				{
					Filename = "bulksign_test_sample.odt",
					FileContent = FileUtility.GetFileContent("bulksign_advanced_tags.odt")
				}
			};

			ApiResult<EnvelopeApiModel> result;

			try
			{
				result = client.PrepareSendEnvelope(token, prepare);
			}
			catch (Exception ex)
			{
				FailedRequestHandler.HandleException(ex, nameof(client.PrepareSendEnvelope));
				return;
			}

			if (result.IsSuccess == false)
			{
				FailedRequestHandler.HandleFailedRequest(result, nameof(client.PrepareSendEnvelope));
				return;
			}

			//the model will include 2 placeholder recipients because we have 2 tags in the documents with different indexes

			EnvelopeApiModel model = result.Result;

			//now change the email placeholder with the real recipient email address
			model.Recipients[0].Email = "test@email.com";
			model.Recipients[0].Name = "First Recipient";

			model.Recipients[0].Email = "second@email.com";
			model.Recipients[0].Name = "Second Recipient";

			//now re-assign the form fields, we already know the ids 

			List<AssignmentApiModel> assignments = new List<AssignmentApiModel>();

			assignments.Add(new AssignmentApiModel
			{
				AssignedToRecipientEmail = model.Recipients[0].Email,
				Signatures = new[]
				{
					new SignatureAssignmentApiModel
					{
						FieldId = "sigFieldSender",
						SignatureType = SignatureTypeApi.DrawTypeToSign
					}
				}
			});

			assignments.Add(new AssignmentApiModel
			{
				AssignedToRecipientEmail = model.Recipients[1].Email,
				Signatures = new[]
				{
					new SignatureAssignmentApiModel
					{
						FieldId = "sigFieldCustomer",
						SignatureType = SignatureTypeApi.ClickToSign
					}
				}
			});

			model.Documents[0].FieldAssignments = assignments.ToArray();

			try
			{
				ApiResult<SendEnvelopeResultApiModel> envelope = client.SendEnvelope(token, model);

				if (result.IsSuccess)
				{
					Console.WriteLine($"Envelope with id {envelope.Result.EnvelopeId} was created");
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