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
					Filename = "bulksign_test_Sample.odt",
					FileContent = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_advanced_tags.odt")
				}
			};

			BulksignResult<EnvelopeApiModel> result;

			try
			{
				result = client.PrepareSendEnvelope(token, prepare);
			}
			catch (BulksignException bex)
			{
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
				return;
			}


			if (result.IsSuccessful == false)
			{
				Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
				return;
			}

			//the model will include 2 placeholder recipients because we have 2 tags in the documents with different indexes

			EnvelopeApiModel model = result.Response;

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
				BulksignResult<SendEnvelopeResultApiModel> envelope = client.SendEnvelope(token, model);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"Envelope with id {envelope.Response.EnvelopeId} was created");
				}
				else
				{
					Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
				}
			}
			catch (BulksignException bex)
			{
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}" );	
			}
		}
	}
}