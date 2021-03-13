using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples.Scenarios
{
	public class PrepareSendEnvelope
	{
		public void PrepareAndSendEnvelope()
		{

			AuthorizationApiModel token = new ApiKeys().GetAuthorizationToken();

			if (string.IsNullOrEmpty(token.UserToken))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			FileInput fi = new FileInput()
			{
				Filename = "bulksign_test_Sample.pdf",
				FileContent = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
			};

			BulksignResult<EnvelopeApiModel> result = api.PrepareSendEnvelope(token, new[] { fi });


			if (result.IsSuccessful)
			{
				EnvelopeApiModel model = result.Response;

				//change this with valid email address, otherwise the SendEnvelope request will fail
				model.Recipients[0].Email = "enter_recipient_email_here";
				model.Recipients[0].Name = "RecipientName";

				foreach (DocumentApiModel document in model.Documents)
				{
					foreach (AssignmentApiModel assignment in document.FieldAssignments)
					{
						assignment.AssignedToRecipientEmail = model.Recipients[0].Email;
					}
				}

				BulksignResult<SendEnvelopeResultApiModel> envelope = api.SendEnvelope(token, model);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"Envelope with id {envelope.Response.EnvelopeId} was created");
				}
			}
			else
			{
				Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
			}

		}
	}
}