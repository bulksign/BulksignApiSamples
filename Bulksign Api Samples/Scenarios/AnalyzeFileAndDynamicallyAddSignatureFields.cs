using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bulksign.Api;

namespace Bulksign.ApiSamples.Scenarios
{
	public class AnalyzeFileAndDynamicallyAddSignatureFields
	{

		public void SendBundle()
		{
			BulkSignApi api = new BulkSignApi();


			AuthorizationApiModel token = new ApiKeys().GetAuthorizationToken();

			if (string.IsNullOrEmpty(token.UserToken))
			{
				Console.WriteLine("Please edit ApiKeys.cs and put your own token/email");
				return;
			}


			byte[] pfdContent = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\singlepage.pdf");

			//this will upload the PDF file, analyze it and return a unique file identifier and the PDF file form fields
			BulksignResult<AnalyzedFileResultApiModel> analyzeResult = api.AnalyzeFile(token, pfdContent);

			BundleApiModel bundle = new BundleApiModel();

			bundle.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Email = "test@test.com",
					Index = 1,
					Name = "Test",
					RecipientType = RecipientTypeApi.Signer
				},

				new RecipientApiModel()
				{
					Email = "other@test.com",
					Index = 1,
					Name = "Other",
					RecipientType = RecipientTypeApi.Signer
				}
			};


			//since we have already sent the PDF document to AnalyzeFile, "reuse it" for the SendBundle request and just pass the file identifier
			bundle.Documents = new[]
			{
				new DocumentApiModel()
				{
					FileIdentifier = new FileIdentifier()
					{
						Identifier = analyzeResult.Response.FileIdentifier
					}
				}
			};

			//assign all form fields / signature field to first recipient

			List<FormFieldResultApiModel> signatures = analyzeResult.Response.Fields.Where(model => model.FieldType == FormFieldTypeApi.Signature).ToList();
			List<FormFieldResultApiModel> fields = analyzeResult.Response.Fields.Where(model => model.FieldType != FormFieldTypeApi.Signature).ToList();

			AssignmentApiModel assignment = new AssignmentApiModel();
			assignment.AssignedToRecipientEmail = bundle.Recipients[0].Email;
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

			bundle.Documents[0].FieldAssignments = new[] { assignment };


			//now add a signature field for the second recipient on each page of the document
			int numberPages = analyzeResult.Response.NumberOfPages;

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
				sig.AssignedToRecipientEmail = bundle.Recipients[1].Email;

				newSignatures.Add(sig);
			}

			bundle.Documents[0].NewSignatures = newSignatures.ToArray();

			BulksignResult<SendBundleResultApiModel> result = api.SendBundle(token, bundle);

			if (result.IsSuccessful)
			{
				Console.WriteLine("Access code for recipient " + result.Response.AccessCodes[0].RecipientName + " is " + result.Response.AccessCodes[0].AccessCode);
				Console.WriteLine("Bundle id is : " + result.Response.BundleId);
			}
			else
			{
				Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
			}

		}

	}
}