using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class AnalyzeFileAndDynamicallyAddSignatureFields
	{

		public void RunSample()
		{
			BulksignApiClient client = new BulksignApiClient();

			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();
			
			ApiResult<AnalyzedFileResultApiModel> analyzeResult = client.AnalyzeFile(token, new FileInput()
			{
				FileContent = FileUtility.GetFileContent("bulksign_test_sample.pdf"),
				Filename = "bulksign_test_sample.pdf"
			});

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit Authentication.cs and set your own API key there");
				return;
			}

			byte[] pfdContent = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\singlepage.pdf");


			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType = EnvelopeTypeApi.Serial;

			envelope.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Email = "test@test.com", //please replace this with own own email address
					Index = 1,
					Name = "Test",
					RecipientType = RecipientTypeApi.Signer
				},

				new RecipientApiModel()
				{
					Email = "other@test.com", //please replace this with own own email address
					Index = 1,
					Name = "Other",
					RecipientType = RecipientTypeApi.Signer
				}
			};


			//since we have already sent the PDF document to AnalyzeFile, "reuse it" for the SendEnvelope request and just pass the file identifier
			envelope.Documents = new[]
			{
				new DocumentApiModel()
				{
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = pfdContent,
					}
				}
			};

			//assign all form fields / signature field to first recipient

			List<FormFieldResultApiModel> signatures = analyzeResult.Result.Fields.Where(model => model.FieldType == FormFieldTypeApi.Signature).ToList();
			List<FormFieldResultApiModel> fields = analyzeResult.Result.Fields.Where(model => model.FieldType != FormFieldTypeApi.Signature).ToList();

			AssignmentApiModel assignment = new AssignmentApiModel();
			assignment.AssignedToRecipientEmail = envelope.Recipients[0].Email;
			assignment.Signatures = new SignatureAssignmentApiModel[signatures.Count];


			for (int i = 0; i < signatures.Count; i++)
			{
				assignment.Signatures[i].FieldId = signatures[i].Id;
				assignment.Signatures[i].SignatureType = SignatureTypeApi.ClickToSign;
			}

			for (int i = 0; i < fields.Count; i++)
			{
				assignment.Fields[i].FieldId = fields[i].Id;
			}

			envelope.Documents[0].FieldAssignments = new[] { assignment };


			//now add a signature field for the second recipient on each page of the document
			int numberPages = analyzeResult.Result.NumberOfPages;

			List<NewSignatureApiModel> newSignatures = new List<NewSignatureApiModel>();

			for (int i = 1; i <= numberPages; i++)
			{
				NewSignatureApiModel sig = new NewSignatureApiModel();
				sig.Height = 100;
				sig.Width = 300;
				sig.PageIndex = i;
				sig.Left = 10;
				sig.Top = 100;
				sig.SignatureType = SignatureTypeApi.ClickToSign;
				sig.AssignedToRecipientEmail = envelope.Recipients[1].Email;

				newSignatures.Add(sig);
			}

			envelope.Documents[0].NewSignatures = newSignatures.ToArray();

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