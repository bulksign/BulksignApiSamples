﻿using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class PrepareSendEnvelope
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
			prepare.DocumentParseOptions = new DocumentParseOptionApiModel()
			{
				ParseTags = false,
				DeleteTagText = false
			};

			prepare.Files = new[]
			{
				new FileInput()
				{
					Filename = "bulksign_test_Sample.pdf",
					FileContent = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
				}
			};


			BulksignResult<EnvelopeApiModel> result = null;

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

			EnvelopeApiModel model = result.Response;

			//now change the email placeholder with the real recipient email address
			model.Recipients[0].Email = "enter_recipient_email_here";
			model.Recipients[0].Name = "RecipientName";

			//assign all form fields to the first recipient . 
			//Obviously if you have multiple recipients, assign the fields as needed
			foreach (DocumentApiModel document in model.Documents)
			{
				foreach (AssignmentApiModel assignment in document.FieldAssignments)
				{
					assignment.AssignedToRecipientEmail = model.Recipients[0].Email;
				}
			}

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
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}